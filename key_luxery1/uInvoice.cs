using System;
using System.Data;
using System.Windows.Forms;

namespace key_luxery1
{
	public partial class uInvoice : UserControl
	{
		private DataTable masterDataTable = new DataTable();
		private int currentPage = 1;
		private int pageSize = 20;
		private int totalPages = 1;

		/// <summary>
		/// Property to receive or hold the active user's role.
		/// </summary>
		public string UserRole { get; set; } = "";

		public uInvoice()
		{
			InitializeComponent();
		}

		private void uInvoice_Load(object sender, EventArgs e)
		{
			LoadInvoices();
		}

		public void LoadInvoices()
		{
			try
			{
				// Preserved your exact original query structure and column aliases
				string query = @"SELECT invoice_id AS [ID], 
										invoice_no AS [Invoice No], 
										customer_name AS [Customer Name], 
										invoice_date AS [Date], 
										due_date AS [Due Date],
										subtotal AS [Subtotal ($)], 
										discount AS [Discount ($)], 
										tax AS [Tax ($)], 
										grand_total AS [Grand Total ($)], 
										total_profit AS [Profit ($)], 
										status AS [Status] 
								 FROM tblInvoice 
								 WHERE (deleted = 0 OR deleted IS NULL) 
								   AND status != 'Draft' 
								   AND status != 'Canceled'
								 ORDER BY invoice_id DESC";

				masterDataTable = sharedClass.GetData(query);
				currentPage = 1;
				DisplayPagedData(masterDataTable);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error loading invoices: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void DisplayPagedData(DataTable sourceTable)
		{
			if (sourceTable == null || sourceTable.Rows.Count == 0)
			{
				dgvInvoices.DataSource = null;
				if (lblPageInfo != null) lblPageInfo.Text = "Page 0 of 0";
				totalPages = 1;
				return;
			}

			int totalRecords = sourceTable.Rows.Count;
			totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
			if (totalPages == 0) totalPages = 1;

			if (currentPage > totalPages) currentPage = totalPages;
			if (currentPage < 1) currentPage = 1;

			DataTable pagedTable = sourceTable.Clone();
			int startIndex = (currentPage - 1) * pageSize;
			int endIndex = Math.Min(startIndex + pageSize, totalRecords);

			for (int i = startIndex; i < endIndex; i++)
			{
				pagedTable.ImportRow(sourceTable.Rows[i]);
			}

			dgvInvoices.DataSource = pagedTable;

			// Handle visibility of Profit ($) column based on user role
			try
			{
				if (dgvInvoices.Columns["Profit ($)"] != null)
				{
					string activeRole = this.UserRole;

					// Fallback to active parent Dashboard form if UserRole was not explicitly set
					if (string.IsNullOrEmpty(activeRole) && this.FindForm() is frmDashboard dashboard)
					{
						activeRole = dashboard.lblRole.Text;
					}

					bool isAdmin = string.Equals(activeRole?.Trim(), "Admin", StringComparison.OrdinalIgnoreCase);
					dgvInvoices.Columns["Profit ($)"].Visible = isAdmin;
				}
			}
			catch
			{
				// Ignore non-critical UI column visibility errors
			}

			if (lblPageInfo != null)
			{
				lblPageInfo.Text = $"Page {currentPage} of {totalPages}";
			}

			if (btnFirst != null) btnFirst.Enabled = (currentPage > 1);
			if (btnPrev != null) btnPrev.Enabled = (currentPage > 1);
			if (btnNext != null) btnNext.Enabled = (currentPage < totalPages);
			if (btnLast != null) btnLast.Enabled = (currentPage < totalPages);
		}

		private void txtSearchInvoice_TextChanged(object sender, EventArgs e)
		{
			string keyword = txtSearchInvoice.Text.Trim().Replace("'", "''");
			if (masterDataTable != null && masterDataTable.Rows.Count > 0)
			{
				DataView dv = masterDataTable.DefaultView;
				dv.RowFilter = $"[Invoice No] LIKE '%{keyword}%' OR [Customer Name] LIKE '%{keyword}%' OR [Status] LIKE '%{keyword}%'";

				DataTable filteredTable = dv.ToTable();
				currentPage = 1;
				DisplayPagedData(filteredTable);
			}
		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			txtSearchInvoice.Clear();
			LoadInvoices();
		}

		private void btnFirst_Click(object sender, EventArgs e)
		{
			currentPage = 1;
			DisplayPagedData(GetCurrentFilteredTable());
		}

		private void btnPrev_Click(object sender, EventArgs e)
		{
			if (currentPage > 1)
			{
				currentPage--;
				DisplayPagedData(GetCurrentFilteredTable());
			}
		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			if (currentPage < totalPages)
			{
				currentPage++;
				DisplayPagedData(GetCurrentFilteredTable());
			}
		}

		private void btnLast_Click(object sender, EventArgs e)
		{
			currentPage = totalPages;
			DisplayPagedData(GetCurrentFilteredTable());
		}

		private DataTable GetCurrentFilteredTable()
		{
			string keyword = txtSearchInvoice.Text.Trim().Replace("'", "''");
			if (string.IsNullOrEmpty(keyword) || masterDataTable == null)
			{
				return masterDataTable;
			}
			else
			{
				DataView dv = masterDataTable.DefaultView;
				dv.RowFilter = $"[Invoice No] LIKE '%{keyword}%' OR [Customer Name] LIKE '%{keyword}%' OR [Status] LIKE '%{keyword}%'";
				return dv.ToTable();
			}
		}

		private void dgvInvoices_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				// Double-click row to inspect or manage invoice details
			}
		}

		private void btnManageInvoice_Click(object sender, EventArgs e)
		{
			frmManageInvoice manageForm = new frmManageInvoice();
			frmDashboard dashboard = new frmDashboard();
			if(dashboard.lblRole.Text .Trim().Equals("Admin", StringComparison.OrdinalIgnoreCase))
			{
				manageForm.btnDeleteInvoice.Visible= true;
			}
			else
			{
				manageForm.btnDeleteInvoice.Visible= false;
				
			}
			manageForm.ShowDialog();
		}
	}
}