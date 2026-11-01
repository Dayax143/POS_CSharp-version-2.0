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

		// ======================================================================
		// 4. RE-PRINT SELECTED INVOICE – MINI PAPER (THERMAL RECEIPT)
		// ======================================================================

		// Roll width: 80 = standard thermal roll, 58 = small roll
		private const float RECEIPT_PAPER_MM = 80f;

		// Logo pixels darker than this become black (0-255). Raise it if the logo prints too thin.
		private const int LOGO_THRESHOLD = 215;

		private class ReceiptData
		{
			public string InvoiceId, InvoiceNo, CustomerName, CustomerPhone, PaymentMethod, Status, User;
			public DateTime? InvoiceDate;
			public decimal Subtotal, Discount, Tax, GrandTotal;
			public DataTable Items;
		}

		private ReceiptData receipt;          // data of the invoice being printed
		private Bitmap receiptLogoCache;      // black & white logo, built once

		private void btnPrintInvoice_Click(object sender, EventArgs e)
		{
			if (dgvInvoices.SelectedRows.Count == 0)
			{
				MessageBox.Show("Please select an invoice row to print.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			selectedInvoiceNoForPrint = dgvInvoices.SelectedRows[0].Cells["Invoice No"].Value.ToString();

			try
			{
				receipt = LoadReceiptData(selectedInvoiceNoForPrint);
				if (receipt == null)
				{
					MessageBox.Show("Invoice not found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}

				PrintDialog printDialog = new PrintDialog();
				printDialog.Document = printDocument1;
				if (printDialog.ShowDialog() != DialogResult.OK) return;

				// Cut the paper to the exact receipt length (units = 1/100 inch)
				float paperW = RECEIPT_PAPER_MM / 25.4f * 100f;
				float paperH = MeasureReceiptHeight(receipt, paperW);
				printDocument1.DefaultPageSettings.PaperSize = new PaperSize("Receipt", (int)Math.Round(paperW), (int)Math.Ceiling(paperH));
				printDocument1.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);

				printPreviewDialog1.Document = printDocument1;
				if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
				{
					printDocument1.Print();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Printing error: " + ex.Message, "Printer Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
		{
			if (receipt == null)
			{
				if (string.IsNullOrEmpty(selectedInvoiceNoForPrint)) return;
				receipt = LoadReceiptData(selectedInvoiceNoForPrint);
				if (receipt == null) return;
			}

			DrawReceipt(e.Graphics, receipt, e.PageBounds.Width);
			e.HasMorePages = false;
		}

		// ---------- DATA ----------
		private ReceiptData LoadReceiptData(string invoiceNo)
		{
			string safeNo = invoiceNo.Replace("'", "''");
			DataTable dtInv = sharedClass.GetData($@"SELECT invoice_id, invoice_no, subtotal, discount, tax, grand_total, invoice_date, customer_name, status
                             FROM tblInvoice WHERE invoice_no = '{safeNo}'");
			if (dtInv == null || dtInv.Rows.Count == 0) return null;

			DataRow r = dtInv.Rows[0];
			ReceiptData d = new ReceiptData();
			d.InvoiceId = r["invoice_id"].ToString();
			d.InvoiceNo = invoiceNo;
			d.CustomerName = (r["customer_name"] != DBNull.Value && r["customer_name"].ToString().Trim().Length > 0)
				? r["customer_name"].ToString() : "Walk-in Customer";
			d.InvoiceDate = r["invoice_date"] != DBNull.Value ? Convert.ToDateTime(r["invoice_date"]) : (DateTime?)null;
			d.Subtotal = r["subtotal"] != DBNull.Value ? Convert.ToDecimal(r["subtotal"]) : 0;
			d.Discount = r["discount"] != DBNull.Value ? Convert.ToDecimal(r["discount"]) : 0;
			d.Tax = r["tax"] != DBNull.Value ? Convert.ToDecimal(r["tax"]) : 0;
			d.GrandTotal = r["grand_total"] != DBNull.Value ? Convert.ToDecimal(r["grand_total"]) : 0;
			d.Status = r["status"] != DBNull.Value ? r["status"].ToString() : "Paid";
			d.User = Properties.Settings.Default.username;

			d.Items = sharedClass.GetData($"SELECT product_name, unit_price, quantity, total_price FROM tblSale WHERE invoice_id = {d.InvoiceId}");

			d.CustomerPhone = "";
			try
			{
				string safeName = d.CustomerName.Replace("'", "''");
				DataTable dtCust = sharedClass.GetData($"SELECT customer_phone FROM tblCustomer WHERE customer_name = '{safeName}'");
				if (dtCust.Rows.Count > 0 && dtCust.Rows[0]["customer_phone"] != DBNull.Value)
					d.CustomerPhone = dtCust.Rows[0]["customer_phone"].ToString();
			}
			catch { }

			d.PaymentMethod = "";
			try
			{
				DataTable dtPay = sharedClass.GetData($"SELECT payment_method FROM tblPayment WHERE invoice_id = {d.InvoiceId}");
				foreach (DataRow pr in dtPay.Rows)
				{
					if (pr["payment_method"] == DBNull.Value) continue;
					string m = pr["payment_method"].ToString().Trim();
					if (m.Length == 0 || d.PaymentMethod.Contains(m)) continue;
					d.PaymentMethod += (d.PaymentMethod.Length > 0 ? ", " : "") + m;
				}
			}
			catch { }

			return d;
		}

		// Runs the drawing code on a dummy surface just to learn how tall the receipt is
		private float MeasureReceiptHeight(ReceiptData d, float paperW)
		{
			using (Bitmap bmp = new Bitmap(1, 1))
			using (Graphics mg = Graphics.FromImage(bmp))
			{
				mg.PageUnit = GraphicsUnit.Inch;   // 1 unit = 1/100 inch, same as the printer
				mg.PageScale = 0.01f;
				return DrawReceipt(mg, d, paperW);
			}
		}

		// Logo converted to pure black & white so it prints sharp on a thermal printer
		private Bitmap GetReceiptLogo()
		{
			if (receiptLogoCache != null) return receiptLogoCache;

			Image src = null;
			bool fromFile = false;
			try { src = Properties.Resources.key_luxery_logo; } catch { }
			if (src == null)
			{
				string p = System.IO.Path.Combine(Application.StartupPath, "key_luxery_logo.png");
				if (System.IO.File.Exists(p)) { src = Image.FromFile(p); fromFile = true; }
			}
			if (src == null) return null;

			try
			{
				// Crop the empty margin at the top/bottom of the PNG
				int srcY = (int)(src.Height * 0.04f);
				int srcH = (int)(src.Height * 0.88f);
				int w = 360;
				int h = Math.Max(1, (int)(w * (float)srcH / src.Width));

				Bitmap bmp = new Bitmap(w, h);
				using (Graphics gg = Graphics.FromImage(bmp))
				{
					gg.Clear(Color.White);
					gg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
					gg.DrawImage(src, new Rectangle(0, 0, w, h), 0, srcY, src.Width, srcH, GraphicsUnit.Pixel);
				}

				for (int py = 0; py < h; py++)
				{
					for (int px = 0; px < w; px++)
					{
						Color c = bmp.GetPixel(px, py);
						int lum = (c.R * 30 + c.G * 59 + c.B * 11) / 100;
						bmp.SetPixel(px, py, lum < LOGO_THRESHOLD ? Color.Black : Color.White);
					}
				}

				receiptLogoCache = bmp;
				return bmp;
			}
			finally
			{
				if (fromFile) src.Dispose();
			}
		}

		// ---------- THE RECEIPT ----------
		// Draws top-to-bottom and returns the final Y (= receipt height). Units: 1/100 inch.
		private float DrawReceipt(Graphics g, ReceiptData d, float paperW)
		{
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

			float W = Math.Min(paperW - 32f, 283f);   // printable width (72 mm on an 80 mm roll)
			float x0 = (paperW - W) / 2f;
			float x1 = x0 + W;
			float cx = paperW / 2f;
			float y = 12f;

			string status = string.IsNullOrWhiteSpace(d.Status) ? "Paid" : d.Status.Trim();
			string payMethod = string.IsNullOrWhiteSpace(d.PaymentMethod) ? "-" : d.PaymentMethod;

			using (Font brandFont = new Font("Georgia", 14f, FontStyle.Bold))
			using (Font totalFont = new Font("Consolas", 12f, FontStyle.Bold))
			using (Font mono = new Font("Consolas", 8f))
			using (Font monoB = new Font("Consolas", 8f, FontStyle.Bold))
			using (Font small = new Font("Consolas", 7f))
			using (Font stampFont = new Font("Consolas", 15f, FontStyle.Bold))
			using (Pen thin = new Pen(Color.Black, 0.8f))
			using (Pen thick = new Pen(Color.Black, 2f))
			using (Pen dashed = new Pen(Color.Black, 0.9f) { DashPattern = new float[] { 3f, 3f } })
			using (StringFormat right = new StringFormat { Alignment = StringAlignment.Far })
			using (StringFormat center = new StringFormat { Alignment = StringAlignment.Center })
			using (StringFormat wrap = new StringFormat())
			{
				// ----- small drawing helpers -----
				float LineH(Font f) => f.GetHeight(g);
				string Spaced(string s) => string.Join(" ", s.ToCharArray());

				void Center(string s, Font f)
				{
					float h = g.MeasureString(s, f, (int)W, center).Height;
					g.DrawString(s, f, Brushes.Black, new RectangleF(x0, y, W, h + 2), center);
					y += h;
				}

				float Wrapped(string s, Font f, float x, float width)
				{
					float h = g.MeasureString(s, f, (int)width, wrap).Height;
					g.DrawString(s, f, Brushes.Black, new RectangleF(x, y, width, h + 2), wrap);
					return h;
				}

				void Left(string s, Font f) { y += Wrapped(s, f, x0, W); }

				void LR(string l, string r, Font f)
				{
					g.DrawString(l, f, Brushes.Black, x0, y);
					g.DrawString(r, f, Brushes.Black, new RectangleF(x0, y, W, LineH(f) + 2), right);
					y += LineH(f);
				}

				void Rule(Pen p) { y += 4; g.DrawLine(p, x0, y, x1, y); y += 5; }

				void DoubleRule()
				{
					y += 4;
					g.DrawLine(thick, x0, y, x1, y);
					g.DrawLine(thin, x0, y + 4, x1, y + 4);
					y += 10;
				}

				void Diamond(float px, float py, float r)
				{
					g.FillPolygon(Brushes.Black, new[] {
						new PointF(px, py - r), new PointF(px + r, py),
						new PointF(px, py + r), new PointF(px - r, py) });
				}

				// ——— line — ◆ ◆ ◆ — line ———
				void Ornament()
				{
					float yy = y + 7;
					g.DrawLine(thin, x0, yy, cx - 24, yy);
					g.DrawLine(thin, cx + 24, yy, x1, yy);
					Diamond(cx, yy, 5f);
					Diamond(cx - 14, yy, 3f);
					Diamond(cx + 14, yy, 3f);
					y += 16;
				}

				// ===== 1. LOGO / BRAND =====
				Bitmap logo = GetReceiptLogo();
				if (logo != null)
				{
					float lw = Math.Min(W * 0.62f, 175f);
					float lh = lw * logo.Height / logo.Width;
					g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
					g.DrawImage(logo, cx - lw / 2f, y, lw, lh);
					y += lh + 4;
				}
				else
				{
					Center(Spaced("KEY LUXERY"), brandFont);
					y += 2;
				}

				// ===== 2. SHOP DETAILS =====
				Center("Jigjigayar, Road NO.1, Hargeisa, Somaliland", small);
				Center("Tel: +01234567890", mono);
				Center("keyluxery@gmail.com", small);
				y += 2;
				Ornament();

				// ===== 3. INVOICE NUMBER (double frame) =====
				float numSize = 14f;
				Font numFont = new Font("Consolas", numSize, FontStyle.Bold);
				while (g.MeasureString(d.InvoiceNo, numFont).Width > W - 24f && numSize > 8f)
				{
					numFont.Dispose();
					numSize -= 0.5f;
					numFont = new Font("Consolas", numSize, FontStyle.Bold);
				}
				using (numFont)
				{
					float boxH = LineH(monoB) + LineH(numFont) + 16f;
					g.DrawRectangle(thick, x0, y, W, boxH);
					g.DrawRectangle(thin, x0 + 4f, y + 4f, W - 8f, boxH - 8f);
					y += 7f;
					Center(Spaced("INVOICE"), monoB);
					Center(d.InvoiceNo, numFont);
					y += 9f;
				}
				y += 4f;

				// ===== 4. CUSTOMER / DATE =====
				string dateStr = d.InvoiceDate.HasValue ? d.InvoiceDate.Value.ToString("dd-MM-yyyy") : "";
				string timeStr = (d.InvoiceDate.HasValue && d.InvoiceDate.Value.TimeOfDay.Ticks > 0)
					? d.InvoiceDate.Value.ToString("hh:mm tt") : "";
				LR("Date: " + dateStr, timeStr, mono);
				Left("Customer: " + d.CustomerName, mono);
				if (!string.IsNullOrWhiteSpace(d.CustomerPhone)) Left("Phone: " + d.CustomerPhone, mono);
				Left("Cashier: " + d.User, mono);

				// ===== 5. ITEMS TABLE =====
				DoubleRule();
				float itemX = x0 + 34f;
				float priceW = 70f;
				float itemW = W - 34f - priceW;
				g.DrawString("Qty", monoB, Brushes.Black, x0, y);
				g.DrawString("Item", monoB, Brushes.Black, itemX, y);
				g.DrawString("Price", monoB, Brushes.Black, new RectangleF(x1 - priceW, y, priceW, LineH(monoB) + 2), right);
				y += LineH(monoB);
				Rule(thin);

				foreach (DataRow row in d.Items.Rows)
				{
					string name = row["product_name"].ToString();
					int qty = Convert.ToInt32(row["quantity"]);
					decimal unit = Convert.ToDecimal(row["unit_price"]);
					decimal total = Convert.ToDecimal(row["total_price"]);

					g.DrawString(qty.ToString(), mono, Brushes.Black, x0, y);
					g.DrawString($"${total:N2}", mono, Brushes.Black, new RectangleF(x1 - priceW, y, priceW, LineH(mono) + 2), right);
					y += Wrapped(name, mono, itemX, itemW);

					g.DrawString($"{qty} x ${unit:N2}", small, Brushes.Black, itemX, y);
					y += LineH(small) + 5f;
				}

				// ===== 6. PAYMENT + TOTALS =====
				Rule(dashed);
				LR(payMethod.ToUpper(), status.ToUpper(), monoB);
				y += 6f;
				LR("Subtotal", $"${d.Subtotal:N2}", mono);
				LR("GST Tax (2.5%)", $"${d.Tax:N2}", mono);
				LR("Discount", (d.Discount > 0 ? "-" : "") + $"${d.Discount:N2}", mono);
				y += 5f;

				// Inverted total bar
				float barH = LineH(totalFont) + 10f;
				g.FillRectangle(Brushes.Black, x0, y, W, barH);
				g.DrawString("TOTAL", totalFont, Brushes.White, x0 + 8f, y + 5f);
				g.DrawString($"${d.GrandTotal:N2}", totalFont, Brushes.White, new RectangleF(x0, y + 5f, W - 8f, LineH(totalFont) + 4f), right);
				y += barH + 8f;

				// ===== 7. TRANSACTION DETAILS =====
				LR("Transaction Type:", "Sale", mono);
				LR("Payment Method:", payMethod, mono);
				LR("Status:", status, mono);
				LR("Printed:", DateTime.Now.ToString("dd-MM-yyyy HH:mm"), mono);

				// ===== 8. STAMP (only when paid) =====
				y += 8f;
				if (string.Equals(status, "Paid", StringComparison.OrdinalIgnoreCase))
				{
					float stampH = 44f;
					var st = g.Save();
					g.TranslateTransform(cx, y + stampH / 2f);
					g.RotateTransform(-10);
					SizeF ss = g.MeasureString("PAID", stampFont);
					g.DrawRectangle(thick, -ss.Width / 2f - 6f, -ss.Height / 2f - 2f, ss.Width + 12f, ss.Height + 4f);
					g.DrawString("PAID", stampFont, Brushes.Black, -ss.Width / 2f, -ss.Height / 2f);
					g.Restore(st);
					y += stampH + 6f;
				}

				// ===== 9. SIGNATURE =====
				y += 12f;
				float sigLineY = y + LineH(monoB) - 2f;
				g.DrawString("X", monoB, Brushes.Black, x0, y);
				g.DrawLine(thin, x0 + 14f, sigLineY, x0 + W * 0.72f, sigLineY);
				y += LineH(monoB) + 2f;
				g.DrawString("Customer signature", small, Brushes.Black, x0 + 14f, y);
				y += LineH(small) + 8f;

				// ===== 10. FOOTER =====
				Ornament();
				DrawKeyIcon(g, cx, y + 22f, 38f, Color.Black);
				y += 46f;
				Center(Spaced("THANK YOU"), monoB);
				Center("for shopping with Key Luxery", mono);
				y += 3f;
				Center("Customer Copy", small);
				Center("Please come again", small);
				y += 4f;
				Rule(dashed);

				return y + 24f;   // extra blank paper so the cutter doesn't slice the last line
			}
		}

		// Draws a key rotated 45° centered at (cx, cy)
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