using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace key_luxery1
{
	public partial class uPayments : UserControl
	{
		private DataTable masterDataTable = new DataTable();
		private int currentPage = 1;
		private int pageSize = 20; // Number of payments per page
		private int totalPages = 1;

		public uPayments()
		{
			InitializeComponent();
		}

		private void uPayments_Load(object sender, EventArgs e)
		{
			InitializePaymentTypes();
			LoadPaymentsData();
		}

		private void InitializePaymentTypes()
		{
			cmbFilterMethod.Items.Clear();
			cmbFilterMethod.Items.Add("All Methods");
			cmbFilterMethod.Items.Add("Cash");
			cmbFilterMethod.Items.Add("Bank Transfer");
			cmbFilterMethod.Items.Add("Mobile Money");
			cmbFilterMethod.Items.Add("Credit Card");
			cmbFilterMethod.SelectedIndex = 0;
		}

		public void LoadPaymentsData()
		{
			try
			{
				string query = "SELECT payment_id AS [Payment ID], invoice_id AS [Invoice ID], amount_paid AS [Amount Paid], payment_method AS [Method], reference_no AS [Reference], payment_date AS [Payment Date], Status FROM tblPayment WHERE deleted=0";
				masterDataTable = sharedClass.GetData(query);

				currentPage = 1;
				ApplyFiltersAndPagination();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error loading payments data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void ApplyFiltersAndPagination()
		{
			if (masterDataTable == null || masterDataTable.Rows.Count == 0)
			{
				dgvPayments.DataSource = null;
				lblPageInfo.Text = "Page 0 of 0";
				totalPages = 1;
				return;
			}

			string searchValue = txtSearchFilter.Text.Trim().Replace("'", "''");
			string selectedMethod = cmbFilterMethod.SelectedItem?.ToString() ?? "All Methods";

			StringBuilder filterExpression = new StringBuilder("1=1");

			if (!string.IsNullOrEmpty(searchValue))
			{
				filterExpression.Append($" AND (reference_No LIKE '%{searchValue}%' OR customer_Name LIKE '%{searchValue}%')");
			}

			if (selectedMethod != "All Methods")
			{
				filterExpression.Append($" AND payment_Method = '{selectedMethod}'");
			}

			DataView dv = masterDataTable.DefaultView;
			dv.RowFilter = filterExpression.ToString();
			DataTable filteredTable = dv.ToTable();

			// Calculate total pages based on filtered records
			int totalRecords = filteredTable.Rows.Count;
			totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
			if (totalPages == 0) totalPages = 1;

			if (currentPage > totalPages) currentPage = totalPages;
			if (currentPage < 1) currentPage = 1;

			// Slice table for current page
			DataTable pagedTable = filteredTable.Clone();
			int startIndex = (currentPage - 1) * pageSize;
			int endIndex = Math.Min(startIndex + pageSize, totalRecords);

			for (int i = startIndex; i < endIndex; i++)
			{
				pagedTable.ImportRow(filteredTable.Rows[i]);
			}

			dgvPayments.DataSource = pagedTable;
			lblPageInfo.Text = $"Page {currentPage} of {totalPages}";

			// Toggle pagination buttons
			btnFirst.Enabled = (currentPage > 1);
			btnPrev.Enabled = (currentPage > 1);
			btnNext.Enabled = (currentPage < totalPages);
			btnLast.Enabled = (currentPage < totalPages);
		}

		private void txtSearchFilter_TextChanged(object sender, EventArgs e)
		{
			currentPage = 1; // Reset to page 1 on search change
			ApplyFiltersAndPagination();
		}

		private void cmbFilterMethod_SelectedIndexChanged(object sender, EventArgs e)
		{
			currentPage = 1; // Reset to page 1 on filter change
			ApplyFiltersAndPagination();
		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			txtSearchFilter.Clear();
			if (cmbFilterMethod.Items.Count > 0)
			{
				cmbFilterMethod.SelectedIndex = 0;
			}
			LoadPaymentsData();
		}

		private void btnFirst_Click(object sender, EventArgs e)
		{
			currentPage = 1;
			ApplyFiltersAndPagination();
		}

		private void btnPrev_Click(object sender, EventArgs e)
		{
			if (currentPage > 1)
			{
				currentPage--;
				ApplyFiltersAndPagination();
			}
		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			if (currentPage < totalPages)
			{
				currentPage++;
				ApplyFiltersAndPagination();
			}
		}

		private void btnLast_Click(object sender, EventArgs e)
		{
			currentPage = totalPages;
			ApplyFiltersAndPagination();
		}

		private void btnExport_Click(object sender, EventArgs e)
		{
			if (masterDataTable == null || masterDataTable.Rows.Count == 0)
			{
				MessageBox.Show("No payment data available to export.", "Export Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "CSV file|*.csv", ValidateNames = true, FileName = "PaymentLedger.csv" })
			{
				if (sfd.ShowDialog() == DialogResult.OK)
				{
					try
					{
						StringBuilder sb = new StringBuilder();

						for (int i = 0; i < dgvPayments.Columns.Count; i++)
						{
							sb.Append(dgvPayments.Columns[i].HeaderText + (i == dgvPayments.Columns.Count - 1 ? "" : ","));
						}
						sb.AppendLine();

						foreach (DataRow row in masterDataTable.Rows)
						{
							for (int i = 0; i < masterDataTable.Columns.Count; i++)
							{
								string cellValue = row[i]?.ToString() ?? "";
								cellValue = "\"" + cellValue.Replace("\"", "\"\"") + "\"";
								sb.Append(cellValue + (i == masterDataTable.Columns.Count - 1 ? "" : ","));
							}
							sb.AppendLine();
						}

						File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
						MessageBox.Show("Payment ledger exported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					catch (Exception ex)
					{
						MessageBox.Show("Error exporting file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}

		private void dgvPayments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				// Handle row double-click if needed
			}
		}
	}
}