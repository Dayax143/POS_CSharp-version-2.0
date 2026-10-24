namespace key_luxery1
{
	partial class frmForgotPassword
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
			this.pnlBrand = new System.Windows.Forms.Panel();
			this.lblBrandTitle = new System.Windows.Forms.Label();
			this.lblBrandSubtitle = new System.Windows.Forms.Label();
			this.lblHeader = new System.Windows.Forms.Label();
			this.lblUser = new System.Windows.Forms.Label();
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.lblNewPass = new System.Windows.Forms.Label();
			this.txtNewPassword = new System.Windows.Forms.TextBox();
			this.lblConfirmPass = new System.Windows.Forms.Label();
			this.txtConfirmPassword = new System.Windows.Forms.TextBox();
			this.btnReset = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.pnlBrand.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlBrand
			// 
			this.pnlBrand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.pnlBrand.Controls.Add(this.lblBrandTitle);
			this.pnlBrand.Controls.Add(this.lblBrandSubtitle);
			this.pnlBrand.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlBrand.Location = new System.Drawing.Point(0, 0);
			this.pnlBrand.Name = "pnlBrand";
			this.pnlBrand.Size = new System.Drawing.Size(260, 420);
			this.pnlBrand.TabIndex = 0;
			// 
			// lblBrandTitle
			// 
			this.lblBrandTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
			this.lblBrandTitle.ForeColor = System.Drawing.Color.White;
			this.lblBrandTitle.Location = new System.Drawing.Point(20, 150);
			this.lblBrandTitle.Name = "lblBrandTitle";
			this.lblBrandTitle.Size = new System.Drawing.Size(220, 80);
			this.lblBrandTitle.TabIndex = 0;
			this.lblBrandTitle.Text = "KEY LUXURY";
			this.lblBrandTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblBrandSubtitle
			// 
			this.lblBrandSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
			this.lblBrandSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
			this.lblBrandSubtitle.Location = new System.Drawing.Point(20, 230);
			this.lblBrandSubtitle.Name = "lblBrandSubtitle";
			this.lblBrandSubtitle.Size = new System.Drawing.Size(220, 40);
			this.lblBrandSubtitle.TabIndex = 1;
			this.lblBrandSubtitle.Text = "Password Recovery";
			this.lblBrandSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblHeader
			// 
			this.lblHeader.AutoSize = true;
			this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
			this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.lblHeader.Location = new System.Drawing.Point(310, 35);
			this.lblHeader.Name = "lblHeader";
			this.lblHeader.Size = new System.Drawing.Size(217, 37);
			this.lblHeader.TabIndex = 1;
			this.lblHeader.Text = "Reset Password";
			// 
			// lblUser
			// 
			this.lblUser.AutoSize = true;
			this.lblUser.Font = new System.Drawing.Font("Segoe UI", 9.5F);
			this.lblUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.lblUser.Location = new System.Drawing.Point(310, 90);
			this.lblUser.Name = "lblUser";
			this.lblUser.Size = new System.Drawing.Size(81, 21);
			this.lblUser.TabIndex = 2;
			this.lblUser.Text = "Username";
			// 
			// txtUsername
			// 
			this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 10.5F);
			this.txtUsername.Location = new System.Drawing.Point(310, 113);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(325, 31);
			this.txtUsername.TabIndex = 3;
			// 
			// lblNewPass
			// 
			this.lblNewPass.AutoSize = true;
			this.lblNewPass.Font = new System.Drawing.Font("Segoe UI", 9.5F);
			this.lblNewPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.lblNewPass.Location = new System.Drawing.Point(310, 155);
			this.lblNewPass.Name = "lblNewPass";
			this.lblNewPass.Size = new System.Drawing.Size(113, 21);
			this.lblNewPass.TabIndex = 4;
			this.lblNewPass.Text = "New Password";
			// 
			// txtNewPassword
			// 
			this.txtNewPassword.Font = new System.Drawing.Font("Segoe UI", 10.5F);
			this.txtNewPassword.Location = new System.Drawing.Point(310, 178);
			this.txtNewPassword.Name = "txtNewPassword";
			this.txtNewPassword.Size = new System.Drawing.Size(325, 31);
			this.txtNewPassword.TabIndex = 5;
			this.txtNewPassword.UseSystemPasswordChar = true;
			// 
			// lblConfirmPass
			// 
			this.lblConfirmPass.AutoSize = true;
			this.lblConfirmPass.Font = new System.Drawing.Font("Segoe UI", 9.5F);
			this.lblConfirmPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.lblConfirmPass.Location = new System.Drawing.Point(310, 220);
			this.lblConfirmPass.Name = "lblConfirmPass";
			this.lblConfirmPass.Size = new System.Drawing.Size(138, 21);
			this.lblConfirmPass.TabIndex = 6;
			this.lblConfirmPass.Text = "Confirm Password";
			// 
			// txtConfirmPassword
			// 
			this.txtConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 10.5F);
			this.txtConfirmPassword.Location = new System.Drawing.Point(310, 243);
			this.txtConfirmPassword.Name = "txtConfirmPassword";
			this.txtConfirmPassword.Size = new System.Drawing.Size(325, 31);
			this.txtConfirmPassword.TabIndex = 7;
			this.txtConfirmPassword.UseSystemPasswordChar = true;
			// 
			// btnReset
			// 
			this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
			this.btnReset.FlatAppearance.BorderSize = 0;
			this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnReset.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
			this.btnReset.ForeColor = System.Drawing.Color.White;
			this.btnReset.Location = new System.Drawing.Point(310, 338);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(150, 40);
			this.btnReset.TabIndex = 8;
			this.btnReset.Text = "Reset";
			this.btnReset.UseVisualStyleBackColor = false;
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
			this.btnCancel.FlatAppearance.BorderSize = 0;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
			this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.btnCancel.Location = new System.Drawing.Point(485, 338);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(150, 40);
			this.btnCancel.TabIndex = 9;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// frmForgotPassword
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
			this.ClientSize = new System.Drawing.Size(682, 420);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnReset);
			this.Controls.Add(this.txtConfirmPassword);
			this.Controls.Add(this.lblConfirmPass);
			this.Controls.Add(this.txtNewPassword);
			this.Controls.Add(this.lblNewPass);
			this.Controls.Add(this.txtUsername);
			this.Controls.Add(this.lblUser);
			this.Controls.Add(this.lblHeader);
			this.Controls.Add(this.pnlBrand);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "frmForgotPassword";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Key Luxury - Password Reset";
			this.pnlBrand.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel pnlBrand;
		private System.Windows.Forms.Label lblBrandTitle;
		private System.Windows.Forms.Label lblBrandSubtitle;
		private System.Windows.Forms.Label lblHeader;
		private System.Windows.Forms.Label lblUser;
		public System.Windows.Forms.TextBox txtUsername;
		private System.Windows.Forms.Label lblNewPass;
		public System.Windows.Forms.TextBox txtNewPassword;
		private System.Windows.Forms.Label lblConfirmPass;
		public System.Windows.Forms.TextBox txtConfirmPassword;
		public System.Windows.Forms.Button btnReset;
		public System.Windows.Forms.Button btnCancel;
	}
}