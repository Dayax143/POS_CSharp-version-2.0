using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace key_luxery1
{
	partial class frmChangePassword
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pnlHeader = new System.Windows.Forms.Panel();
			this.lblTitle = new System.Windows.Forms.Label();
			this.lblUsername = new System.Windows.Forms.Label();
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.lblCurrentPassword = new System.Windows.Forms.Label();
			this.txtCurrentPassword = new System.Windows.Forms.TextBox();
			this.lblNewPassword = new System.Windows.Forms.Label();
			this.txtNewPassword = new System.Windows.Forms.TextBox();
			this.lblConfirmPassword = new System.Windows.Forms.Label();
			this.txtConfirmPassword = new System.Windows.Forms.TextBox();
			this.chkShowPassword = new System.Windows.Forms.CheckBox();
			this.btnChangePassword = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.pnlHeader.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlHeader
			// 
			this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
			this.pnlHeader.Controls.Add(this.lblTitle);
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlHeader.Location = new System.Drawing.Point(0, 0);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Size = new System.Drawing.Size(460, 60);
			this.pnlHeader.TabIndex = 0;
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTitle.ForeColor = System.Drawing.Color.White;
			this.lblTitle.Location = new System.Drawing.Point(20, 15);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(232, 31);
			this.lblTitle.TabIndex = 0;
			this.lblTitle.Text = "🔒 Change Password";
			// 
			// lblUsername
			// 
			this.lblUsername.AutoSize = true;
			this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 10F);
			this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.lblUsername.Location = new System.Drawing.Point(35, 75);
			// Fix for the errors in the lblUsername.Name assignment
			this.lblUsername.Name = "lblUsername";
			this.lblUsername.Size = new System.Drawing.Size(91, 23);
			this.lblUsername.TabIndex = 1;
			this.lblUsername.Text = "Username:";
			// 
			// txtUsername
			// 
			this.txtUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
			this.txtUsername.Enabled = false;
			this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 11F);
			this.txtUsername.Location = new System.Drawing.Point(39, 102);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(380, 32);
			this.txtUsername.TabIndex = 2;
			// 
			// lblCurrentPassword
			// 
			this.lblCurrentPassword.AutoSize = true;
			this.lblCurrentPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
			this.lblCurrentPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.lblCurrentPassword.Location = new System.Drawing.Point(35, 145);
			this.lblCurrentPassword.Name = "lblCurrentPassword";
			this.lblCurrentPassword.Size = new System.Drawing.Size(147, 23);
			this.lblCurrentPassword.TabIndex = 3;
			this.lblCurrentPassword.Text = "Current Password:";
			// 
			// txtCurrentPassword
			// 
			this.txtCurrentPassword.Font = new System.Drawing.Font("Segoe UI", 11F);
			this.txtCurrentPassword.Location = new System.Drawing.Point(39, 172);
			this.txtCurrentPassword.Name = "txtCurrentPassword";
			this.txtCurrentPassword.PasswordChar = '●';
			this.txtCurrentPassword.Size = new System.Drawing.Size(380, 32);
			this.txtCurrentPassword.TabIndex = 4;
			// 
			// lblNewPassword
			// 
			this.lblNewPassword.AutoSize = true;
			this.lblNewPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
			this.lblNewPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.lblNewPassword.Location = new System.Drawing.Point(35, 215);
			this.lblNewPassword.Name = "lblNewPassword";
			this.lblNewPassword.Size = new System.Drawing.Size(123, 23);
			this.lblNewPassword.TabIndex = 5;
			this.lblNewPassword.Text = "New Password:";
			// 
			// txtNewPassword
			// 
			this.txtNewPassword.Font = new System.Drawing.Font("Segoe UI", 11F);
			this.txtNewPassword.Location = new System.Drawing.Point(39, 242);
			this.txtNewPassword.Name = "txtNewPassword";
			this.txtNewPassword.PasswordChar = '●';
			this.txtNewPassword.Size = new System.Drawing.Size(380, 32);
			this.txtNewPassword.TabIndex = 6;
			// 
			// lblConfirmPassword
			// 
			this.lblConfirmPassword.AutoSize = true;
			this.lblConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
			this.lblConfirmPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.lblConfirmPassword.Location = new System.Drawing.Point(35, 285);
			this.lblConfirmPassword.Name = "lblConfirmPassword";
			this.lblConfirmPassword.Size = new System.Drawing.Size(189, 23);
			this.lblConfirmPassword.TabIndex = 7;
			this.lblConfirmPassword.Text = "Confirm New Password:";
			// 
			// txtConfirmPassword
			// 
			this.txtConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 11F);
			this.txtConfirmPassword.Location = new System.Drawing.Point(39, 312);
			this.txtConfirmPassword.Name = "txtConfirmPassword";
			this.txtConfirmPassword.PasswordChar = '●';
			this.txtConfirmPassword.Size = new System.Drawing.Size(380, 32);
			this.txtConfirmPassword.TabIndex = 8;
			// 
			// chkShowPassword
			// 
			this.chkShowPassword.AutoSize = true;
			this.chkShowPassword.Font = new System.Drawing.Font("Segoe UI", 9.5F);
			this.chkShowPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
			this.chkShowPassword.Location = new System.Drawing.Point(39, 355);
			this.chkShowPassword.Name = "chkShowPassword";
			this.chkShowPassword.Size = new System.Drawing.Size(139, 25);
			this.chkShowPassword.TabIndex = 9;
			this.chkShowPassword.Text = "Show Passwords";
			this.chkShowPassword.UseVisualStyleBackColor = true;
			this.chkShowPassword.CheckedChanged += new System.EventHandler(this.chkShowPassword_CheckedChanged);
			// 
			// btnChangePassword
			// 
			this.btnChangePassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
			this.btnChangePassword.FlatAppearance.BorderSize = 0;
			this.btnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnChangePassword.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
			this.btnChangePassword.ForeColor = System.Drawing.Color.White;
			this.btnChangePassword.Location = new System.Drawing.Point(39, 400);
			this.btnChangePassword.Name = "btnChangePassword";
			this.btnChangePassword.Size = new System.Drawing.Size(250, 42);
			this.btnChangePassword.TabIndex = 10;
			this.btnChangePassword.Text = "Update Password";
			this.btnChangePassword.UseVisualStyleBackColor = false;
			this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
			this.btnCancel.FlatAppearance.BorderSize = 0;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
			this.btnChangePassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.btnCancel.Location = new System.Drawing.Point(299, 400);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(120, 42);
			this.btnCancel.TabIndex = 11;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// frmChangePassword
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
			this.ClientSize = new System.Drawing.Size(460, 470);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnChangePassword);
			this.Controls.Add(this.chkShowPassword);
			this.Controls.Add(this.txtConfirmPassword);
			this.Controls.Add(this.lblConfirmPassword);
			this.Controls.Add(this.txtNewPassword);
			this.Controls.Add(this.lblNewPassword);
			this.Controls.Add(this.txtCurrentPassword);
			this.Controls.Add(this.lblCurrentPassword);
			this.Controls.Add(this.txtUsername);
			this.Controls.Add(this.lblUsername);
			this.Controls.Add(this.pnlHeader);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmChangePassword";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Key Luxury - Change Password";
			this.pnlHeader.ResumeLayout(false);
			this.pnlHeader.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel pnlHeader;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.Label lblUsername;
		public System.Windows.Forms.TextBox txtUsername;
		private System.Windows.Forms.Label lblCurrentPassword;
		private System.Windows.Forms.TextBox txtCurrentPassword;
		private System.Windows.Forms.Label lblNewPassword;
		private System.Windows.Forms.TextBox txtNewPassword;
		private System.Windows.Forms.Label lblConfirmPassword;
		private System.Windows.Forms.TextBox txtConfirmPassword;
		private System.Windows.Forms.CheckBox chkShowPassword;
		private System.Windows.Forms.Button btnChangePassword;
		private System.Windows.Forms.Button btnCancel;
	}
}