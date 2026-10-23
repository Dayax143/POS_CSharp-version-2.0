using System;
using System.Data;
using System.Windows.Forms;

namespace key_luxery1
{
	public partial class frmStock : Form
	{
		private sharedClass db = new sharedClass();
		private string selectedProductId = "";
		// ➡️ ADD THIS LINE HERE:
		private string selectedProductName = "";
		public frmStock()
		{
			InitializeComponent();
		}

		private void frmStock_Load(object sender, EventArgs e)
		{
			LoadStockInventory();
			LoadStockLogs();
		}

		/// <summary>
		/// Loads current inventory quantities using sharedClass.DisplayData
		/// </summary>
		private void LoadStockInventory(string searchKeyword = "")
		{
			try
			{
				string query = "SELECT Product_ID AS [ID], Product_Name AS [Product Name], Category AS [Category], Quantity AS [Stock In Hand], Price AS [Unit Price] from tblProduct WHERE deleted=0";

				if (!string.IsNullOrEmpty(searchKeyword))
				{
					query += $" AND Product_Name LIKE '%{searchKeyword.Replace("'", "''")}%'";
				}

				query += " ORDER BY quantity ASC"; // Highlights low stock items at the top

				sharedClass.DisplayData(dgvCurrentStock, query);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error loading stock inventory: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Loads stock movement logs, active sales count, units sold, and profit for the selected product, ignoring deleted invoices/sales.
		/// </summary>
		private void LoadStockLogs()
		{
			try
			{
				// If no product is selected yet, clear the grid
				if (string.IsNullOrEmpty(selectedProductId))
				{
					dgvStockLogs.DataSource = null;
					return;
				}

				// Query joining product sales data, filtering out deleted invoices/records
				string query = $@"SELECT 
									p.Product_ID AS [Product ID],
									p.Product_Name AS [Product Name],
									p.Category AS [Category],
									ISNULL(SUM(si.quantity), 0) AS [Total Units Sold],
									COUNT(DISTINCT i.invoice_id) AS [Total Invoices],
									ISNULL(SUM(i.total_profit), 0) AS [Total Profit ($)],
									ISNULL(SUM(si.total_price), 0) AS [Total Sales Amount ($)]
								 FROM tblProduct p
								 LEFT JOIN tblSale si ON p.Product_ID = si.product_id and si.deleted=0
								 LEFT JOIN tblInvoice i ON si.invoice_id = i.invoice_id 
									AND (i.deleted = 0 OR i.deleted IS NULL)
								 WHERE p.Product_ID = {selectedProductId}
								 GROUP BY p.Product_ID, p.Product_Name, p.Category";

				DataTable dt = sharedClass.GetData(query);
				dgvStockLogs.DataSource = dt;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error loading product analytics: " + ex.Message, "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void dgvCurrentStock_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0) return;

			var row = dgvCurrentStock.Rows[e.RowIndex];
			selectedProductId = row.Cells["ID"].Value.ToString();
			selectedProductName = row.Cells["Product Name"].Value.ToString();

			txtSelectedProduct.Text = $"{selectedProductName} (Qty: {row.Cells["Stock In Hand"].Value})";
			LoadStockLogs();
		}

		private void txtSearchStock_TextChanged(object sender, EventArgs e)
		{
			LoadStockInventory(txtSearchStock.Text.Trim());
		}

		private void btnRestock_Click(object sender, EventArgs e)
		{
			ProcessStockAdjustment(true);
		}

		private void btnAdjust_Click(object sender, EventArgs e)
		{
			ProcessStockAdjustment(false);
		}

		/// <summary>
		/// Handles both Restocking (addition) and Shrinkage/Damage (subtraction) atomically.
		/// </summary>
		private void ProcessStockAdjustment(bool isRestock)
		{
			if (string.IsNullOrEmpty(selectedProductId))
			{
				MessageBox.Show("Please select a product from the inventory grid first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (!int.TryParse(txtAdjustmentQty.Text.Trim(), out int qty) || qty <= 0)
			{
				MessageBox.Show("Please enter a valid numeric quantity greater than 0.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			try
			{
				string operation = isRestock ? "+" : "-";
				string actionType = isRestock ? "Restock" : "Shrinkage/Damage";
				// Prevent stock from dropping below 0 on subtraction (Adjust/Damage)
				if (!isRestock)
				{
					DataTable dt = sharedClass.GetData($"SELECT Quantity FROM tblProduct WHERE Product_ID = {selectedProductId} and deleted=0");
					if (dt != null && dt.Rows.Count > 0)
					{
						int currentStock = Convert.ToInt32(dt.Rows[0]["Quantity"]);
						if (currentStock - qty < 0)
						{
							MessageBox.Show($"Action denied! Stock cannot drop below 0. Current stock is only {currentStock}.", "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							return;
						}
					}
				}

				string query = $"UPDATE tblProduct SET Quantity = Quantity {operation} {qty} WHERE Product_ID = {selectedProductId}";
				db.ExecuteQueries(query);

				MessageBox.Show($"Stock successfully adjusted! Action: {actionType} ({qty} units).", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

				// Refresh grids
				LoadStockInventory();
				LoadStockLogs();
				txtAdjustmentQty.Text = "1";
				txtSelectedProduct.Text = "Select item from grid ->";
				selectedProductId = "";
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error updating stock: " + ex.Message, "Transaction Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}