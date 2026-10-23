using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace key_luxery1
{
	public partial class frmChangePassword : Form
	{
		// Public property to receive the username from the Dashboard when opening this form
		public string LoggedUsername { get; set; } = "";

		sharedClass db = new sharedClass();

		public frmChangePassword()
		{
			InitializeComponent();
		}

		private void frmChangePassword_Load(object sender, EventArgs e)
		{
			// Populate the disabled username textbox when the form loads
			txtUsername.Text = LoggedUsername;
		}

		private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
		{
			// Toggle password character visibility
			char passwordChar = chkShowPassword.Checked ? '\0' : '●';
			txtCurrentPassword.PasswordChar = passwordChar;
			txtNewPassword.PasswordChar = passwordChar;
			txtConfirmPassword.PasswordChar = passwordChar;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnChangePassword_Click(object sender, EventArgs e)
		{
			// 1. Get values from textboxes
			string currentPassword = txtCurrentPassword.Text.Trim();
			string newPassword = txtNewPassword.Text.Trim();
			string confirmPassword = txtConfirmPassword.Text.Trim();

			// 2. Validate inputs
			if (string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
			{
				MessageBox.Show("Please fill in all password fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (newPassword != confirmPassword)
			{
				MessageBox.Show("New passwords do not match. Please re-type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtNewPassword.Clear();
				txtConfirmPassword.Clear();
				txtNewPassword.Focus();
				return;
			}

			try
			{
				// 3. Use the username from txtUsername or LoggedUsername property
				string currentUsername = txtUsername.Text.Replace("@", "").Trim();

				if (string.IsNullOrEmpty(currentUsername))
				{
					MessageBox.Show("User session error. Please log in again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				string query = $"SELECT passWord FROM tblUser WHERE userName = '{currentUsername.Replace("'", "''")}'";
				DataTable dt = sharedClass.GetData(query);

				if (dt.Rows.Count > 0)
				{
					string encryptedDbPassword = dt.Rows[0]["passWord"]?.ToString();
					string decryptedDbPassword = Decrypt(encryptedDbPassword);

					// 4. Verify if the current password entered matches the database
					if (decryptedDbPassword == currentPassword)
					{
						// 5. Encrypt the new password
						string encryptedNewPassword = Encrypt(newPassword);

						// 6. Update the database with the new encrypted password
						string updateQuery = $"UPDATE tblUser SET passWord = '{encryptedNewPassword.Replace("'", "''")}' WHERE userName = '{currentUsername.Replace("'", "''")}'";

						db.ExecuteQueries(updateQuery);

						MessageBox.Show("Password changed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

						// Clear fields and close form after success
						txtCurrentPassword.Clear();
						txtNewPassword.Clear();
						txtConfirmPassword.Clear();
						this.Close();
					}
					else
					{
						MessageBox.Show("Incorrect current password. Please try again.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
						txtCurrentPassword.Clear();
						txtCurrentPassword.Focus();
					}
				}
				else
				{
					MessageBox.Show("User not found in the system.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error changing password: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private string Decrypt(string encryptedText)
		{
			if (string.IsNullOrEmpty(encryptedText))
				return string.Empty;

			string key = "0123456789abcdef0123456789abcdef";
			byte[] keyBytes = Encoding.UTF8.GetBytes(key);
			byte[] iv = new byte[16];

			using (Aes aes = Aes.Create())
			{
				aes.Key = keyBytes;
				aes.IV = iv;

				ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

				try
				{
					byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
					using (MemoryStream ms = new MemoryStream(encryptedBytes))
					using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
					using (StreamReader sr = new StreamReader(cs))
					{
						return sr.ReadToEnd();
					}
				}
				catch
				{
					// Fallback if an old password was saved in plain-text
					return encryptedText;
				}
			}
		}

		private string Encrypt(string plainText)
		{
			if (string.IsNullOrEmpty(plainText))
				return string.Empty;

			string key = "0123456789abcdef0123456789abcdef";
			byte[] keyBytes = Encoding.UTF8.GetBytes(key);
			byte[] iv = new byte[16];

			using (Aes aes = Aes.Create())
			{
				aes.Key = keyBytes;
				aes.IV = iv;

				ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
				using (MemoryStream ms = new MemoryStream())
				using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
				using (StreamWriter sw = new StreamWriter(cs))
				{
					sw.Write(plainText);
					sw.Close();
					return Convert.ToBase64String(ms.ToArray());
				}
			}
		}
	}
}