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
		private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
		{
			Graphics g = e.Graphics;

			// Enable high-quality rendering
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

			// 1. DYNAMICALLY READ SELECTED PRINTER PAGE DIMENSIONS
			int pageWidth = e.PageBounds.Width;
			int pageHeight = e.PageBounds.Height;

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
			using (Font brandFont = new Font("Segoe UI", 13f * scale, FontStyle.Bold))
			using (Font boldFont = new Font("Segoe UI", 8.5f * scale, FontStyle.Bold))
			using (Font regularFont = new Font("Segoe UI", 8.5f * scale, FontStyle.Regular))
			using (Font smallFont = new Font("Segoe UI", 7.5f * scale, FontStyle.Regular))
			{
				int offset = 0;

				// --- 2. TOP HEADER SECTION ---
				int topHeaderY = startY + offset;

				// Left-side Black Brand Box
				int sidebarWidth = (int)(printableWidth * 0.38f);
				g.FillRectangle(darkBlackBrush, startX, topHeaderY, sidebarWidth, (int)(40 * scale));
				g.DrawString("Key Luxery Logo", brandFont, whiteBrush, startX + (int)(12 * scale), topHeaderY + (int)(9 * scale));

				// Right-side INVOICE Title & Invoice No
				g.DrawString("INVOICE", invoiceLabelFont, goldBrush, startX + (int)(printableWidth * 0.60f), topHeaderY);
				g.DrawString("No. 789456123", boldFont, darkTextBrush, startX + (int)(printableWidth * 0.60f), topHeaderY + (int)(28 * scale));

				offset += (int)(55 * scale);

				// --- 3. BILL TO & METADATA SECTION ---
				int metaY = startY + offset;
				g.DrawString("INVOICE TO:", boldFont, goldBrush, startX, metaY);

				string customerDetails = $"{txtCustomer_name.Text}\nKey Luxery\nPhone: +01234567890\nAddress: abc, Road NO.1, Hargeisa, Somaliland";
				g.DrawString(customerDetails, regularFont, darkTextBrush, startX, metaY + (int)(16 * scale));

				// Right side Date info
				int metaRightX = startX + (int)(printableWidth * 0.60f);
				g.DrawString($"Invoice Date: {dtpSaleDate.Value.ToString("dd-MM-yyyy")}", regularFont, darkTextBrush, metaRightX, metaY + (int)(16 * scale));

				offset += (int)(80 * scale);

				// --- 4. TABLE HEADERS (Rich Gold Bar) ---
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

				// --- 5. TABLE ROWS WITH ALTERNATING SHADING ---
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

					if (rowCounter % 2 == 0)
					{
						g.FillRectangle(lightGrayBrush, startX, startY + offset, printableWidth, rowHeight);
					}
					g.DrawRectangle(goldPen, startX, startY + offset, printableWidth, rowHeight);

					g.DrawString(rowCounter.ToString("D2"), regularFont, darkTextBrush, colNoX, startY + offset + (int)(5 * scale));
					g.DrawString(pName, regularFont, darkTextBrush, colDescX, startY + offset + (int)(5 * scale));
					g.DrawString(priceVal.ToString("N2"), regularFont, darkTextBrush, colPriceX, startY + offset + (int)(5 * scale), rightFormat);
					g.DrawString(qtyVal.ToString("D2"), regularFont, darkTextBrush, colUnitX, startY + offset + (int)(5 * scale), centerFormat);
					g.DrawString(subTotalVal.ToString("N2"), regularFont, darkTextBrush, colTotalX, startY + offset + (int)(5 * scale), rightFormat);

					offset += rowHeight;
					rowCounter++;
				}

				offset += (int)(15 * scale);

				// --- 6. SUB-TOTAL & TAX GOLD BANNER ---
				int subtotalBarHeight = (int)(30 * scale);
				g.FillRectangle(goldBrush, startX, startY + offset, printableWidth, subtotalBarHeight);

				decimal gstTax = subtotalSum * 0.025m; // 2.5% GST Tax
				g.DrawString("Sub Total:", boldFont, whiteBrush, startX + (int)(10 * scale), startY + offset + (int)(7 * scale));
				g.DrawString($"${subtotalSum:N2}", boldFont, whiteBrush, startX + printableWidth - (int)(15 * scale), startY + offset + (int)(7 * scale), rightFormat);

				offset += subtotalBarHeight;

				g.DrawString("Country Tax 2.5%", regularFont, darkTextBrush, startX + (int)(10 * scale), startY + offset + (int)(4 * scale));
				offset += (int)(25 * scale);

				// --- 7. LOWER SECTION: LEFT BLACK SIDEBAR & RIGHT TOTALS ---
				int lowerSectionY = startY + offset;
				int sidebarHeight = (int)(110 * scale);

				// Solid Black Left Sidebar Column (Thank You & Payment Info)
				g.FillRectangle(darkBlackBrush, startX, lowerSectionY, sidebarWidth, sidebarHeight);

				g.DrawString("Thank you for your payment", boldFont, whiteBrush, startX + (int)(10 * scale), lowerSectionY + (int)(10 * scale));
				string paymentInfoText = $"Payment Info:\nZaad: free@psd.zone\nStatus: Paid\nUser:{Properties.Settings.Default.username}";
				g.DrawString(paymentInfoText, smallFont, whiteBrush, startX + (int)(10 * scale), lowerSectionY + (int)(32 * scale));

				// Calculations & Conditional Discount Check
				decimal discount = 0;
				bool hasDiscount = decimal.TryParse(txtDiscount?.Text?.Trim(), out discount) && discount > 0;

				decimal grandTotal = (subtotalSum + gstTax) - (hasDiscount ? discount : 0);
				if (grandTotal < 0) grandTotal = 0;

				// Right Side Totals Calculation block
				int totalsX = startX + (int)(printableWidth * 0.55f);
				int currentTotalY = lowerSectionY + (int)(10 * scale);

				if (hasDiscount)
				{
					g.DrawString("Discount:", regularFont, darkTextBrush, totalsX, currentTotalY);
					g.DrawString($"-${discount:N2}", regularFont, darkTextBrush, startX + printableWidth, currentTotalY, rightFormat);
					currentTotalY += (int)(22 * scale);
				}

				g.DrawString("TOTAL :", invoiceLabelFont, goldBrush, totalsX, currentTotalY);
				g.DrawString($"${grandTotal:N2}", invoiceLabelFont, goldBrush, startX + printableWidth, currentTotalY, rightFormat);

				// Authorised Signature Line (Right Side below totals)
				int signWidth = (int)(180 * scale);
				int signX = startX + printableWidth - signWidth;
				int signY = lowerSectionY + (int)(75 * scale);

				g.DrawLine(darkPen, signX, signY, startX + printableWidth, signY);
				g.DrawString("Manager name\nKey Luxery", smallFont, darkTextBrush, signX, signY + (int)(4 * scale));

				offset += sidebarHeight + (int)(15 * scale);

				// --- 8. BOTTOM FOOTER BAR WITH CONTACT DETAILS ---
				int footerY = startY + offset;
				int footerHeight = (int)(35 * scale);

				g.FillRectangle(darkBlackBrush, startX, footerY, printableWidth, footerHeight);
				g.DrawString("Key Luxery", brandFont, whiteBrush, startX + (int)(10 * scale), footerY + (int)(8 * scale));

				string contactFooterText = "📞 +01234567890     ✉ keyluxery@gmail.com     📍 123, Street 1, Hargeisa, Somaliland";
				g.DrawString(contactFooterText, smallFont, whiteBrush, startX + (int)(120 * scale), footerY + (int)(11 * scale));
			}
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

		private decimal exchangeRate = 8500m; // Example exchange rate

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
	}
}