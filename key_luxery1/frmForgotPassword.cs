using System;
using System.Data;
using System.Windows.Forms;

namespace key_luxery1
{
	public partial class frmForgotPassword : Form
	{
		// Variable to hold the expected answer retrieved from DB
		private string expectedAnswer = string.Empty;
		private bool isAnswerVerified = false;

		sharedClass db = new sharedClass();
		public frmForgotPassword()
		{
			InitializeComponent();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		// Step 1: Find username & fetch recovery question
		private void btnVerifyUser_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtUsername.Text))
			{
				MessageBox.Show("Please enter your username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtUsername.Focus();
				return;
			}

			 //TODO: Fetch recovery question and answer from database based on txtUsername.Text
			// Example placeholder query logic:
			//DataTable dt = sharedClass.ExecuteQueries($"SELECT SecurityQuestion, SecurityAnswer FROM Users WHERE Username = '{txtUsername.Text}'");

			// Demo/Mock Check for illustration:
			if (txtUsername.Text.Trim().ToLower() == "admin")
			{
				txtRecoveryQuestion.Text = "What is the name of your first pet?";
				expectedAnswer = "Fluffy"; // Replace with actual DB query result
			}
			else
			{
				MessageBox.Show("Username not found. Please verify your entry.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				txtRecoveryQuestion.Clear();
				expectedAnswer = string.Empty;
			}
		}

		// Step 2: Validate Answer & Update Password
		private void btnReset_Click(object sender, EventArgs e)
		{
			// Check if username and question are loaded
			if (string.IsNullOrWhiteSpace(txtRecoveryQuestion.Text))
			{
				MessageBox.Show("Please enter a valid username and click 'Find' first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			// Check security answer
			if (string.IsNullOrWhiteSpace(txtAnswer.Text))
			{
				MessageBox.Show("Please enter your security answer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtAnswer.Focus();
				return;
			}

			// Validate Answer (Case-Insensitive)
			if (txtAnswer.Text.Trim().Equals(expectedAnswer, StringComparison.OrdinalIgnoreCase))
			{
				// Enable password fields if verified for the first time
				if (!isAnswerVerified)
				{
					isAnswerVerified = true;
					txtNewPassword.Enabled = true;
					txtConfirmPassword.Enabled = true;
					txtUsername.Enabled = false;
					txtAnswer.Enabled = false;
					btnVerifyUser.Enabled = false;

					MessageBox.Show("Security Answer Verified! Please enter your new password below.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
					txtNewPassword.Focus();
					return;
				}
			}
			else
			{
				MessageBox.Show("Incorrect Security Answer. Please try again.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
				txtAnswer.Clear();
				txtAnswer.Focus();
				return;
			}

			// Perform Password Reset when enabled
			if (isAnswerVerified)
			{
				if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
				{
					MessageBox.Show("Please enter a new password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtNewPassword.Focus();
					return;
				}

				if (txtNewPassword.Text != txtConfirmPassword.Text)
				{
					MessageBox.Show("Passwords do not match. Please re-enter.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtConfirmPassword.Clear();
					txtConfirmPassword.Focus();
					return;
				}

				// TODO: Execute DB Update query here
				// Example: Database.ExecuteNonQuery($"UPDATE Users SET Password='{txtNewPassword.Text}' WHERE Username='{txtUsername.Text}'");

				MessageBox.Show("Your password has been successfully renewed!", "Password Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
				this.Close();
			}
		}
	}
}