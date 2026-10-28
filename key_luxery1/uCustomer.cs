using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace key_luxery1
{
	public partial class uCustomer : UserControl
	{
		private sharedClass db = new sharedClass();
		private DataTable masterDataTable = new DataTable();
		private int currentPage = 1;
		private int pageSize = 20; // Number of customers per page
		private int totalPages = 1;

		public uCustomer()
		{
			InitializeComponent();
		}

		private void uCustomers_Load(object sender, EventArgs e)
		{
			LoadCustomersData();
		}

		/// <summary>
		/// Loads or filters customer records from tblCustomer into the DataTable and applies pagination.
		/// </summary>
		public void LoadCustomersData(string searchKeyword = "")
		{
			try
			{
				// Preserved your exact original query structure and column aliases
				string query = "SELECT customer_id AS [Customer ID], customer_name AS [Client Name], customer_phone AS [Phone Number], customer_address AS [Address], status AS [Status], date AS [Registration Date] FROM tblCustomer WHERE deleted=0";

				if (!string.IsNullOrEmpty(searchKeyword))
				{
					string safeKeyword = searchKeyword.Replace("'", "''");
					query += $" AND (customer_name LIKE '%{safeKeyword}%' OR customer_id LIKE '%{safeKeyword}%' OR CAST(customer_phone AS VARCHAR) LIKE '%{safeKeyword}%' OR customer_address LIKE '%{safeKeyword}%' OR status LIKE '%{safeKeyword}%')";
				}

				query += " ORDER BY date DESC";

				// Fetch data into masterDataTable using sharedClass.GetData (or equivalent data retrieval method)
				masterDataTable = sharedClass.GetData(query);

				currentPage = 1;
				DisplayPagedData(masterDataTable);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error loading customer data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void DisplayPagedData(DataTable sourceTable)
		{
			if (sourceTable == null || sourceTable.Rows.Count == 0)
			{
				dgvCustomers.DataSource = null;
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

			dgvCustomers.DataSource = pagedTable;

			if (lblPageInfo != null)
			{
				lblPageInfo.Text = $"Page {currentPage} of {totalPages}";
			}

			// Toggle pagination button states if they exist
			if (btnFirst != null) btnFirst.Enabled = (currentPage > 1);
			if (btnPrev != null) btnPrev.Enabled = (currentPage > 1);
			if (btnNext != null) btnNext.Enabled = (currentPage < totalPages);
			if (btnLast != null) btnLast.Enabled = (currentPage < totalPages);
		}

		private void txtSearchFilter_TextChanged(object sender, EventArgs e)
		{
			LoadCustomersData(txtSearchFilter.Text.Trim());
		}

		private void btnAddCustomer_Click(object sender, EventArgs e)
		{
			frmManageCustomer customer = new frmManageCustomer();
			customer.ShowDialog();
			LoadCustomersData(); // Refresh list after adding
		}

		private void btnManageCustomer_Click(object sender, EventArgs e)
		{
			frmManageCustomer customer = new frmManageCustomer();
			customer.ShowDialog();
			LoadCustomersData(); // Refresh list after managing
		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			txtSearchFilter.Clear();
			LoadCustomersData();
			//MessageBox.Show("Customer list refreshed.", "Refreshed", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void btnExport_Click(object sender, EventArgs e)
		{
			try
			{
				if (masterDataTable != null && masterDataTable.Rows.Count > 0)
				{
					SaveFileDialog sfd = new SaveFileDialog();
					sfd.Filter = "CSV (*.csv)|*.csv";
					sfd.FileName = "Customer_Directory_" + DateTime.Now.ToString("yyyyMMdd") + ".csv";

					if (sfd.ShowDialog() == DialogResult.OK)
					{
						using (StreamWriter sw = new StreamWriter(sfd.FileName))
						{
							for (int i = 0; i < masterDataTable.Columns.Count; i++)
							{
								sw.Write(masterDataTable.Columns[i].ColumnName);
								if (i < masterDataTable.Columns.Count - 1) sw.Write(",");
							}
							sw.WriteLine();

							foreach (DataRow row in masterDataTable.Rows)
							{
								for (int i = 0; i < masterDataTable.Columns.Count; i++)
								{
									if (row[i] != null)
									{
										sw.Write('"' + row[i].ToString().Replace("\"", "\"\"") + '"');
									}
									if (i < masterDataTable.Columns.Count - 1) sw.Write(",");
								}
								sw.WriteLine();
							}
						}
						MessageBox.Show("Customer directory exported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
				else
				{
					MessageBox.Show("No customer records to export.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error exporting data: " + ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnPrev_Click(object sender, EventArgs e)
		{
			if (currentPage > 1)
			{
				currentPage--;
				DisplayPagedData(masterDataTable);
			}
		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			if (currentPage < totalPages)
			{
				currentPage++;
				DisplayPagedData(masterDataTable);
			}
		}

		private void btnFirst_Click(object sender, EventArgs e)
		{
			currentPage = 1;
			DisplayPagedData(masterDataTable);
		}

		private void btnLast_Click(object sender, EventArgs e)
		{
			currentPage = totalPages;
			DisplayPagedData(masterDataTable);
		}
	}
}