using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace key_luxery1
{
	public partial class frmLogin : Form
	{
		private readonly sharedClass db = new sharedClass();
		private static readonly string SecretKey = "0123456789abcdef0123456789abcdef"; // 32 bytes AES-256

		public frmLogin() => InitializeComponent();
		private void lnkForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			frmForgotPassword forgotForm = new frmForgotPassword();
			forgotForm.ShowDialog();
		}
		public void loginFunction()
		{
			string username = TxtUser.Text.Trim();
			string password = TxtPassword.Text.Trim();

			if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
			{
				MessageBox.Show("Please enter both username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			try
			{
				// Reverted to your standard sharedClass query approach
				string query = $"SELECT userName, passWord, role, user_Status FROM tblUser WHERE userName = '{username.Replace("'", "''")}'";
				DataTable dt = sharedClass.GetData(query);

				if (dt != null && dt.Rows.Count > 0)
				{
					DataRow row = dt.Rows[0];
					string encryptedDbPassword = row["passWord"]?.ToString();
					string userStatus = row["user_Status"]?.ToString();
					string role = row["role"]?.ToString();

					bool isActive = string.Equals(userStatus, "Active", StringComparison.OrdinalIgnoreCase) ||
									string.Equals(userStatus, "1", StringComparison.OrdinalIgnoreCase) ||
									string.Equals(userStatus, "True", StringComparison.OrdinalIgnoreCase);

					if (!isActive)
					{
						MessageBox.Show("Your account is inactive or disabled. Contact administrator.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}

					string decryptedDbPassword = Decrypt(encryptedDbPassword);

					if (decryptedDbPassword == password)
					{
						frmDashboard dashboard = new frmDashboard
						{
							UserRole = role
						};

						if (role == "Admin")
						{
							dashboard.PnlSidebar.Visible = true;
							dashboard.BtnSidebar.Visible = true;
						}
						else
						{
							dashboard.PnlSidebar.Visible = false;
							dashboard.BtnSidebar.Visible = false;
						}

						//MessageBox.Show($"Welcome back, {username}!\nRole: {role}", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

						dashboard.lblUsername.Text = username;
						dashboard.lblRole.Text = role;
						dashboard.Show();
						this.Hide();
					}
					else
					{
						MessageBox.Show("Invalid password. Please try again.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
						TxtPassword.Clear();
						TxtPassword.Focus();
					}
				}
				else
				{
					MessageBox.Show("Username not found in the system.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
					TxtUser.Focus();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Login error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public string Decrypt(string cipherText)
		{
			if (string.IsNullOrEmpty(cipherText)) return cipherText;
			try
			{
				byte[] iv = new byte[16];
				byte[] buffer = Convert.FromBase64String(cipherText);

				using (Aes aes = Aes.Create())
				{
					aes.Key = Encoding.UTF8.GetBytes(SecretKey);
					aes.IV = iv;
					using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
					using (var ms = new MemoryStream(buffer))
					using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
					using (var sr = new StreamReader(cs))
					{
						return sr.ReadToEnd();
					}
				}
			}
			catch
			{
				return cipherText; // Fallback for legacy plain text passwords
			}
		}

		private void btnConfig_Click(object sender, EventArgs e)
		{
			using (var configForm = new frmConfig())
			{
				configForm.ShowDialog();
			}
		}	

		private void btnLogin_Click(object sender, EventArgs e) => loginFunction();

		private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
		{
			TxtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
		}

		private void btnExit_Click(object sender, EventArgs e) => Application.Exit();

		private void frmLogin_FormClosed(object sender, FormClosedEventArgs e) => Application.Exit();

		private void frmLogin_Load(object sender, EventArgs e)
		{
			TxtPassword.Clear();
			TxtUser.Clear();
			TxtUser.Focus();
		}

		private void txtPassword_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				e.SuppressKeyPress = true;
				loginFunction();
			}
		}
	}
}