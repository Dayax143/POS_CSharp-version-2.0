using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace key_luxery1
{
	public partial class frmManageInvoice : Form
	{
		sharedClass db = new sharedClass();
		private string selectedInvoiceNoForPrint = "";
		string userRole = Properties.Settings.Default.userRole;
		public frmManageInvoice()
		{
			InitializeComponent();
		}

		public string UserRole { get; set; } = "";
		private void frmManageInvoice_Load(object sender, EventArgs e)
		{
			try
			{
				LoadInvoices();
				//// Check user role and hide buttons if not Admin
				//if (!string.Equals(userRole, "Admin", StringComparison.OrdinalIgnoreCase))
				//{
				//	btnDeleteInvoice.Visible = false;
				//}
				//else if (string.Equals(userRole, "Admin", StringComparison.OrdinalIgnoreCase))
				//{
				//	btnDeleteInvoice.Visible = true;
				//}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error checking permissions: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		// --- 1. LOAD INVOICES MATCHING EXACT tblInvoice SCHEMA ---
		public void LoadInvoices()
		{
			try
			{
				string query = @"SELECT invoice_id AS [ID], 
                                        invoice_no AS [Invoice No], 
                                        customer_name AS [Customer Name], 
                                        invoice_date AS [Invoice Date], 
                                        due_date AS [Due Date],
                                        subtotal AS [Subtotal ($)], 
                                        discount AS [Discount ($)], 
                                        tax AS [Tax ($)], 
                                        grand_total AS [Grand Total ($)], 
                                        total_profit AS [Profit ($)], 
                                        status AS [Status] 
                                 FROM tblInvoice tblCustomer 
                                 WHERE (deleted = 0 OR deleted IS NULL) AND (status != 'Canceled' and status !='Draft')
                                 ORDER BY invoice_id DESC";

				DataTable dt = sharedClass.GetData(query);
				dgvInvoices.DataSource = dt;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error loading invoices: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		// --- 2. LIVE SEARCH FILTER ---
		private void txtSearchInvoiceNo_TextChanged(object sender, EventArgs e)
		{
			string keyword = txtSearchInvoiceNo.Text.Trim();
			try
			{
				string query = $@"SELECT invoice_id AS [ID], 
                                        invoice_no AS [Invoice No], 
                                        customer_name AS [Customer Name], 
                                        invoice_date AS [Invoice Date], 
                                        due_date AS [Due Date],
                                        subtotal AS [Subtotal ($)], 
                                        discount AS [Discount ($)], 
                                        tax AS [Tax ($)], 
                                        grand_total AS [Grand Total ($)], 
                                        total_profit AS [Profit ($)], 
                                        status AS [Status] 
                                 FROM tblInvoice tblCustomer 
                                 WHERE (deleted = 0 OR deleted IS NULL) 
                                   AND (status != 'Draft' and status !='Canceled')
                                   AND invoice_no LIKE '%{keyword}%'
                                 ORDER BY invoice_id DESC";

				DataTable dt = sharedClass.GetData(query);
				dgvInvoices.DataSource = dt;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error searching invoices: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			txtSearchInvoiceNo.Clear();
			LoadInvoices();
		}

		// --- 3. VIEW ITEMS FOR SELECTED INVOICE ---
		private void btnViewItems_Click(object sender, EventArgs e)
		{
			if (dgvInvoices.SelectedRows.Count > 0)
			{
				string invoiceId = dgvInvoices.SelectedRows[0].Cells["ID"].Value.ToString();
				string invoiceNo = dgvInvoices.SelectedRows[0].Cells["Invoice No"].Value.ToString();

				try
				{
					string query = $"SELECT product_name AS [Product], quantity AS [Qty], unit_price AS [Price], total_price AS [Total] FROM tblSale WHERE invoice_id = {invoiceId}";
					DataTable dtItems = sharedClass.GetData(query);

					string summary = $"Items Breakdown for Invoice: {invoiceNo}\n\n";
					foreach (DataRow r in dtItems.Rows)
					{
						summary += $"- {r["Product"]} (x{r["Qty"]}) @ ${Convert.ToDecimal(r["Price"]):N2} = ${Convert.ToDecimal(r["Total"]):N2}\n";
					}
					MessageBox.Show(summary, "Invoice Items", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error loading invoice items: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				MessageBox.Show("Please select an invoice row to view items.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		// --- 4. RE-PRINT SELECTED INVOICE FROM HISTORY ---
		private void btnPrintInvoice_Click(object sender, EventArgs e)
		{
			if (dgvInvoices.SelectedRows.Count > 0)
			{
				selectedInvoiceNoForPrint = dgvInvoices.SelectedRows[0].Cells["Invoice No"].Value.ToString();

				try
				{
					PrintDialog printDialog = new PrintDialog();
					printDialog.Document = printDocument1;

					if (printDialog.ShowDialog() == DialogResult.OK)
					{
						printPreviewDialog1.Document = printDocument1;
						if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
						{
							printDocument1.Print();
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Printing error: " + ex.Message, "Printer Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			else
			{
				MessageBox.Show("Please select an invoice row to print.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		// --- 5. PRINT PAGE LAYOUT USING tblInvoice COLUMNS ---
		// --- KEY LUXURY: DATABASE-DRIVEN BLACK & GOLD PRINT LAYOUT ---
		private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
		{
			if (string.IsNullOrEmpty(selectedInvoiceNoForPrint)) return;

			Graphics g = e.Graphics;

			// Enable high-quality rendering
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

			// 1. DYNAMICALLY READ SELECTED PRINTER PAGE DIMENSIONS & PRINTER USER
			int pageWidth = e.PageBounds.Width;
			int pageHeight = e.PageBounds.Height;
			string printerUser = e.PageSettings.PrinterSettings.PrinterName; // Captures active printer/user name

			// Proportional outer boundaries
			int startX = (int)(pageWidth * 0.06f);
			int startY = (int)(pageWidth * 0.06f);
			int printableWidth = pageWidth - (startX * 2);

			// Fluid scale factor based on active page width
			float scale = (float)printableWidth / 550f;
			if (scale < 0.65f) scale = 0.65f;

			// Define Color Palette (Black & Rich Gold Luxury Theme)
			using (Brush darkBlackBrush = new SolidBrush(Color.FromArgb(22, 22, 22)))       // Solid Black Sidebar & Footer
			using (Brush goldBrush = new SolidBrush(Color.FromArgb(197, 160, 89)))        // Rich Gold Headers & Accents
			using (Brush darkTextBrush = new SolidBrush(Color.FromArgb(40, 40, 40)))     // Dark Charcoal Text
			using (Brush lightGrayBrush = new SolidBrush(Color.FromArgb(250, 248, 242))) // Soft Cream/Ivory Alternating Row Shading
			using (Brush whiteBrush = new SolidBrush(Color.White))
			using (Pen goldPen = new Pen(Color.FromArgb(197, 160, 89), 1))
			using (Pen darkPen = new Pen(Color.FromArgb(80, 80, 80), 1))

			// Define Fluid Scaled Fonts
			using (Font invoiceLabelFont = new Font("Segoe UI", 16f * scale, FontStyle.Bold))
			using (Font brandFont = new Font("Segoe UI", 12f * scale, FontStyle.Bold))
			using (Font boldFont = new Font("Segoe UI", 8.5f * scale, FontStyle.Bold))
			using (Font regularFont = new Font("Segoe UI", 8.5f * scale, FontStyle.Regular))
			using (Font smallFont = new Font("Segoe UI", 7.5f * scale, FontStyle.Regular))
			{
				// 2. FETCH RECORD MATCHING tblInvoice SCHEMA COLUMNS
				string invoiceQuery = $@"SELECT invoice_id, invoice_no, subtotal, discount, tax, grand_total, invoice_date, customer_name 
                                   FROM tblInvoice WHERE invoice_no = '{selectedInvoiceNoForPrint}'";
				DataTable dtInvoice = sharedClass.GetData(invoiceQuery);

				if (dtInvoice.Rows.Count == 0) return;
				DataRow invRow = dtInvoice.Rows[0];

				string customerName = invRow["customer_name"] != DBNull.Value ? invRow["customer_name"].ToString() : "Walk-in Customer";
				string invoiceDate = invRow["invoice_date"] != DBNull.Value ? Convert.ToDateTime(invRow["invoice_date"]).ToString("dd-MM-yyyy HH:mm") : "";
				decimal subtotal = invRow["subtotal"] != DBNull.Value ? Convert.ToDecimal(invRow["subtotal"]) : 0;
				decimal discount = invRow["discount"] != DBNull.Value ? Convert.ToDecimal(invRow["discount"]) : 0;
				decimal tax = invRow["tax"] != DBNull.Value ? Convert.ToDecimal(invRow["tax"]) : 0;
				decimal grandTotal = invRow["grand_total"] != DBNull.Value ? Convert.ToDecimal(invRow["grand_total"]) : 0;
				string invoiceId = invRow["invoice_id"].ToString();

				int offset = 0;

				// --- 3. TOP HEADER SECTION (LOGO PLACEHOLDER & INVOICE META) ---
				int topHeaderY = startY + offset;

				// Logo Placeholder Box (Top Left)
				int logoBoxWidth = (int)(150 * scale);
				int logoBoxHeight = (int)(45 * scale);
				g.DrawRectangle(goldPen, startX, topHeaderY, logoBoxWidth, logoBoxHeight);
				string logoPlaceholderText = "[ KEY LUXURY LOGO ]";
				SizeF logoTextSize = g.MeasureString(logoPlaceholderText, smallFont);
				g.DrawString(logoPlaceholderText, smallFont, goldBrush, startX + (logoBoxWidth - logoTextSize.Width) / 2, topHeaderY + (logoBoxHeight - logoTextSize.Height) / 2);

				// Right-side INVOICE Title & Invoice No
				g.DrawString("INVOICE", invoiceLabelFont, goldBrush, startX + (int)(printableWidth * 0.58f), topHeaderY);
				g.DrawString($"No. {selectedInvoiceNoForPrint}", boldFont, darkTextBrush, startX + (int)(printableWidth * 0.58f), topHeaderY + (int)(28 * scale));

				offset += (int)(60 * scale);

				// --- 4. BILL TO & METADATA SECTION ---
				int metaY = startY + offset;
				g.DrawString("INVOICE TO:", boldFont, goldBrush, startX, metaY);

				string customerDetails = $"{customerName}\nKey Luxury Store Client\nPrinter User: {printerUser}";
				g.DrawString(customerDetails, regularFont, darkTextBrush, startX, metaY + (int)(16 * scale));

				// Right side Date info
				int metaRightX = startX + (int)(printableWidth * 0.58f);
				g.DrawString($"Invoice Date: {invoiceDate}", regularFont, darkTextBrush, metaRightX, metaY + (int)(16 * scale));

				offset += (int)(80 * scale);

				// --- 5. TABLE HEADERS (Rich Gold Bar) ---
				int tableHeaderHeight = (int)(26 * scale);
				g.FillRectangle(goldBrush, startX, startY + offset, printableWidth, tableHeaderHeight);

				int colNoX = startX + (int)(10 * scale);
				int colDescX = startX + (int)(45 * scale);
				int colPriceX = startX + (int)(printableWidth * 0.50f);
				int colUnitX = startX + (int)(printableWidth * 0.70f);
				int colTotalX = startX + printableWidth - (int)(15 * scale);

				StringFormat rightFormat = new StringFormat { Alignment = StringAlignment.Far };
				StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center };

				g.DrawString("No.", boldFont, whiteBrush, colNoX, startY + offset + (int)(5 * scale));
				g.DrawString("Descriptions", boldFont, whiteBrush, colDescX, startY + offset + (int)(5 * scale));
				g.DrawString("Unit Price", boldFont, whiteBrush, colPriceX, startY + offset + (int)(5 * scale), rightFormat);
				g.DrawString("Unit", boldFont, whiteBrush, colUnitX, startY + offset + (int)(5 * scale), centerFormat);
				g.DrawString("Price", boldFont, whiteBrush, colTotalX, startY + offset + (int)(5 * scale), rightFormat);

				offset += tableHeaderHeight;

				// --- 6. FETCH ITEMIZED PRODUCTS FROM tblSale & DRAW ROWS ---
				string itemsQuery = $"SELECT product_name, unit_price, quantity, total_price FROM tblSale WHERE invoice_id = {invoiceId}";
				DataTable dtItems = sharedClass.GetData(itemsQuery);

				int rowHeight = (int)(26 * scale);
				int rowCounter = 1;

				foreach (DataRow row in dtItems.Rows)
				{
					string pName = row["product_name"].ToString();
					decimal priceVal = Convert.ToDecimal(row["unit_price"]);
					int qtyVal = Convert.ToInt32(row["quantity"]);
					decimal lineTotal = Convert.ToDecimal(row["total_price"]);

					if (rowCounter % 2 == 0)
					{
						g.FillRectangle(lightGrayBrush, startX, startY + offset, printableWidth, rowHeight);
					}
					g.DrawRectangle(goldPen, startX, startY + offset, printableWidth, rowHeight);

					g.DrawString(rowCounter.ToString("D2"), regularFont, darkTextBrush, colNoX, startY + offset + (int)(5 * scale));
					g.DrawString(pName, regularFont, darkTextBrush, colDescX, startY + offset + (int)(5 * scale));
					g.DrawString(priceVal.ToString("N2"), regularFont, darkTextBrush, colPriceX, startY + offset + (int)(5 * scale), rightFormat);
					g.DrawString(qtyVal.ToString("D2"), regularFont, darkTextBrush, colUnitX, startY + offset + (int)(5 * scale), centerFormat);
					g.DrawString(lineTotal.ToString("N2"), regularFont, darkTextBrush, colTotalX, startY + offset + (int)(5 * scale), rightFormat);

					offset += rowHeight;
					rowCounter++;
				}

				offset += (int)(10 * scale);

				// --- 7. SUB-TOTAL AS A RICH GOLD BACKGROUND BANNER ---
				int subtotalBarHeight = (int)(30 * scale);
				g.FillRectangle(goldBrush, startX, startY + offset, printableWidth, subtotalBarHeight);

				g.DrawString("Sub Total:", boldFont, whiteBrush, startX + (int)(10 * scale), startY + offset + (int)(7 * scale));
				g.DrawString($"${subtotal:N2}", boldFont, whiteBrush, startX + printableWidth - (int)(15 * scale), startY + offset + (int)(7 * scale), rightFormat);

				offset += subtotalBarHeight + (int)(15 * scale);

				// --- 8. LOWER SECTION: PAYMENT SIDEBAR & CALCULATION TABLE ---
				int lowerSectionY = startY + offset;
				int sidebarWidth = (int)(printableWidth * 0.40f);
				int sidebarHeight = (int)(125 * scale);

				// Solid Black Left Sidebar Column (Thank You & Payment Info)
				g.FillRectangle(darkBlackBrush, startX, lowerSectionY, sidebarWidth, sidebarHeight);

				g.DrawString("Thank you for your business", boldFont, whiteBrush, startX + (int)(10 * scale), lowerSectionY + (int)(10 * scale));
				string paymentInfoText = "Payment Info:\nMethod: Cash\nStore: Key Luxury\nStatus: Paid";
				g.DrawString(paymentInfoText, smallFont, whiteBrush, startX + (int)(10 * scale), lowerSectionY + (int)(32 * scale));

				// Conditional Discount Check
				bool hasDiscount = discount > 0;

				// --- 9. TAX, DISCOUNT & GRAND TOTAL TABLE ---
				int tableX = startX + (int)(printableWidth * 0.48f);
				int tableWidth = printableWidth - (tableX - startX);
				int tableRowHeight = (int)(22 * scale);
				int currentTableY = lowerSectionY;

				int totalRowsCount = hasDiscount ? 3 : 2; // GST Tax, optional Discount, and Grand Total
				int summaryTableHeight = totalRowsCount * tableRowHeight;

				g.DrawRectangle(goldPen, tableX, currentTableY, tableWidth, summaryTableHeight);

				void DrawSummaryTableCell(string label, string val, int rowIndex, bool isBold = false)
				{
					int rowY = currentTableY + (rowIndex * tableRowHeight);
					if (rowIndex > 0)
					{
						g.DrawLine(goldPen, tableX, rowY, tableX + tableWidth, rowY);
					}

					Font fontToUse = isBold ? boldFont : regularFont;
					Brush brushToUse = isBold ? goldBrush : darkTextBrush;

					g.DrawString(label, fontToUse, darkTextBrush, tableX + (int)(8 * scale), rowY + (int)(4 * scale));
					g.DrawString(val, fontToUse, brushToUse, tableX + tableWidth - (int)(8 * scale), rowY + (int)(4 * scale), rightFormat);
				}

				int rIdx = 0;
				DrawSummaryTableCell("GST Tax (2.5%):", $"${tax:N2}", rIdx++);

				if (hasDiscount)
				{
					DrawSummaryTableCell("Discount:", $"-${discount:N2}", rIdx++);
				}

				DrawSummaryTableCell("GRAND TOTAL:", $"${grandTotal:N2}", rIdx, isBold: true);

				// --- 10. AUTHORISED SIGNATURE LINE ---
				int signWidth = (int)(180 * scale);
				int signX = startX + printableWidth - signWidth;
				int signY = lowerSectionY + sidebarHeight - (int)(35 * scale);

				g.DrawLine(darkPen, signX, signY, startX + printableWidth, signY);
				g.DrawString("Authorised Signature\nKey Luxury Store", smallFont, darkTextBrush, signX, signY + (int)(4 * scale));

				offset += sidebarHeight + (int)(15 * scale);

				// --- 11. BOTTOM FOOTER BAR WITH CONTACT DETAILS ---
				int footerY = startY + offset;
				int footerHeight = (int)(35 * scale);

				g.FillRectangle(darkBlackBrush, startX, footerY, printableWidth, footerHeight);
				g.DrawString("KEY LUXURY", brandFont, whiteBrush, startX + (int)(10 * scale), footerY + (int)(8 * scale));

				string contactFooterText = "+1 (800) KEY-LUXURY     support@keyluxury.com     Official Sales Receipt";
				g.DrawString(contactFooterText, smallFont, whiteBrush, startX + (int)(130 * scale), footerY + (int)(11 * scale));
			}
		}

		// --- 6. SOFT DELETE INVOICE (deleted = 1) ---
		private void btnDeleteInvoice_Click(object sender, EventArgs e)
		{
			if (dgvInvoices.SelectedRows.Count > 0)
			{
				string invoiceId = dgvInvoices.SelectedRows[0].Cells["ID"].Value.ToString();
				string invoiceNo = dgvInvoices.SelectedRows[0].Cells["Invoice No"].Value.ToString();

				DialogResult result = MessageBox.Show($"Are you sure you want to delete invoice {invoiceNo}?",
					"Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (result == DialogResult.Yes)
				{
					try
					{
						// 1. Restore stock quantities for all items linked to this invoice
						string restoreStock = @"UPDATE p 
                            SET p.quantity = p.quantity + s.quantity 
                            FROM tblProduct p 
                            INNER JOIN tblSale s ON p.product_id = s.product_id 
                            WHERE s.invoice_id = " + invoiceId;
						db.ExecuteQueries(restoreStock);

						// 2. Mark payment records as deleted (deleted = 1) instead of removing them
						string softDeletePayment = $"UPDATE tblPayment SET deleted = 1 WHERE invoice_id = {invoiceId}";
						db.ExecuteQueries(softDeletePayment);

						// 3. Mark sale items as deleted (deleted = 1) instead of removing them
						string softDeleteSales = $"UPDATE tblSale SET deleted = 1 WHERE invoice_id = {invoiceId}";
						db.ExecuteQueries(softDeleteSales);

						// 4. Reset/Clear the Invoice record back to unpaid/default state and mark it cancelled/deleted if applicable
						string resetInvoice = $"UPDATE tblInvoice SET " +
											  $"deleted = 1 " +
											  $"WHERE invoice_id = {invoiceId}";
						db.ExecuteQueries(resetInvoice);

						// 4. Reset/Clear the Invoice record back to unpaid/default state and mark it cancelled/deleted if applicable
						string resetGSTExpense = $"UPDATE tblExpenses SET " +
											  $"deleted = 1 " +
											  $"WHERE invoice_id = {invoiceId}";
						db.ExecuteQueries(resetGSTExpense);

						MessageBox.Show("Transaction successfully undone and marked as deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					catch (Exception ex)
					{
						MessageBox.Show("Error undoing transaction: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
			else
			{
				MessageBox.Show("Please select an invoice row to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}