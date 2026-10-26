using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace key_luxery1
{
	public partial class frmManageSale : Form
	{
		sharedClass db = new sharedClass();
		// Track state variables
		private string currentInvoiceId = "";
		private bool isSaleCompleted = false;

		// Found product cache
		private string foundProductId = "";
		private string foundProductName = "";
		private decimal foundProductPrice = 0;
		private int foundProductStock = 0;

		string userrole = Properties.Settings.Default.userRole;

		public frmManageSale()
		{
			InitializeComponent();
		}

		private void frmManageSale_Load(object sender, EventArgs e)
		{
			InitializeNewInvoiceOnOpen();

			if (userrole != "Admin")
			{
				txtDiscount.ReadOnly = true;
			}
			else if (userrole == "Admin")
			{
				txtDiscount.ReadOnly = false;
			}
		}

		// --- 1. CREATE INVOICE IMMEDIATELY UPON OPENING FORM ---
		private void InitializeNewInvoiceOnOpen()
		{
			try
			{
				// Generate next Invoice Number sequence string
				DataTable dt = sharedClass.GetData("SELECT TOP 1 invoice_id FROM tblInvoice ORDER BY invoice_id DESC");
				int nextId = 1;
				if (dt.Rows.Count > 0 && dt.Rows[0]["invoice_id"] != DBNull.Value)
				{
					nextId = Convert.ToInt32(dt.Rows[0]["invoice_id"]) + 1;
				}

				string generatedInvoiceNo = $"INV-" + DateTime.Now.ToString("yyMMddHHmmss");
				string currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

				// Insert a draft/unpaid placeholder record immediately into tblInvoice
				string query = $"INSERT INTO tblInvoice (invoice_no, invoice_date, subtotal, discount,tax, grand_total, status) " +
							   $"VALUES ('{generatedInvoiceNo}', '{currentDate}', 0.00, 0.00, 0.00, 0.00, 'Draft')";
				db.ExecuteQueries(query);

				// Fetch the newly created database ID
				DataTable dtCreated = sharedClass.GetData($"SELECT invoice_id FROM tblInvoice WHERE invoice_no = '{generatedInvoiceNo}'");
				if (dtCreated.Rows.Count > 0)
				{
					currentInvoiceId = dtCreated.Rows[0]["invoice_id"].ToString();
					txtInvoiceId.Text = currentInvoiceId;
					txtInvoiceNo.Text = generatedInvoiceNo;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error initializing draft invoice: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				MessageBox.Show("The error is from generating invoice", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		// Search customer automatically by typing phone number
		// Declare a variable at the form level to hold the ID
		public int? selectedCustomerId;
		private void txtCustomerPhone_TextChanged(object sender, EventArgs e)
		{
			//string phone = txtCustomerPhone.Text.Trim();
			//selectedCustomerId = null; // Reset if not found

			//if (!string.IsNullOrEmpty(phone))
			//{
			//	foreach (DataRowView item in cmbCustomer.Items)
			//	{
			//		DataRow row = item.Row;
			//		string dbPhone = row["customer_phone"]?.ToString() ?? "";

			//		if (dbPhone.Equals(phone))
			//		{
			//			cmbCustomer.SelectedItem = item;

			//			// Get the customer ID from the data row
			//			selectedCustomerId = int.Parse(row["customer_id"].ToString());
			//			txtcustomer_id.Text = selectedCustomerId.ToString();
			//			break;
			//		}
			//	}
			//}
		}

		// --- 2. FIND PRODUCT BY NAME & FETCH DATA ---
		private void btnFindProduct_Click(object sender, EventArgs e)
		{
			string searchName = txtSearchProduct.Text.Trim();
			if (string.IsNullOrEmpty(searchName))
			{
				MessageBox.Show("Please enter a product name to search.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			try
			{
				DataTable dt = sharedClass.GetData($"SELECT product_id, product_name, price, quantity FROM tblProduct WHERE product_name LIKE '%{searchName}%' AND deleted = 0");

				if (dt.Rows.Count > 0)
				{
					DataRow row = dt.Rows[0];
					foundProductId = row["product_id"].ToString();
					foundProductName = row["product_name"].ToString();
					foundProductPrice = Convert.ToDecimal(row["price"]);
					foundProductStock = Convert.ToInt32(row["quantity"]);

					txtSearchProduct.Text = foundProductName;
					MessageBox.Show($"Product Found!\nName: {foundProductName}\nPrice: ${foundProductPrice:N2}\nIn Stock: {foundProductStock}",
						"Product Match", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					MessageBox.Show("No active product found with that name.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					foundProductId = "";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error finding product: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnAddItem_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(foundProductId))
			{
				MessageBox.Show("Please search and click 'Find' for a valid product first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (!int.TryParse(txtQuantity.Text.Trim(), out int qty) || qty <= 0)
			{
				MessageBox.Show("Please enter a valid quantity.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (qty > foundProductStock)
			{
				MessageBox.Show($"Requested quantity exceeds available stock ({foundProductStock} left).", "Stock Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			foreach (DataGridViewRow row in dgvCart.Rows)
			{
				if (row.Cells["colProductID"].Value.ToString() == foundProductId)
				{
					int existingQty = Convert.ToInt32(row.Cells["colQuantity"].Value);
					int newQty = existingQty + qty;
					if (newQty > foundProductStock)
					{
						MessageBox.Show("Total cart quantity exceeds available stock.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					row.Cells["colQuantity"].Value = newQty;
					row.Cells["colSubtotal"].Value = newQty * foundProductPrice;
					CalculateGrandTotal();
					return;
				}
			}

			decimal subtotal = foundProductPrice * qty;
			dgvCart.Rows.Add(foundProductId, foundProductName, foundProductPrice, qty, subtotal);
			CalculateGrandTotal();
		}

		private void btnRemoveItem_Click(object sender, EventArgs e)
		{
			if (dgvCart.SelectedRows.Count > 0)
			{
				dgvCart.Rows.RemoveAt(dgvCart.SelectedRows[0].Index);
				CalculateGrandTotal();
			}
			else
			{
				MessageBox.Show("Please select a cart item to remove.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void btnClearCart_Click(object sender, EventArgs e)
		{
			dgvCart.Rows.Clear();
			CalculateGrandTotal();
		}

		private void txtDiscount_TextChanged(object sender, EventArgs e)
		{
			CalculateGrandTotal();
		}

		private void CalculateGrandTotal()
		{
			decimal subtotal = 0;
			foreach (DataGridViewRow row in dgvCart.Rows)
			{
				subtotal += Convert.ToDecimal(row.Cells["colSubtotal"].Value);
			}

			decimal discount = 0;
			decimal.TryParse(txtDiscount.Text.Trim(), out discount);

			// 1. Calculate 2.5% tax for the entire invoice
			decimal taxRate = 0.025m;
			decimal taxAmount = subtotal * taxRate;

			// Show tax amount in txtTax
			txtTax.Text = taxAmount.ToString("0.00");

			// 2. Grand total = subtotal + tax - discount
			decimal grandTotal = (subtotal + taxAmount) - discount;
			lblGrandTotalValue.Text = $"${grandTotal:N2}";
		}

		// --- 3. COMPLETE SALE & FINALISE DRAFT INVOICE ---
		private void btnCompleteSale_Click(object sender, EventArgs e)
		{
			completeSale();
			printPreviewFunction();
		}

		public void printPreviewFunction()
		{

			// 7. Safe Print Execution Block
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
			catch (Exception printEx)
			{
				MessageBox.Show("Sale saved successfully, but printing failed due to printer settings: " + printEx.Message,
					"Printer Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}

			this.Close();
		}

		// --- KEY LUXURY: BLACK & GOLD LUXURY INVOICE (With Logo Placeholder & Summary Table) ---
		// --- PROFESSIONAL BLACK & GOLD LUXURY INVOICE PRINT LAYOUT ---
		// --- KEY LUXURY: BLACK & GOLD LUXURY INVOICE PRINT LAYOUT ---
		private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
		{
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
				g.DrawString(txtInvoiceNo.Text, boldFont, whiteBrush, startX + printableWidth - (int)(12 * scale), startY + offset + (int)(9 * scale), rightFormat);
				offset += bandH + (int)(10 * scale);

				// ===== CUSTOMER DETAILS & DATE =====
				int metaY = startY + offset;
				int step = (int)(18 * scale);
				int labelW = (int)(60 * scale);
				int halfW = (int)(printableWidth * 0.5f);

				// Left: invoice to
				g.DrawString("INVOICE TO", boldFont, goldBrush, startX, metaY);
				int lineY = metaY + (int)(17 * scale);
				g.DrawLine(goldPen, startX, lineY, startX + halfW - (int)(15 * scale), lineY);

				int row1 = lineY + (int)(5 * scale);
				int row2 = row1 + step;
				g.DrawString("Customer:", boldFont, darkTextBrush, startX, row1);
				g.DrawString(txtCustomer_name.Text, regularFont, darkTextBrush, startX + labelW, row1);
				g.DrawString("Phone:", boldFont, darkTextBrush, startX, row2);
				g.DrawString(txtSearchCustomerPhone.Text, regularFont, darkTextBrush, startX + labelW, row2);

				// Right: date
				int rightX = startX + printableWidth;
				g.DrawString("INVOICE DATE", boldFont, goldBrush, rightX, metaY, rightFormat);
				g.DrawLine(goldPen, rightX - (int)(120 * scale), lineY, rightX, lineY);
				g.DrawString(dtpSaleDate.Value.ToString("dd-MM-yyyy"), regularFont, darkTextBrush, rightX, row1, rightFormat);

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

				// ===== TABLE ROWS =====
				decimal subtotalSum = 0;
				int rowHeight = (int)(26 * scale);
				int rowCounter = 1;

				foreach (DataGridViewRow row in dgvCart.Rows)
				{
					if (row.IsNewRow) continue;

					string pName = row.Cells["colProductName"].Value?.ToString() ?? "";
					decimal priceVal = Convert.ToDecimal(row.Cells["colPrice"].Value ?? 0);
					int qtyVal = Convert.ToInt32(row.Cells["colQuantity"].Value ?? 0);
					decimal subTotalVal = Convert.ToDecimal(row.Cells["colSubtotal"].Value ?? 0);
					subtotalSum += subTotalVal;

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
					g.DrawString(subTotalVal.ToString("N2"), regularFont, darkTextBrush, colTotalX, rowY + textPad, rightFormat);

					offset += rowHeight;
					rowCounter++;
				}

				// ===== CALCULATIONS =====
				offset += (int)(12 * scale);   // small gap after the last row

				decimal gstTax = subtotalSum * 0.025m;
				decimal discount = 0;
				bool hasDiscount = decimal.TryParse(txtDiscount?.Text?.Trim(), out discount) && discount > 0;
				if (!hasDiscount) discount = 0;
				decimal grandTotal = (subtotalSum + gstTax) - discount;
				if (grandTotal < 0) grandTotal = 0;

				// ===== LOWER SECTION: PAYMENT INFO (left) + TOTALS TABLE (right) =====
				int blockY = startY + offset;
				int sidebarWidth = (int)(printableWidth * 0.38f);

				int tableW = (int)(printableWidth * 0.48f);
				int tableX = startX + printableWidth - tableW;
				int smallRowH = (int)(22 * scale);
				int grandH = (int)(44 * scale);
				int pad = (int)(10 * scale);

				int ty = blockY;
				ty = DrawTotalRow(g, "Sub Total", $"${subtotalSum:N2}", tableX, ty, tableW, smallRowH, regularFont, darkTextBrush, goldPen, lightGrayBrush, pad);
				ty = DrawTotalRow(g, "GST Tax (2.5%)", $"${gstTax:N2}", tableX, ty, tableW, smallRowH, regularFont, darkTextBrush, goldPen, null, pad);
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
				string paymentInfoText = $"Payment Info:\nMethod: {cmbPaymentMethod.Text}\nStatus: Paid\nUser: {Properties.Settings.Default.username}";
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

				// Area available for text (small padding left and right)
				float padX = 10f * scale;
				RectangleF footerTextRect = new RectangleF(
					startX + padX,
					footerY + (int)(3 * scale),                 // below the gold line
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
					Alignment = StringAlignment.Center,         // centered horizontally
					LineAlignment = StringAlignment.Center,     // centered vertically
					FormatFlags = StringFormatFlags.NoWrap
				})
				{
					g.DrawString(contactFooterText, footerFont, whiteBrush, footerTextRect, footerFormat);
				}
			}
		}

		// ---------- HELPERS (put these inside the same class) ----------

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

		private void btnCancel_Click(object sender, EventArgs e)
		{
			try
			{
				//update the invoice into draft or canceled
				string cancelInvoice = $"UPDATE tblInvoice SET " +
										   $"status = 'Canceled' " +
										   $"WHERE invoice_id = {currentInvoiceId}";
				db.ExecuteQueries(cancelInvoice);
				MessageBox.Show("Sale canceled. Draft invoice has been marked as Canceled.", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Clean up empty draft invoice if cashier exits without completing the sale
		private void frmManageSale_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!isSaleCompleted && !string.IsNullOrEmpty(currentInvoiceId))
			{
				try
				{
					string cleanupQuery = $"DELETE FROM tblInvoice WHERE invoice_id = {currentInvoiceId} AND status = 'Draft'";
					db.ExecuteQueries(cleanupQuery);
				}
				catch
				{
					// Fail silently on cleanup if needed
				}
			}
		}


		public void completeSale()
		{
			if (dgvCart.Rows.Count == 0)
			{
				MessageBox.Show("Your cart is empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			try
			{
				string saleDate = dtpSaleDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
				string paymentMethod = cmbPaymentMethod.Text;

				decimal subtotal = 0;
				foreach (DataGridViewRow row in dgvCart.Rows)
				{
					subtotal += Convert.ToDecimal(row.Cells["colSubtotal"].Value);
				}

				decimal discount = 0;
				decimal.TryParse(txtDiscount.Text.Trim(), out discount);

				// 1. Calculate 2.5% tax for the entire invoice
				decimal taxRate = 0.025m;
				decimal taxAmount = subtotal * taxRate;

				// Display the calculated tax amount in txtTax formatted to 2 decimal places
				txtTax.Text = taxAmount.ToString("0.00");

				// 2. Grand total = subtotal + tax - discount
				decimal grandTotal = (subtotal + taxAmount) - discount;
				if (grandTotal < 0) grandTotal = 0;

				decimal totalProfit = 0;

				// 3. Process each item in the cart, calculate item profit, and update stock
				foreach (DataGridViewRow row in dgvCart.Rows)
				{
					string productId = row.Cells["colProductID"].Value.ToString();
					decimal price = Convert.ToDecimal(row.Cells["colPrice"].Value);
					decimal qty = Convert.ToDecimal(row.Cells["colQuantity"].Value);
					decimal lineTotal = Convert.ToDecimal(row.Cells["colSubtotal"].Value);
					string product_name = row.Cells["colProductName"].Value.ToString();

					// Fetch unitcost from tblProduct for this item
					decimal unitCost = 0;
					object costObj = db.ExecuteScalar($"SELECT unitcost FROM tblProduct WHERE product_id = {productId}");
					if (costObj != null && costObj != DBNull.Value)
					{
						unitCost = Convert.ToDecimal(costObj);
					}

					// Calculate line profit: (Cart Unit Price - Product Unit Cost) * Quantity
					decimal lineProfit = (price - unitCost) * qty;
					totalProfit += lineProfit;

					// Insert sale record
					string insertsale = $"INSERT INTO tblSale (invoice_id, product_id, product_name, quantity, unit_price, total_price, date, deleted) " +
										$"VALUES ({currentInvoiceId}, {productId}, '{product_name}', {qty}, {price}, {lineTotal}, '{saleDate}', 0)";
					db.ExecuteQueries(insertsale);

					// Deduct stock
					string updateStock = $"UPDATE tblProduct SET quantity = quantity - {qty} WHERE product_id = {productId}";
					db.ExecuteQueries(updateStock);
				}

				// 4. Deduct the discount from the total accumulated profit
				totalProfit -= discount;

				// Handle customer ID (if empty or not selected, save as NULL)
				string customerIdValue = string.IsNullOrWhiteSpace(txtcustomer_id.Text)
					? "NULL"
					: txtcustomer_id.Text.Trim();

				// 5. Update the Invoice with customer_id, tax, grand total, and profit
				string updateInvoice = $"UPDATE tblInvoice SET " +
									 $"invoice_date = '{saleDate}', " +
									 $"subtotal = {subtotal}, " +
									 $"tax = {taxAmount}, " +
									 $"discount = {discount}, " +
									 $"grand_total = {grandTotal}, " +
									 $"total_profit = {totalProfit}, " +
									 $"customer_id = {customerIdValue}, " +
									 $"customer_name = '{txtCustomer_name.Text.Replace("'", "''")}', " +
									 $"status = 'Paid' " +
									 $"WHERE invoice_id = {currentInvoiceId}";
				db.ExecuteQueries(updateInvoice);

				// 6. Insert Payment Record
				string insertPayment = $"INSERT INTO tblPayment (invoice_id, amount_paid, payment_date, payment_method, deleted) " +
									 $"VALUES ({currentInvoiceId}, {grandTotal}, '{saleDate}', '{paymentMethod}',0)";
				db.ExecuteQueries(insertPayment);

				string insertExpense = $"INSERT INTO tblExpenses (Category, Amount, ExpenseDate, Description, CreatedAt, Status, Deleted, invoice_id) " +
					   $"VALUES ('Taxes & Insurance', {taxAmount}, GETDATE(), 'GST Tax payment', GETDATE(), 'Active', 0, {currentInvoiceId})";
				db.ExecuteQueries(insertExpense);

				isSaleCompleted = true;
				MessageBox.Show("Sale completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error completing sale: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnSaveOnly_Click(object sender, EventArgs e)
		{
			completeSale();
			this.Close();
		}

		private void printPreviewDialog1_Load(object sender, EventArgs e)
		{

		}
		/// <summary>
		/// 1. Searches for an existing customer by phone number and updates UI state dynamically.
		/// </summary>
		/// 
		private void SearchCustomer(bool showMessages)
		{
			try
			{
				string phone = txtSearchCustomerPhone.Text.Trim();
				if (string.IsNullOrEmpty(phone))
				{
					if (showMessages)
					{
						MessageBox.Show("Please enter a customer phone number.", "Validation Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						txtSearchCustomerPhone.Focus();
					}
					else
					{
						txtcustomer_id.Clear();
						txtCustomer_name.Clear();
						btnFindCustomer.Text = "Search";
					}
					return;
				}

				string query = $"SELECT customer_id, customer_name, customer_phone, status FROM tblCustomer WHERE deleted = 0 AND customer_phone = '{phone.Replace("'", "''")}'";
				DataTable dt = sharedClass.GetData(query);

				if (dt != null && dt.Rows.Count > 0)
				{
					// Customer Found: Populate fields and switch button to "History"
					txtcustomer_id.Text = dt.Rows[0]["customer_id"].ToString();
					txtCustomer_name.Text = dt.Rows[0]["customer_name"].ToString();
					btnFindCustomer.Text = "History";
				}
				else
				{
					// Customer Not Found: Clear fields and switch button to "Register"
					txtcustomer_id.Clear();
					txtCustomer_name.Clear();
					btnFindCustomer.Text = "Register";

					if (showMessages)
					{
						MessageBox.Show("No active customer found with that phone number. Enter their name and click 'Register'.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
			}
			catch (Exception ex)
			{
				if (showMessages)
				{
					MessageBox.Show("Error searching customer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
		private void LoadSearchedCustomer()
		{
			SearchCustomer(true);
		}

		/// <summary>
		/// 2. Handles registering a brand new customer into tblCustomer.
		/// </summary>
		private void RegisterCustomer()
		{
			try
			{
				string phone = txtSearchCustomerPhone.Text.Trim();
				string name = txtCustomer_name.Text.Trim();

				if (string.IsNullOrEmpty(phone))
				{
					MessageBox.Show("Please enter a customer phone number.", "Validation Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtSearchCustomerPhone.Focus();
					return;
				}

				if (string.IsNullOrEmpty(name))
				{
					MessageBox.Show("Please enter the customer name to register.", "Validation Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtCustomer_name.Focus();
					return;
				}

				// Fixed phone quotation to ensure safe SQL insertion for numbers starting with 0 or symbols
				string query = $"INSERT INTO tblCustomer (customer_phone, customer_name, deleted, status, date) VALUES ('{phone.Replace("'", "''")}', '{name.Replace("'", "''")}', 0, 'Active', GETDATE())";
				db.ExecuteQueries(query);

				MessageBox.Show("Customer registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

				// Automatically reload the newly registered customer and switch button back to "History"
				LoadSearchedCustomer();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error registering customer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Dynamic Action Button: Acts as Search, Register, or History based on current state text.
		/// </summary>

		/// <label>Optional: Reset button text to 'Search' if the phone number is changed</label>
		private void txtSearchCustomerPhone_TextChanged(object sender, EventArgs e)
		{
			// Reset button back to Search/Default when typing a new phone number
			SearchCustomer(false);

			//btnFindCustomer.Text = "Search";
		}

		private void btnFindCustomer_Click(object sender, EventArgs e)
		{
			string currentAction = btnFindCustomer.Text.Trim();

			if (currentAction == "Register")
			{
				RegisterCustomer();
			}
			else if (currentAction == "History")
			{
				// Open the Customer History Profile form we built
				frmCustomerHistory historyForm = new frmCustomerHistory();
				historyForm.CustomerName = txtCustomer_name.Text.Trim();
				historyForm.ShowDialog();
			}
			else
			{
				// Default action: Search
				LoadSearchedCustomer();
			}
		}

		private void txtTax_TextChanged(object sender, EventArgs e)
		{
			CalculateGrandTotal();
		}

		private decimal exchangeRate = 11500m; // Example exchange rate

		private void cmbCurrencyMode_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbCurrencyMode.SelectedItem?.ToString() == "SLSH")
			{
				lblExchangeRateDisplay.Text = $"Current Rate: 1 USD = {exchangeRate:N0} SLSH";
			}
			else
			{
				lblExchangeRateDisplay.Text = "Base Currency: USD ($)";
			}
		}

		private void cmbCurrencyMode_TextChanged(object sender, EventArgs e)
		{
			cmbCurrencyMode_SelectedIndexChanged(sender , e);
		}

		private void cmbCurrencyMode_SelectedIndexChanged_1(object sender, EventArgs e)
		{

		}
	}
}