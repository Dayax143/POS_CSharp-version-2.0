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
		// --- KEY LUXURY: DATABASE-DRIVEN BLACK & GOLD REPRINT LAYOUT ---
		private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
		{
			if (string.IsNullOrEmpty(selectedInvoiceNoForPrint)) return;

			// ===== 1. FETCH DATA FIRST (so nothing is drawn if the invoice is missing) =====
			string safeInvoiceNo = selectedInvoiceNoForPrint.Replace("'", "''");
			string invoiceQuery = $@"SELECT invoice_id, invoice_no, subtotal, discount, tax, grand_total, invoice_date, customer_name
                             FROM tblInvoice WHERE invoice_no = '{safeInvoiceNo}'";
			DataTable dtInvoice = sharedClass.GetData(invoiceQuery);
			if (dtInvoice.Rows.Count == 0) return;
			DataRow invRow = dtInvoice.Rows[0];

			string customerName = invRow["customer_name"] != DBNull.Value ? invRow["customer_name"].ToString() : "Walk-in Customer";
			string invoiceDate = invRow["invoice_date"] != DBNull.Value ? Convert.ToDateTime(invRow["invoice_date"]).ToString("dd-MM-yyyy") : "";
			decimal subtotal = invRow["subtotal"] != DBNull.Value ? Convert.ToDecimal(invRow["subtotal"]) : 0;
			decimal discount = invRow["discount"] != DBNull.Value ? Convert.ToDecimal(invRow["discount"]) : 0;
			decimal tax = invRow["tax"] != DBNull.Value ? Convert.ToDecimal(invRow["tax"]) : 0;
			decimal grandTotal = invRow["grand_total"] != DBNull.Value ? Convert.ToDecimal(invRow["grand_total"]) : 0;
			string invoiceId = invRow["invoice_id"].ToString();

			string itemsQuery = $"SELECT product_name, unit_price, quantity, total_price FROM tblSale WHERE invoice_id = {invoiceId}";
			DataTable dtItems = sharedClass.GetData(itemsQuery);

			// Customer phone from tblCustomer (matched by invoice_id)
			// NOTE: change "phone" below if your column has a different name
			string customerPhone = "";
			try
			{
				DataTable dtCust = sharedClass.GetData($"SELECT customer_phone FROM tblCustomer WHERE customer_name = '{customerName}'");
				if (dtCust.Rows.Count > 0 && dtCust.Rows[0]["customer_phone"] != DBNull.Value)
					customerPhone = dtCust.Rows[0]["customer_phone"].ToString();
			}
			catch { }

			// Payment method from tblPayment (matched by invoice_id); joins multiple payments like "Cash, Zaad"
			// NOTE: change "payment_method" below if your column has a different name
			string paymentMethod = "";
			try
			{
				DataTable dtPay = sharedClass.GetData($"SELECT payment_method FROM tblPayment WHERE invoice_id = {invoiceId}");
				foreach (DataRow pr in dtPay.Rows)
				{
					if (pr["payment_method"] == DBNull.Value) continue;
					string m = pr["payment_method"].ToString().Trim();
					if (m.Length == 0 || paymentMethod.Contains(m)) continue;
					paymentMethod += (paymentMethod.Length > 0 ? ", " : "") + m;
				}
			}
			catch { }

			// ===== 2. PAGE SETUP =====
			Graphics g = e.Graphics;
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

			int pageWidth = e.PageBounds.Width;
			int pageHeight = e.PageBounds.Height;

			int startX = (int)(pageWidth * 0.07f);
			int startY = (int)(pageWidth * 0.07f);
			int printableWidth = pageWidth - (startX * 2);

			float scale = (float)printableWidth / 550f;
			if (scale < 0.65f) scale = 0.65f;

			Color gold = Color.FromArgb(197, 160, 89);
			Color black = Color.FromArgb(22, 22, 22);

			using (Brush darkBlackBrush = new SolidBrush(black))
			using (Brush goldBrush = new SolidBrush(gold))
			using (Brush darkTextBrush = new SolidBrush(Color.FromArgb(40, 40, 40)))
			using (Brush lightGrayBrush = new SolidBrush(Color.FromArgb(250, 245, 232)))
			using (Brush whiteBrush = new SolidBrush(Color.White))
			using (Pen goldPen = new Pen(gold, 1))
			using (Pen thinGoldPen = new Pen(Color.FromArgb(120, gold), 0.5f))
			using (Pen darkPen = new Pen(Color.FromArgb(80, 80, 80), 1))
			using (Font invoiceLabelFont = new Font("Segoe UI", 16f * scale, FontStyle.Bold))
			using (Font logoFont = new Font("Georgia", 20f * scale, FontStyle.Bold))
			using (Font boldFont = new Font("Segoe UI", 8.5f * scale, FontStyle.Bold))
			using (Font regularFont = new Font("Segoe UI", 8.5f * scale, FontStyle.Regular))
			using (Font smallFont = new Font("Segoe UI", 7.5f * scale, FontStyle.Regular))
			using (Font stampFont = new Font("Segoe UI", 18f * scale, FontStyle.Bold))
			using (Font grandFont = new Font("Segoe UI", 20f * scale, FontStyle.Bold))
			using (StringFormat rightFormat = new StringFormat { Alignment = StringAlignment.Far })
			using (StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center })
			{
				int centerX = pageWidth / 2;

				// ===== PAGE BORDER (double gold line + corner diamonds) =====
				int m1 = (int)(startX * 0.45f);
				int m2 = m1 + (int)(5 * scale);
				g.DrawRectangle(goldPen, m1, m1, pageWidth - m1 * 2, pageHeight - m1 * 2);
				g.DrawRectangle(thinGoldPen, m2, m2, pageWidth - m2 * 2, pageHeight - m2 * 2);
				int d = (int)(5 * scale);
				foreach (var p in new[] {
			new Point(m1, m1), new Point(pageWidth - m1, m1),
			new Point(m1, pageHeight - m1), new Point(pageWidth - m1, pageHeight - m1) })
				{
					g.FillPolygon(goldBrush, new[] {
				new Point(p.X, p.Y - d), new Point(p.X + d, p.Y),
				new Point(p.X, p.Y + d), new Point(p.X - d, p.Y) });
				}

				int offset = 0;

				// ===== LOGO (TOP CENTER) =====
				int logoTop = startY + offset;

				Image logoImg = null;
				try { logoImg = Properties.Resources.key_luxery_logo; } catch { }
				if (logoImg == null)
				{
					string logoPath = System.IO.Path.Combine(Application.StartupPath, "key_luxery_logo.png");
					if (System.IO.File.Exists(logoPath)) logoImg = Image.FromFile(logoPath);
				}

				if (logoImg != null)
				{
					g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
					g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

					// Crop the empty white margin at the top and bottom of the PNG
					int srcY = (int)(logoImg.Height * 0.04f);
					int srcH = (int)(logoImg.Height * 0.88f);

					int logoH = (int)(115 * scale);
					int logoW = (int)(logoH * ((float)logoImg.Width / srcH));
					Rectangle dest = new Rectangle(centerX - logoW / 2, logoTop, logoW, logoH);
					g.DrawImage(logoImg, dest, 0, srcY, logoImg.Width, srcH, GraphicsUnit.Pixel);
					logoImg.Dispose();
					offset += logoH + (int)(6 * scale);
				}
				else
				{
					DrawSpaced(g, "KEY LUXERY", logoFont, goldBrush, centerX, logoTop, 4f * scale);
					offset += (int)(34 * scale);
				}

				// ===== INVOICE BAND =====
				int bandH = (int)(32 * scale);
				g.FillRectangle(darkBlackBrush, startX, startY + offset, printableWidth, bandH);
				g.FillRectangle(goldBrush, startX, startY + offset, (int)(6 * scale), bandH);
				g.DrawString("INVOICE", invoiceLabelFont, goldBrush, startX + (int)(16 * scale), startY + offset + (int)(3 * scale));
				g.DrawString($"No. {selectedInvoiceNoForPrint}", boldFont, whiteBrush, startX + printableWidth - (int)(12 * scale), startY + offset + (int)(9 * scale), rightFormat);
				offset += bandH + (int)(10 * scale);

				// ===== CUSTOMER DETAILS & DATE =====
				int metaY = startY + offset;
				int labelW = (int)(60 * scale);
				int halfW = (int)(printableWidth * 0.5f);

				// Left: invoice to
				g.DrawString("INVOICE TO", boldFont, goldBrush, startX, metaY);
				int lineY = metaY + (int)(17 * scale);
				g.DrawLine(goldPen, startX, lineY, startX + halfW - (int)(15 * scale), lineY);

				int row1 = lineY + (int)(5 * scale);
				g.DrawString("Customer:", boldFont, darkTextBrush, startX, row1);
				g.DrawString(customerName, regularFont, darkTextBrush, startX + labelW, row1);

				int row2 = row1 + (int)(18 * scale);
				g.DrawString("Phone:", boldFont, darkTextBrush, startX, row2);
				g.DrawString(string.IsNullOrWhiteSpace(customerPhone) ? "-" : customerPhone, regularFont, darkTextBrush, startX + labelW, row2);

				// Right: date
				int rightX = startX + printableWidth;
				g.DrawString("INVOICE DATE", boldFont, goldBrush, rightX, metaY, rightFormat);
				g.DrawLine(goldPen, rightX - (int)(120 * scale), lineY, rightX, lineY);
				g.DrawString(invoiceDate, regularFont, darkTextBrush, rightX, row1, rightFormat);

				offset += (int)(72 * scale);

				// ===== WATERMARK KEY (behind the table) =====
				DrawKeyIcon(g, centerX, startY + offset + 90 * scale, 170f * scale, Color.FromArgb(18, gold));

				// ===== TABLE HEADER =====
				int tableHeaderHeight = (int)(26 * scale);
				g.FillRectangle(goldBrush, startX, startY + offset, printableWidth, tableHeaderHeight);

				int colNoX = startX + (int)(10 * scale);
				int colDescX = startX + (int)(45 * scale);
				int colPriceX = startX + (int)(printableWidth * 0.50f);
				int colUnitX = startX + (int)(printableWidth * 0.70f);
				int colTotalX = startX + printableWidth - (int)(15 * scale);
				int textPad = (int)(5 * scale);

				g.DrawString("No.", boldFont, whiteBrush, colNoX, startY + offset + textPad);
				g.DrawString("Descriptions", boldFont, whiteBrush, colDescX, startY + offset + textPad);
				g.DrawString("Unit Price", boldFont, whiteBrush, colPriceX, startY + offset + textPad, rightFormat);
				g.DrawString("Unit", boldFont, whiteBrush, colUnitX, startY + offset + textPad, centerFormat);
				g.DrawString("Price", boldFont, whiteBrush, colTotalX, startY + offset + textPad, rightFormat);

				offset += tableHeaderHeight;

				// ===== TABLE ROWS (from tblSale) =====
				int rowHeight = (int)(26 * scale);
				int rowCounter = 1;

				foreach (DataRow row in dtItems.Rows)
				{
					string pName = row["product_name"].ToString();
					decimal priceVal = Convert.ToDecimal(row["unit_price"]);
					int qtyVal = Convert.ToInt32(row["quantity"]);
					decimal lineTotal = Convert.ToDecimal(row["total_price"]);

					int rowY = startY + offset;
					if (rowCounter % 2 == 0)
						g.FillRectangle(lightGrayBrush, startX, rowY, printableWidth, rowHeight);

					// light gold line under each row + side accent
					g.DrawLine(thinGoldPen, startX, rowY + rowHeight, startX + printableWidth, rowY + rowHeight);
					g.FillRectangle(goldBrush, startX, rowY, (int)(3 * scale), rowHeight);

					g.DrawString(rowCounter.ToString("D2"), regularFont, darkTextBrush, colNoX, rowY + textPad);
					g.DrawString(pName, regularFont, darkTextBrush, colDescX, rowY + textPad);
					g.DrawString(priceVal.ToString("N2"), regularFont, darkTextBrush, colPriceX, rowY + textPad, rightFormat);
					g.DrawString(qtyVal.ToString("D2"), regularFont, darkTextBrush, colUnitX, rowY + textPad, centerFormat);
					g.DrawString(lineTotal.ToString("N2"), regularFont, darkTextBrush, colTotalX, rowY + textPad, rightFormat);

					offset += rowHeight;
					rowCounter++;
				}

				offset += (int)(12 * scale);   // small gap after the last row

				// ===== LOWER SECTION: PAYMENT INFO (left) + TOTALS TABLE (right) =====
				int blockY = startY + offset;
				int sidebarWidth = (int)(printableWidth * 0.38f);

				int tableW = (int)(printableWidth * 0.48f);
				int tableX = startX + printableWidth - tableW;
				int smallRowH = (int)(22 * scale);
				int grandH = (int)(44 * scale);
				int pad = (int)(10 * scale);

				// Values come straight from tblInvoice (not recalculated)
				int ty = blockY;
				ty = DrawTotalRow(g, "Sub Total", $"${subtotal:N2}", tableX, ty, tableW, smallRowH, regularFont, darkTextBrush, goldPen, lightGrayBrush, pad);
				ty = DrawTotalRow(g, "GST Tax (2.5%)", $"${tax:N2}", tableX, ty, tableW, smallRowH, regularFont, darkTextBrush, goldPen, null, pad);
				ty = DrawTotalRow(g, "Discount", (discount > 0 ? "-" : "") + $"${discount:N2}", tableX, ty, tableW, smallRowH, regularFont, darkTextBrush, goldPen, lightGrayBrush, pad);

				// Grand total (bigger, black bar with gold text)
				g.FillRectangle(darkBlackBrush, tableX, ty, tableW, grandH);
				g.FillRectangle(goldBrush, tableX, ty, tableW, (int)(3 * scale));
				using (StringFormat midLeft = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center })
				using (StringFormat midRight = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center })
				{
					RectangleF gr = new RectangleF(tableX + pad, ty + (int)(3 * scale), tableW - pad * 2, grandH - (int)(3 * scale));
					g.DrawString("GRAND TOTAL", boldFont, whiteBrush, gr, midLeft);
					g.DrawString($"${grandTotal:N2}", grandFont, goldBrush, gr, midRight);
				}
				ty += grandH;

				// Left black sidebar, same height as the totals table
				int blockH = Math.Max(ty - blockY, (int)(95 * scale));
				g.FillRectangle(darkBlackBrush, startX, blockY, sidebarWidth, blockH);
				g.FillRectangle(goldBrush, startX, blockY, sidebarWidth, (int)(3 * scale));
				g.DrawString("Thank you for your payment", boldFont, goldBrush, startX + pad, blockY + (int)(9 * scale));
				string paymentInfoText = $"Payment Info:\nMethod: {(string.IsNullOrWhiteSpace(paymentMethod) ? "-" : paymentMethod)}\nStatus: Paid\nUser: {Properties.Settings.Default.username}";
				g.DrawString(paymentInfoText, smallFont, whiteBrush, startX + pad, blockY + (int)(30 * scale));

				// PAID stamp (between sidebar and totals table)
				var state = g.Save();
				g.TranslateTransform(startX + sidebarWidth + (tableX - (startX + sidebarWidth)) / 2f, blockY + blockH / 2f);
				g.RotateTransform(-14);
				using (Pen stampPen = new Pen(Color.FromArgb(150, gold), 2f * scale))
				using (Brush stampBrush = new SolidBrush(Color.FromArgb(150, gold)))
				{
					SizeF sz = g.MeasureString("PAID", stampFont);
					g.DrawRectangle(stampPen, -sz.Width / 2 - 4, -sz.Height / 2 - 2, sz.Width + 8, sz.Height + 4);
					g.DrawString("PAID", stampFont, stampBrush, -sz.Width / 2, -sz.Height / 2);
				}
				g.Restore(state);

				// Signature (right, under the totals table)
				int signWidth = (int)(150 * scale);
				int signX = startX + printableWidth - signWidth;
				int signY = blockY + blockH + (int)(28 * scale);
				g.DrawLine(darkPen, signX, signY, startX + printableWidth, signY);
				g.DrawString("Manager signature", smallFont, darkTextBrush, signX, signY + (int)(3 * scale));

				offset = (signY - startY) + (int)(24 * scale);

				// ===== FOOTER (ANCHORED TO PAGE BOTTOM) =====
				int footerHeight = (int)(38 * scale);
				int footerY = pageHeight - startY - footerHeight;

				// Safety: if a long invoice reaches the footer area, push the footer below the content
				int contentBottom = startY + offset;
				if (footerY < contentBottom) footerY = contentBottom;

				g.FillRectangle(darkBlackBrush, startX, footerY, printableWidth, footerHeight);
				g.FillRectangle(goldBrush, startX, footerY, printableWidth, (int)(3 * scale));

				string contactFooterText = "Tel: +01234567890   |   Email: keyluxery@gmail.com   |   Jigjigayar, Road NO.1, Hargeisa, Somaliland";

				float padX = 10f * scale;
				RectangleF footerTextRect = new RectangleF(
					startX + padX,
					footerY + (int)(3 * scale),
					printableWidth - padX * 2,
					footerHeight - (int)(3 * scale));

				// Shrink the font until the text fits on one line
				float fontSize = 7.5f * scale;
				Font footerFont = new Font("Segoe UI", fontSize, FontStyle.Regular);
				while (g.MeasureString(contactFooterText, footerFont).Width > footerTextRect.Width && fontSize > 4f)
				{
					footerFont.Dispose();
					fontSize -= 0.25f;
					footerFont = new Font("Segoe UI", fontSize, FontStyle.Regular);
				}

				using (footerFont)
				using (StringFormat footerFormat = new StringFormat
				{
					Alignment = StringAlignment.Center,
					LineAlignment = StringAlignment.Center,
					FormatFlags = StringFormatFlags.NoWrap
				})
				{
					g.DrawString(contactFooterText, footerFont, whiteBrush, footerTextRect, footerFormat);
				}
			}
		}

		// ---------- HELPERS (skip any that already exist in this class) ----------

		// Draws a gold key rotated 45° centered at (cx, cy)
		private void DrawKeyIcon(Graphics g, float cx, float cy, float size, Color color)
		{
			float r = size / 2f;
			var state = g.Save();
			g.TranslateTransform(cx, cy);
			g.RotateTransform(-45);
			using (Pen ringPen = new Pen(color, r * 0.13f))
			using (Pen shaftPen = new Pen(color, r * 0.10f) { StartCap = System.Drawing.Drawing2D.LineCap.Round, EndCap = System.Drawing.Drawing2D.LineCap.Round })
			using (Brush b = new SolidBrush(color))
			{
				g.DrawEllipse(ringPen, -r * 0.85f, -r * 0.28f, r * 0.56f, r * 0.56f); // bow
				g.DrawLine(shaftPen, -r * 0.29f, 0, r * 0.85f, 0);                      // shaft
				g.FillRectangle(b, r * 0.50f, 0, r * 0.12f, r * 0.30f);                 // tooth 1
				g.FillRectangle(b, r * 0.70f, 0, r * 0.12f, r * 0.22f);                 // tooth 2
			}
			g.Restore(state);
		}

		// Draws centered text with extra letter spacing
		private void DrawSpaced(Graphics g, string text, Font font, Brush brush, float centerX, float y, float spacing)
		{
			var fmt = StringFormat.GenericTypographic;
			float total = 0;
			foreach (char c in text) total += g.MeasureString(c.ToString(), font, 1000, fmt).Width + spacing;
			total -= spacing;

			float x = centerX - total / 2f;
			foreach (char c in text)
			{
				g.DrawString(c.ToString(), font, brush, x, y, fmt);
				x += g.MeasureString(c.ToString(), font, 1000, fmt).Width + spacing;
			}
		}

		// Draws one boxed row of the totals table: label on the left, value on the right. Returns the next row's Y.
		private int DrawTotalRow(Graphics g, string label, string value, int x, int y, int w, int h,
			Font font, Brush textBrush, Pen linePen, Brush shadeBrush, int pad)
		{
			if (shadeBrush != null) g.FillRectangle(shadeBrush, x, y, w, h);
			g.DrawRectangle(linePen, x, y, w, h);

			RectangleF r = new RectangleF(x + pad, y, w - pad * 2, h);
			using (StringFormat left = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center })
			using (StringFormat right = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center })
			{
				g.DrawString(label, font, textBrush, r, left);
				g.DrawString(value, font, textBrush, r, right);
			}
			return y + h;
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