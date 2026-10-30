using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace key_luxery1
{
	public partial class uHome : UserControl
	{
		private const int LOW_STOCK_THRESHOLD = 5;
		sharedClass db = new sharedClass();

		/// <summary>Raised by the quick-action buttons; frmDashboard handles it.</summary>
		public event Action<string> NavigateRequested;

		private bool _loadedOnce;
		private readonly Button[] _quick = new Button[4];

		public uHome()
		{
			InitializeComponent();
			DoubleBuffered = true;

			btnRefresh.Click += btnRefresh_Click;
			btnRefresh.Cursor = Cursors.Hand;
			btnRefresh.Text = "\u21BB  Refresh";

			BuildQuickActions();

			// Re-flow the layout whenever the available area changes
			// (sidebar collapse/expand, window resize).
			pnlTop.SizeChanged += (s, e) => LayoutHome();
			pnlMain.SizeChanged += (s, e) => LayoutHome();
			LayoutHome();
		}

		#region Flexible layout + quick actions

		private void BuildQuickActions()
		{
			_quick[0] = MakeQuick("\U0001F6D2  New sale", "sales", true);
			_quick[1] = MakeQuick("\U0001F9FE  New invoice", "invoice", false);
			_quick[2] = MakeQuick("\U0001F6CD\uFE0F  Add product", "products", false);
			_quick[3] = MakeQuick("\U0001F464  Add customer", "customers", false);
			foreach (var b in _quick) pnlTop.Controls.Add(b);
		}

		private Button MakeQuick(string text, string route, bool primary)
		{
			var b = new Button
			{
				Text = text,
				Size = new Size(124, 34),
				FlatStyle = FlatStyle.Flat,
				Font = new Font("Segoe UI", 9F, FontStyle.Bold),
				Cursor = Cursors.Hand,
				UseVisualStyleBackColor = false,
				BackColor = primary ? Color.FromArgb(37, 99, 235) : Color.White,
				ForeColor = primary ? Color.White : Color.FromArgb(31, 41, 55)
			};
			b.FlatAppearance.BorderSize = primary ? 0 : 1;
			b.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
			b.Click += (s, e) => NavigateRequested?.Invoke(route);
			return b;
		}

		private void LayoutHome()
		{
			if (pnlTop == null || pnlMain == null) return;

			// --- KPI cards spread evenly across the width ---
			Control[] cards = { pnlCard1, pnlCard4, pnlCard5, pnlCard6, pnlCard2, pnlCard3, panel1 };
			const int left = 12, gap = 10, top = 59, h = 90, refreshW = 100;

			btnRefresh.SetBounds(Math.Max(left, pnlTop.Width - left - refreshW), top, refreshW, h);
			int avail = btnRefresh.Left - gap - left;
			int w = Math.Max(110, (avail - gap * (cards.Length - 1)) / cards.Length);
			for (int i = 0; i < cards.Length; i++)
				cards[i].SetBounds(left + i * (w + gap), top, w, h);

			// --- quick action buttons (top-right of header row) ---
			bool showQuick = pnlTop.Width >= 1000;
			for (int i = 0; i < _quick.Length; i++)
			{
				_quick[i].Visible = showQuick;
				_quick[i].Location = new Point(pnlTop.Width - left - (_quick.Length - i) * (124 + 8) + 8, 12);
			}

			// --- two analytics areas share the width ---
			int mTop = 10, mH = Math.Max(200, pnlMain.Height - 20);
			int total = pnlMain.Width - 36;
			int leftW = total * 52 / 100;
			tabAnalytics.SetBounds(12, mTop, leftW, mH);
			tabRightFeed.SetBounds(12 + leftW + 12, mTop, total - leftW, mH);

			// --- financial tab: second column starts at the middle ---
			int col2 = Math.Max(300, pnlFinancialContainer.Width / 2);
			foreach (var c in new Control[] { lblZaadTitle, lblZaadVal, lblEdahabTitle, lblEdahabVal,
											  lblBankTitle, lblBankVal, label1, lblCashVal, lblOtherTitle, lblOtherVal })
				c.Left = col2;
		}

		#endregion

		/// <summary>Reloads every widget. Called by frmDashboard each time Home is opened.</summary>
		public void loadHome()
		{
			_loadedOnce = true;
			LoadDashboardMetrics();
			LoadTopSellingProducts();
			LoadCategorySales();
			LoadFinancialBreakdown();
			LoadRecentSalesFeed();
			LoadLowStockAlerts();
		}

		private void uHome_Load(object sender, EventArgs e)
		{
			LayoutHome();
			if (!_loadedOnce) loadHome(); // avoids a double load when frmDashboard already called loadHome()
		}

		public void LoadDashboardMetrics()
		{
			try
			{
				// 1. Total Revenue
				object res1 = db.ExecuteScalarQuery("SELECT ISNULL(SUM(grand_total), 0) FROM tblInvoice WHERE (deleted = 0 OR deleted IS NULL)");
				lblCard1Value.Text = Convert.ToDecimal(res1 ?? 0).ToString("C2");

				// 2. Total Sales Count
				object res2 = db.ExecuteScalarQuery("SELECT COUNT(*) FROM tblSale WHERE (deleted = 0 OR deleted IS NULL)");
				lblCard2Value.Text = (res2 ?? 0).ToString();

				// 3. Active Products (in stock)
				object res3 = db.ExecuteScalarQuery("SELECT COUNT(*) FROM tblproduct WHERE quantity>0 AND (deleted = 0 OR deleted IS NULL)");
				lblCard3Value.Text = (res3 ?? 0).ToString();

				// 4. Last Month Revenue
				string qryLastMonthRev = @"SELECT ISNULL(SUM(grand_total), 0) FROM tblInvoice 
                                           WHERE invoice_date >= DATEADD(month, DATEDIFF(month, 0, GETDATE()) - 1, 0)
                                           AND invoice_date < DATEADD(month, DATEDIFF(month, 0, GETDATE()), 0)
                                           AND (deleted = 0 OR deleted IS NULL)";
				object res4 = db.ExecuteScalarQuery(qryLastMonthRev);
				lblCard4Value.Text = Convert.ToDecimal(res4 ?? 0).ToString("C2");

				// 5. Total Expenses
				object res5 = db.ExecuteScalarQuery("SELECT ISNULL(SUM(Amount), 0) from tblExpenses where deleted=0");
				lblCard5Value.Text = Convert.ToDecimal(res5 ?? 0).ToString("C2");

				// 6. Last Month Expenses
				string qryLastMonthExp = @"SELECT ISNULL(SUM(Amount), 0) from tblExpenses 
                                           WHERE ExpenseDate >= DATEADD(month, DATEDIFF(month, 0, GETDATE()) - 1, 0)
                                           AND ExpenseDate < DATEADD(month, DATEDIFF(month, 0, GETDATE()), 0)";
				object res6 = db.ExecuteScalarQuery(qryLastMonthExp);
				lblCard6Value.Text = Convert.ToDecimal(res6 ?? 0).ToString("C2");

				// Payment method breakdown
				lblZaadVal.Text = PaymentTotal("payment_method = 'Zaad'");
				lblEdahabVal.Text = PaymentTotal("payment_method = 'eDahab'");
				lblCashVal.Text = PaymentTotal("payment_method = 'Cash'");
				lblBankVal.Text = PaymentTotal("payment_method = 'Premiere Bank'");
				lblOtherVal.Text = PaymentTotal("payment_method NOT IN ('Zaad', 'eDahab', 'Premiere Bank','Cash')");

				// Total Customers
				object custObj = db.ExecuteScalar("SELECT COUNT(customer_id) FROM tblCustomer WHERE deleted = 0");
				int totalCustomers = (custObj != null && custObj != DBNull.Value) ? Convert.ToInt32(custObj) : 0;
				lblCustomers.Text = totalCustomers.ToString("N0");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error loading dashboard metrics: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private string PaymentTotal(string condition)
		{
			string q = "SELECT ISNULL(SUM(amount_paid), 0) FROM tblPayment WHERE " + condition + " AND (deleted = 0 OR deleted IS NULL)";
			object r = db.ExecuteScalarQuery(q);
			return Convert.ToDecimal(r ?? 0).ToString("C2");
		}

		/// <summary>Top 10 best selling products</summary>
		private void LoadTopSellingProducts()
		{
			try
			{
				string query = "SELECT TOP 10 product_name AS [Product], SUM(quantity) AS [Qty Sold], SUM(total_price) AS [Revenue] " +
							   "FROM tblSale WHERE deleted = 0 " +
							   "GROUP BY product_name " +
							   "ORDER BY [Qty Sold] DESC";

				dgvTopProducts.DataSource = sharedClass.GetData(query);
				if (dgvTopProducts.Columns.Contains("Revenue"))
					dgvTopProducts.Columns["Revenue"].DefaultCellStyle.Format = "C2";
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("Top Products Error: " + ex.Message);
			}
		}

		/// <summary>Sales aggregated by product category</summary>
		private void LoadCategorySales()
		{
			try
			{
				string query = "SELECT p.category AS [Category], SUM(s.quantity) AS [Items Sold], SUM(s.total_price) AS [Total Revenue] " +
							   "FROM tblSale s " +
							   "INNER JOIN tblProduct p ON s.product_id = p.product_id " +
							   "WHERE s.deleted = 0 " +
							   "GROUP BY p.category " +
							   "ORDER BY [Total Revenue] DESC";

				dgvCategorySales.DataSource = sharedClass.GetData(query);
				if (dgvCategorySales.Columns.Contains("Total Revenue"))
					dgvCategorySales.Columns["Total Revenue"].DefaultCellStyle.Format = "C2";
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("Category Sales Error: " + ex.Message);
			}
		}

		/// <summary>Gross sales, tax, discount and net profit</summary>
		private void LoadFinancialBreakdown()
		{
			try
			{
				string query = "SELECT SUM(subtotal) AS GrossSales, SUM(tax) AS TotalTax, SUM(discount) AS TotalDiscount, SUM(total_profit) AS NetProfit " +
							   "FROM tblInvoice WHERE deleted = 0 AND status = 'Paid'";

				DataTable dt = sharedClass.GetData(query);
				if (dt != null && dt.Rows.Count > 0)
				{
					DataRow row = dt.Rows[0];
					lblGrossSalesVal.Text = Money(row["GrossSales"]);
					lblTaxVal.Text = Money(row["TotalTax"]);
					lblDiscountVal.Text = Money(row["TotalDiscount"]);
					lblNetProfitVal.Text = Money(row["NetProfit"]);
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("Financial Breakdown Error: " + ex.Message);
			}
		}

		private static string Money(object v)
		{
			return (v != null && v != DBNull.Value ? Convert.ToDecimal(v) : 0m).ToString("C2");
		}

		/// <summary>Latest paid invoices</summary>
		private void LoadRecentSalesFeed()
		{
			try
			{
				string query = "SELECT TOP 15 invoice_no AS [Inv #], customer_name AS [Customer], grand_total AS [Total] " +
							   "FROM tblInvoice WHERE deleted = 0 AND status = 'Paid' " +
							   "ORDER BY invoice_date DESC";

				dgvRecentSales.DataSource = sharedClass.GetData(query);
				if (dgvRecentSales.Columns.Contains("Total"))
					dgvRecentSales.Columns["Total"].DefaultCellStyle.Format = "C2";
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("Recent Sales Error: " + ex.Message);
			}
		}

		/// <summary>Low stock items with conditional row colors</summary>
		private void LoadLowStockAlerts()
		{
			try
			{
				string query = "SELECT product_name AS [Product], quantity AS [Stock], price AS [Price] " +
							   "FROM tblProduct WHERE deleted = 0 AND quantity <= " + LOW_STOCK_THRESHOLD + " " +
							   "ORDER BY quantity ASC";

				DataTable dt = sharedClass.GetData(query);
				dgvLowStock.DataSource = dt;

				if (dgvLowStock.Columns.Contains("Price"))
					dgvLowStock.Columns["Price"].DefaultCellStyle.Format = "C2";

				foreach (DataGridViewRow row in dgvLowStock.Rows)
				{
					if (row.Cells["Stock"].Value != null && Convert.ToInt32(row.Cells["Stock"].Value) == 0)
					{
						row.DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230);
						row.DefaultCellStyle.ForeColor = Color.DarkRed;
					}
					else
					{
						row.DefaultCellStyle.BackColor = Color.FromArgb(255, 245, 230);
					}
				}

				// Show the alert tab only the first time, so Refresh doesn't steal the user's tab.
				if (dt != null && dt.Rows.Count > 0 && !_alertShown)
				{
					tabRightFeed.SelectedTab = tabLowStock;
					_alertShown = true;
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("Low Stock Alerts Error: " + ex.Message);
			}
		}

		private bool _alertShown;

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			loadHome();
		}
	}
}