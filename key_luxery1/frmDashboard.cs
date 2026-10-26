using System;
using System.Drawing;
using System.Windows.Forms;

namespace key_luxery1
{
	public partial class frmDashboard : Form
	{
		public string UserRole { get; set; } = "Admin";

		public frmDashboard()
		{
			InitializeComponent();
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
			this.UpdateStyles();
		}

		private void frmDashboard_Load(object sender, EventArgs e)
		{
			lblDate.Text = "Date: " + DateTime.Now.ToString("yyyy-MM-dd");
			Properties.Settings.Default.userRole = UserRole; // Store role in settings for child controls
			ApplyUserPermissions();
		}

		#region Role-Based Access Control

		public void ApplyUserPermissions()
		{
			bool isAdmin = string.Equals(UserRole, "Admin", StringComparison.OrdinalIgnoreCase);

			// Hide Admin-only sidebar and top buttons for non-admins
			BtnDashboard.Visible = isAdmin;
			PnlDashboard.Visible = isAdmin;
			btnExpense.Visible = isAdmin;
			pnlExpense.Visible = isAdmin;
			btnReport.Visible = isAdmin;
			pnlReport.Visible = isAdmin;
			BtnUsers.Visible = isAdmin;
			PnlUsers.Visible = isAdmin;

			btnProducts.Visible = isAdmin;
			pnlProducts.Visible = isAdmin;
			btnPayments.Visible = isAdmin;
			pnlPayment.Visible = isAdmin;
			btnCustomer.Visible = isAdmin;
			pnlCustomer.Visible = isAdmin;

			if (!isAdmin)
			{
				// Reposition Non-Admin top tabs cleanly
				btnSales.Location = new Point(241, 43);
				pnlSale.Location = new Point(241, 86);
				btnInvoice.Location = new Point(373, 43);
				pnlInvoice.Location = new Point(373, 86);

				// Default view for non-admin
				LoadUserControl(new uSales(), pnlSale);
			}
			else
			{
				// Default view for admin
				LoadUserControl(new uHome(), PnlDashboard);
			}
		}

		#endregion

		#region High-Performance Dynamic Navigation (Lazy Loading)

		private void LoadUserControl(UserControl uc, Panel activeIndicator)
		{
			// Reset all indicator panels visibility quickly
			foreach (Control ctrl in pnlTopNav.Controls)
			{
				if (ctrl is Panel p && p != pnlTopNav) p.Visible = false;
			}
			foreach (Control ctrl in PnlSidebar.Controls)
			{
				if (ctrl is Panel p) p.Visible = false;
			}

			if (activeIndicator != null)
				activeIndicator.Visible = true;

			// Inject user control into main content area dynamically
			pnlMainContent.Controls.Clear();
			uc.Dock = DockStyle.Fill;

			// Pass role down if control supports it (e.g., uInvoice)
			if (uc is uInvoice inv) inv.UserRole = this.UserRole;

			pnlMainContent.Controls.Add(uc);
			uc.BringToFront();
		}

		#endregion

		#region Navigation Click Events

		private void BtnDashboard_Click(object sender, EventArgs e) => LoadUserControl(new uHome(), PnlDashboard);
		// Inside frmDashboard navigation click event
		private void btnSales_Click(object sender, EventArgs e)
		{
			// Instantiates uSales ONLY on click, keeping login & dashboard startup instantaneous
			LoadUserControl(new uSales(), pnlSale);
		}
		private void btnProducts_Click(object sender, EventArgs e) => LoadUserControl(new uProducts(), pnlProducts);
		private void btnInvoice_Click(object sender, EventArgs e) => LoadUserControl(new uInvoice(), pnlInvoice);
		private void btnPayments_Click(object sender, EventArgs e) => LoadUserControl(new uPayments(), pnlPayment);
		private void btnCustomer_Click(object sender, EventArgs e) => LoadUserControl(new uCustomer(), pnlCustomer);
		private void BtnUsers_Click(object sender, EventArgs e) => LoadUserControl(new uManageUsers(), PnlUsers);
		private void BtnSetting_Click(object sender, EventArgs e) => LoadUserControl(new uSettings(), PnlSetting);
		private void BtnAbout_Click(object sender, EventArgs e) => LoadUserControl(new uAbout(), PnlAbout);

		private void btnExpense_Click(object sender, EventArgs e)
		{
			using (var expense = new frmExpense()) { expense.ShowDialog(); }
		}

		private void btnReport_Click(object sender, EventArgs e)
		{
			using (var report = new frmReport()) { report.ShowDialog(); }
		}

		private void BtnSidebar_Click(object sender, EventArgs e)
		{
			PnlSidebar.Width = (PnlSidebar.Width >= 203) ? 0 : 203;
		}

		#endregion

		#region Logout & Password Handling

		private void btnLogout_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to log out?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				Form loginForm = Application.OpenForms["frmLogin"];
				if (loginForm != null) loginForm.Show();
				this.Hide();
			}
		}

		private void btnChangePassword_Click(object sender, EventArgs e)
		{
			try
			{
				using (var changePass = new frmChangePassword())
				{
					changePass.txtUsername.Text = lblUsername.Text;
					changePass.ShowDialog();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void frmDashboard_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
		}

		#endregion
	}
}