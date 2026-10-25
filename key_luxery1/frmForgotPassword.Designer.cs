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
			this.btnVerifyUser = new System.Windows.Forms.Button();
			this.lblRecoveryQuestion = new System.Windows.Forms.Label();
			this.txtRecoveryQuestion = new System.Windows.Forms.TextBox();
			this.lblAnswer = new System.Windows.Forms.Label();
			this.txtAnswer = new System.Windows.Forms.TextBox();
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
			this.pnlBrand.Size = new System.Drawing.Size(260, 495);
			this.pnlBrand.TabIndex = 0;
			// 
			// lblBrandTitle
			// 
			this.lblBrandTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
			this.lblBrandTitle.ForeColor = System.Drawing.Color.White;
			this.lblBrandTitle.Location = new System.Drawing.Point(20, 180);
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
			this.lblBrandSubtitle.Location = new System.Drawing.Point(20, 260);
			this.lblBrandSubtitle.Name = "lblBrandSubtitle";
			this.lblBrandSubtitle.Size = new System.Drawing.Size(220, 40);
			this.lblBrandSubtitle.TabIndex = 1;
			this.lblBrandSubtitle.Text = "Account Recovery";
			this.lblBrandSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblHeader
			// 
			this.lblHeader.AutoSize = true;
			this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
			this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.lblHeader.Location = new System.Drawing.Point(310, 20);
			this.lblHeader.Name = "lblHeader";
			this.lblHeader.Size = new System.Drawing.Size(216, 37);
			this.lblHeader.TabIndex = 1;
			this.lblHeader.Text = "Reset Password";
			// 
			// lblUser
			// 
			this.lblUser.AutoSize = true;
			this.lblUser.Font = new System.Drawing.Font("Segoe UI", 9.5F);
			this.lblUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.lblUser.Location = new System.Drawing.Point(310, 65);
			this.lblUser.Name = "lblUser";
			this.lblUser.Size = new System.Drawing.Size(81, 21);
			this.lblUser.TabIndex = 2;
			this.lblUser.Text = "Username";
			// 
			// txtUsername
			// 
			this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 10.5F);
			this.txtUsername.Location = new System.Drawing.Point(310, 88);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(271, 31);
			this.txtUsername.TabIndex = 3;
			// 
			// btnVerifyUser
			// 
			this.btnVerifyUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
			this.btnVerifyUser.FlatAppearance.BorderSize = 0;
			this.btnVerifyUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnVerifyUser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.btnVerifyUser.ForeColor = System.Drawing.Color.White;
			this.btnVerifyUser.Location = new System.Drawing.Point(587, 88);
			this.btnVerifyUser.Name = "btnVerifyUser";
			this.btnVerifyUser.Size = new System.Drawing.Size(84, 31);
			this.btnVerifyUser.TabIndex = 4;
			this.btnVerifyUser.Text = "Find";
			this.btnVerifyUser.UseVisualStyleBackColor = false;
			this.btnVerifyUser.Click += new System.EventHandler(this.btnVerifyUser_Click);
			// 
			// lblRecoveryQuestion
			// 
			this.lblRecoveryQuestion.AutoSize = true;
			this.lblRecoveryQuestion.Font = new System.Drawing.Font("Segoe UI", 9.5F);
			this.lblRecoveryQuestion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.lblRecoveryQuestion.Location = new System.Drawing.Point(310, 128);
			this.lblRecoveryQuestion.Name = "lblRecoveryQuestion";
			this.lblRecoveryQuestion.Size = new System.Drawing.Size(133, 21);
			this.lblRecoveryQuestion.TabIndex = 5;
			this.lblRecoveryQuestion.Text = "Security Question";
			// 
			// txtRecoveryQuestion
			// 
			this.txtRecoveryQuestion.Font = new System.Drawing.Font("Segoe UI", 10.5F);
			this.txtRecoveryQuestion.Location = new System.Drawing.Point(310, 151);
			this.txtRecoveryQuestion.Name = "txtRecoveryQuestion";
			this.txtRecoveryQuestion.ReadOnly = true;
			this.txtRecoveryQuestion.Size = new System.Drawing.Size(361, 31);
			this.txtRecoveryQuestion.TabIndex = 6;
			// 
			// lblAnswer
			// 
			this.lblAnswer.AutoSize = true;
			this.lblAnswer.Font = new System.Drawing.Font("Segoe UI", 9.5F);
			this.lblAnswer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.lblAnswer.Location = new System.Drawing.Point(310, 191);
			this.lblAnswer.Name = "lblAnswer";
			this.lblAnswer.Size = new System.Drawing.Size(122, 21);
			this.lblAnswer.TabIndex = 7;
			this.lblAnswer.Text = "Security Answer";
			// 
			// txtAnswer
			// 
			this.txtAnswer.Font = new System.Drawing.Font("Segoe UI", 10.5F);
			this.txtAnswer.Location = new System.Drawing.Point(310, 214);
			this.txtAnswer.Name = "txtAnswer";
			this.txtAnswer.Size = new System.Drawing.Size(361, 31);
			this.txtAnswer.TabIndex = 8;
			// 
			// lblNewPass
			// 
			this.lblNewPass.AutoSize = true;
			this.lblNewPass.Font = new System.Drawing.Font("Segoe UI", 9.5F);
			this.lblNewPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.lblNewPass.Location = new System.Drawing.Point(310, 254);
			this.lblNewPass.Name = "lblNewPass";
			this.lblNewPass.Size = new System.Drawing.Size(112, 21);
			this.lblNewPass.TabIndex = 9;
			this.lblNewPass.Text = "New Password";
			// 
			// txtNewPassword
			// 
			this.txtNewPassword.Enabled = false;
			this.txtNewPassword.Font = new System.Drawing.Font("Segoe UI", 10.5F);
			this.txtNewPassword.Location = new System.Drawing.Point(310, 277);
			this.txtNewPassword.Name = "txtNewPassword";
			this.txtNewPassword.Size = new System.Drawing.Size(361, 31);
			this.txtNewPassword.TabIndex = 10;
			this.txtNewPassword.UseSystemPasswordChar = true;
			// 
			// lblConfirmPass
			// 
			this.lblConfirmPass.AutoSize = true;
			this.lblConfirmPass.Font = new System.Drawing.Font("Segoe UI", 9.5F);
			this.lblConfirmPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.lblConfirmPass.Location = new System.Drawing.Point(310, 317);
			this.lblConfirmPass.Name = "lblConfirmPass";
			this.lblConfirmPass.Size = new System.Drawing.Size(137, 21);
			this.lblConfirmPass.TabIndex = 11;
			this.lblConfirmPass.Text = "Confirm Password";
			// 
			// txtConfirmPassword
			// 
			this.txtConfirmPassword.Enabled = false;
			this.txtConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 10.5F);
			this.txtConfirmPassword.Location = new System.Drawing.Point(310, 340);
			this.txtConfirmPassword.Name = "txtConfirmPassword";
			this.txtConfirmPassword.Size = new System.Drawing.Size(361, 31);
			this.txtConfirmPassword.TabIndex = 12;
			this.txtConfirmPassword.UseSystemPasswordChar = true;
			// 
			// btnReset
			// 
			this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
			this.btnReset.FlatAppearance.BorderSize = 0;
			this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnReset.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
			this.btnReset.ForeColor = System.Drawing.Color.White;
			this.btnReset.Location = new System.Drawing.Point(310, 400);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(150, 40);
			this.btnReset.TabIndex = 13;
			this.btnReset.Text = "Reset Password";
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
			this.btnCancel.Location = new System.Drawing.Point(521, 400);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(150, 40);
			this.btnCancel.TabIndex = 14;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// frmForgotPassword
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
			this.ClientSize = new System.Drawing.Size(716, 495);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnReset);
			this.Controls.Add(this.lblConfirmPass);
			this.Controls.Add(this.lblNewPass);
			this.Controls.Add(this.txtAnswer);
			this.Controls.Add(this.lblAnswer);
			this.Controls.Add(this.txtRecoveryQuestion);
			this.Controls.Add(this.lblRecoveryQuestion);
			this.Controls.Add(this.btnVerifyUser);
			this.Controls.Add(this.txtConfirmPassword);
			this.Controls.Add(this.txtNewPassword);
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
		public System.Windows.Forms.Button btnVerifyUser;
		private System.Windows.Forms.Label lblRecoveryQuestion;
		public System.Windows.Forms.TextBox txtRecoveryQuestion;
		private System.Windows.Forms.Label lblAnswer;
		public System.Windows.Forms.TextBox txtAnswer;
		private System.Windows.Forms.Label lblNewPass;
		public System.Windows.Forms.TextBox txtNewPassword;
		private System.Windows.Forms.Label lblConfirmPass;
		public System.Windows.Forms.TextBox txtConfirmPassword;
		public System.Windows.Forms.Button btnReset;
		public System.Windows.Forms.Button btnCancel;
	}
}