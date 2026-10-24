using System;
using System.Windows.Forms;

namespace key_luxery1
{
	public partial class frmForgotPassword : Form
	{
		public frmForgotPassword()
		{
			InitializeComponent();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnReset_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtUsername.Text))
			{
				MessageBox.Show("Please enter your username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtUsername.Focus();
				return;
			}

			if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
			{
				MessageBox.Show("Please enter a new password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtNewPassword.Focus();
				return;
			}

			if (txtNewPassword.Text != txtConfirmPassword.Text)
			{
				MessageBox.Show("Passwords do not match. Please try again.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtConfirmPassword.Clear();
				txtConfirmPassword.Focus();
				return;
			}

			// TODO: Add your database update logic here to update the user's password in your database.
			// Example: DatabaseHelper.UpdatePassword(txtUsername.Text, txtNewPassword.Text);

			MessageBox.Show("Password reset successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
			this.Close();
		}
	}
}