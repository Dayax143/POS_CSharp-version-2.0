using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace key_luxery1
{
	public class sharedClass
	{
		// Properties kept to maintain class state
		public string query;
		public DialogResult confirm;
		public string message = "Are you sure you want to delete this record";
		public string title = "Confirmation dialog";

		// Get the connection string from your project settings
		public static SqlConnection GetConnection()
		{
			return new SqlConnection(Properties.Settings.Default.SqlConnectionString);
		}

		// Add this ExecuteScalar method to your SharedClass
		public object ExecuteScalar(string query)
		{
			object result = null;

			using (SqlConnection conn = GetConnection())
			{
				using (SqlCommand cmd = new SqlCommand(query, conn))
				{
					try
					{
						conn.Open();
						result = cmd.ExecuteScalar();
					}
					catch (Exception ex)
					{
						throw new Exception("Error executing scalar query: " + ex.Message);
					}
				}
			}

			return result;
		}

		// Method for executing non-query commands (INSERT, UPDATE, DELETE)
		public void ExecuteQueries(string _query)
		{
			try
			{
				using (SqlConnection con = GetConnection())
				{
					con.Open();
					SqlCommand cmd = new SqlCommand(_query, con);
					cmd.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Consolidated method to display data in a DataGridView
		public static void DisplayData(DataGridView dgv, string _query)
		{
			try
			{
				using (SqlDataAdapter adapter = new SqlDataAdapter(_query, GetConnection()))
				{
					DataTable virtualTable = new DataTable();
					adapter.Fill(virtualTable);
					dgv.DataSource = virtualTable;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Added feature: Returns a DataTable for dropdown data binding (e.g., Customers, Products)
		public static DataTable GetData(string _query)
		{
			DataTable dt = new DataTable();
			try
			{
				using (SqlConnection con = GetConnection())
				{
					using (SqlDataAdapter adapter = new SqlDataAdapter(_query, con))
					{
						adapter.Fill(dt);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			return dt;
		}

		// Execute scalar queries (e.g., COUNT, SUM)
		public object ExecuteScalarQuery(string _query)
		{
			try
			{
				using (SqlConnection con = GetConnection())
				{
					con.Open();
					SqlCommand cmd = new SqlCommand(_query, con);
					return cmd.ExecuteScalar();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return null;
			}
		}

		// ... (Keep your existing connection string, GetData, ExecuteQueries, etc.)

		/// <summary>
		/// Retrieves a specific slice of data (pagination) directly using SQL OFFSET-FETCH 
		/// (Requires SQL Server 2012 or newer).
		/// </summary>
		/// <param name="tableName">The name of the table or a base query string (without ORDER BY)</param>
		/// <param name="orderByColumn">The column name to sort by</param>
		/// <param name="pageNumber">Current page index (1-based)</param>
		/// <param name="pageSize">Number of records per page</param>
		/// <param name="whereClause">Optional WHERE filter (e.g., "deleted = 0 AND status = 'Active'")</param>
		/// <returns>A DataTable containing only the rows for the requested page</returns>
		public static DataTable GetPagedData(string tableName, string orderByColumn, int pageNumber, int pageSize, string whereClause = "")
		{
			DataTable dt = new DataTable();

			// Ensure pageNumber and pageSize are valid
			if (pageNumber < 1) pageNumber = 1;
			if (pageSize < 1) pageSize = 20;

			int offset = (pageNumber - 1) * pageSize;

			string query = $"SELECT * FROM {tableName}";

			if (!string.IsNullOrEmpty(whereClause))
			{
				query += $" WHERE {whereClause}";
			}

			// SQL Server pagination clause (OFFSET / FETCH)
			query += $" ORDER BY {orderByColumn} OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY;";

			dt = GetData(query);
			return dt;
		}

		/// <summary>
		/// Gets the total count of records for pagination calculations.
		/// </summary>
		public static int GetTotalRecordCount(string tableName, string whereClause = "")
		{
			try
			{
				string query = $"SELECT COUNT(*) FROM {tableName}";
				if (!string.IsNullOrEmpty(whereClause))
				{
					query += $" WHERE {whereClause}";
				}

				DataTable dt = GetData(query);
				if (dt != null && dt.Rows.Count > 0)
				{
					return Convert.ToInt32(dt.Rows[0][0]);
				}
			}
			catch
			{
				// Return 0 if count fails
			}
			return 0;
		}

		// Clear simple input controls
		public void clearData(params Control[] controls)
		{
			try
			{
				foreach (Control control in controls)
				{
					if (control is TextBox textBox)
					{
						textBox.Clear();
					}
					else if (control is ComboBox comboBox)
					{
						comboBox.SelectedIndex = -1;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}