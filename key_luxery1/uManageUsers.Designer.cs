namespace key_luxery1
{
	partial class uManageUsers
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
			this.panelTop = new System.Windows.Forms.Panel();
			this.btnAddUser = new System.Windows.Forms.Button();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.txtSearchFilter = new System.Windows.Forms.TextBox();
			this.lblSearch = new System.Windows.Forms.Label();
			this.lblTitle = new System.Windows.Forms.Label();
			this.dgvUsers = new System.Windows.Forms.DataGridView();
			this.panel2 = new System.Windows.Forms.Panel();
			this.btnLast = new System.Windows.Forms.Button();
			this.btnPrev = new System.Windows.Forms.Button();
			this.btnNext = new System.Windows.Forms.Button();
			this.btnFirst = new System.Windows.Forms.Button();
			this.lblPageInfo = new System.Windows.Forms.Label();
			this.panelTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelTop
			// 
			this.panelTop.BackColor = System.Drawing.Color.White;
			this.panelTop.Controls.Add(this.btnAddUser);
			this.panelTop.Controls.Add(this.btnRefresh);
			this.panelTop.Controls.Add(this.txtSearchFilter);
			this.panelTop.Controls.Add(this.lblSearch);
			this.panelTop.Controls.Add(this.lblTitle);
			this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelTop.Location = new System.Drawing.Point(0, 0);
			this.panelTop.Name = "panelTop";
			this.panelTop.Size = new System.Drawing.Size(1219, 90);
			this.panelTop.TabIndex = 0;
			// 
			// btnAddUser
			// 
			this.btnAddUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
			this.btnAddUser.FlatAppearance.BorderSize = 0;
			this.btnAddUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAddUser.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
			this.btnAddUser.ForeColor = System.Drawing.Color.White;
			this.btnAddUser.Location = new System.Drawing.Point(920, 28);
			this.btnAddUser.Name = "btnAddUser";
			this.btnAddUser.Size = new System.Drawing.Size(140, 35);
			this.btnAddUser.TabIndex = 4;
			this.btnAddUser.Text = "(+) Add New User";
			this.btnAddUser.UseVisualStyleBackColor = false;
			this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
			// 
			// btnRefresh
			// 
			this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
			this.btnRefresh.FlatAppearance.BorderSize = 0;
			this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
			this.btnRefresh.ForeColor = System.Drawing.Color.White;
			this.btnRefresh.Location = new System.Drawing.Point(1075, 28);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(110, 35);
			this.btnRefresh.TabIndex = 3;
			this.btnRefresh.Text = "Refresh";
			this.btnRefresh.UseVisualStyleBackColor = false;
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
			// 
			// txtSearchFilter
			// 
			this.txtSearchFilter.Font = new System.Drawing.Font("Segoe UI", 9.5F);
			this.txtSearchFilter.Location = new System.Drawing.Point(457, 28);
			this.txtSearchFilter.Name = "txtSearchFilter";
			this.txtSearchFilter.Size = new System.Drawing.Size(240, 29);
			this.txtSearchFilter.TabIndex = 2;
			this.txtSearchFilter.TextChanged += new System.EventHandler(this.txtSearchFilter_TextChanged);
			// 
			// lblSearch
			// 
			this.lblSearch.AutoSize = true;
			this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F);
			this.lblSearch.Location = new System.Drawing.Point(392, 32);
			this.lblSearch.Name = "lblSearch";
			this.lblSearch.Size = new System.Drawing.Size(64, 21);
			this.lblSearch.TabIndex = 1;
			this.lblSearch.Text = "Search :";
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
			this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.lblTitle.Location = new System.Drawing.Point(25, 28);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(250, 37);
			this.lblTitle.TabIndex = 0;
			this.lblTitle.Text = "System User Roles";
			// 
			// dgvUsers
			// 
			this.dgvUsers.AllowUserToAddRows = false;
			this.dgvUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgvUsers.BackgroundColor = System.Drawing.Color.White;
			this.dgvUsers.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dgvUsers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgvUsers.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
			dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
			this.dgvUsers.ColumnHeadersHeight = 38;
			dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 9.5F);
			dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
			dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvUsers.DefaultCellStyle = dataGridViewCellStyle10;
			this.dgvUsers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvUsers.EnableHeadersVisualStyles = false;
			this.dgvUsers.Location = new System.Drawing.Point(0, 90);
			this.dgvUsers.Name = "dgvUsers";
			this.dgvUsers.ReadOnly = true;
			this.dgvUsers.RowHeadersVisible = false;
			this.dgvUsers.RowHeadersWidth = 51;
			this.dgvUsers.RowTemplate.Height = 32;
			this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvUsers.Size = new System.Drawing.Size(1219, 585);
			this.dgvUsers.TabIndex = 1;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.btnLast);
			this.panel2.Controls.Add(this.btnPrev);
			this.panel2.Controls.Add(this.btnNext);
			this.panel2.Controls.Add(this.btnFirst);
			this.panel2.Controls.Add(this.lblPageInfo);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 675);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(1219, 75);
			this.panel2.TabIndex = 15;
			// 
			// btnLast
			// 
			this.btnLast.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.btnLast.FlatAppearance.BorderSize = 0;
			this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnLast.ForeColor = System.Drawing.Color.White;
			this.btnLast.Location = new System.Drawing.Point(288, 25);
			this.btnLast.Name = "btnLast";
			this.btnLast.Size = new System.Drawing.Size(45, 30);
			this.btnLast.TabIndex = 11;
			this.btnLast.Text = ">>";
			this.btnLast.UseVisualStyleBackColor = false;
			this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
			// 
			// btnPrev
			// 
			this.btnPrev.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.btnPrev.FlatAppearance.BorderSize = 0;
			this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnPrev.ForeColor = System.Drawing.Color.White;
			this.btnPrev.Location = new System.Drawing.Point(76, 25);
			this.btnPrev.Name = "btnPrev";
			this.btnPrev.Size = new System.Drawing.Size(45, 30);
			this.btnPrev.TabIndex = 8;
			this.btnPrev.Text = "<";
			this.btnPrev.UseVisualStyleBackColor = false;
			this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
			// 
			// btnNext
			// 
			this.btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.btnNext.FlatAppearance.BorderSize = 0;
			this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnNext.ForeColor = System.Drawing.Color.White;
			this.btnNext.Location = new System.Drawing.Point(237, 25);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(45, 30);
			this.btnNext.TabIndex = 10;
			this.btnNext.Text = ">";
			this.btnNext.UseVisualStyleBackColor = false;
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			// 
			// btnFirst
			// 
			this.btnFirst.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.btnFirst.FlatAppearance.BorderSize = 0;
			this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnFirst.ForeColor = System.Drawing.Color.White;
			this.btnFirst.Location = new System.Drawing.Point(25, 25);
			this.btnFirst.Name = "btnFirst";
			this.btnFirst.Size = new System.Drawing.Size(45, 30);
			this.btnFirst.TabIndex = 7;
			this.btnFirst.Text = "<<";
			this.btnFirst.UseVisualStyleBackColor = false;
			this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
			// 
			// lblPageInfo
			// 
			this.lblPageInfo.AutoSize = true;
			this.lblPageInfo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
			this.lblPageInfo.Location = new System.Drawing.Point(127, 30);
			this.lblPageInfo.Name = "lblPageInfo";
			this.lblPageInfo.Size = new System.Drawing.Size(94, 21);
			this.lblPageInfo.TabIndex = 9;
			this.lblPageInfo.Text = "Page 1 of 1";
			// 
			// uManageUsers
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
			this.Controls.Add(this.dgvUsers);
			this.Controls.Add(this.panelTop);
			this.Controls.Add(this.panel2);
			this.Name = "uManageUsers";
			this.Size = new System.Drawing.Size(1219, 750);
			this.Load += new System.EventHandler(this.uManageUsers_Load);
			this.panelTop.ResumeLayout(false);
			this.panelTop.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelTop;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.TextBox txtSearchFilter;
		private System.Windows.Forms.Label lblSearch;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Button btnAddUser;
		private System.Windows.Forms.DataGridView dgvUsers;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button btnLast;
		private System.Windows.Forms.Button btnPrev;
		private System.Windows.Forms.Button btnNext;
		private System.Windows.Forms.Button btnFirst;
		private System.Windows.Forms.Label lblPageInfo;
	}
}