using System;
using System.Data;
using System.Windows.Forms;

namespace key_luxery1
{
	public partial class uManageUsers : UserControl
	{
		private sharedClass db = new sharedClass();
		private int currentPage = 1;
		private int pageSize = 20; // Number of users per page
		private int totalPages = 1;

		public uManageUsers()
		{
			InitializeComponent();
		}

		private void uManageUsers_Load(object sender, EventArgs e)
		{
			LoadUsersData();
		}

		public void LoadUsersData(string searchKeyword = "")
		{
			try
			{
				// 1. Build your base where clause
				string whereClause = "1=1";
				if (!string.IsNullOrEmpty(searchKeyword))
				{
					string safeKeyword = searchKeyword.Replace("'", "''");
					whereClause += $" AND (username LIKE '%{safeKeyword}%' OR role LIKE '%{safeKeyword}%' OR User_status LIKE '%{safeKeyword}%')";
				}

				// 2. Get total record count using sharedClass helper
				int totalRecords = sharedClass.GetTotalRecordCount("tblUser", whereClause);
				totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
				if (totalPages == 0) totalPages = 1;

				if (currentPage > totalPages) currentPage = totalPages;
				if (currentPage < 1) currentPage = 1;

				// 3. Fetch only the paged slice from the database
				// Note: Using subquery/aliasing or matching your original select columns
				string tableName = "(SELECT user_id AS [User ID], username AS [Username], role AS [Role], User_status AS [Status], date AS [Created Date] FROM tblUser) AS SubQuery";

				DataTable pagedTable = sharedClass.GetPagedData(tableName, "[Created Date] DESC", currentPage, pageSize, whereClause);

				// 4. Bind to DataGridView
				dgvUsers.DataSource = pagedTable;

				// 5. Update pagination UI controls
				if (lblPageInfo != null)
				{
					lblPageInfo.Text = $"Page {currentPage} of {totalPages}";
				}

				if (btnFirst != null) btnFirst.Enabled = (currentPage > 1);
				if (btnPrev != null) btnPrev.Enabled = (currentPage > 1);
				if (btnNext != null) btnNext.Enabled = (currentPage < totalPages);
				if (btnLast != null) btnLast.Enabled = (currentPage < totalPages);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error loading system users: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void txtSearchFilter_TextChanged(object sender, EventArgs e)
		{
			currentPage = 1; // Reset to first page on search
			LoadUsersData(txtSearchFilter.Text.Trim());
		}

		private void btnAddUser_Click(object sender, EventArgs e)
		{
			frmManageUser manageuser = new frmManageUser();
			manageuser.ShowDialog();
			LoadUsersData(txtSearchFilter.Text.Trim()); // Refresh list
		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			txtSearchFilter.Clear();
			currentPage = 1;
			LoadUsersData();
			MessageBox.Show("User list refreshed.", "Refreshed", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void btnFirst_Click(object sender, EventArgs e)
		{
			currentPage = 1;
			LoadUsersData(txtSearchFilter.Text.Trim());
		}

		private void btnPrev_Click(object sender, EventArgs e)
		{
			if (currentPage > 1)
			{
				currentPage--;
				LoadUsersData(txtSearchFilter.Text.Trim());
			}
		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			if (currentPage < totalPages)
			{
				currentPage++;
				LoadUsersData(txtSearchFilter.Text.Trim());
			}
		}

		private void btnLast_Click(object sender, EventArgs e)
		{
			currentPage = totalPages;
			LoadUsersData(txtSearchFilter.Text.Trim());
		}
	}
}