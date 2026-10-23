namespace key_luxery1
{
	partial class uCustomer
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.panelTopBar = new System.Windows.Forms.Panel();
			this.btnExport = new System.Windows.Forms.Button();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.btnManageCustomer = new System.Windows.Forms.Button();
			this.btnAddCustomer = new System.Windows.Forms.Button();
			this.lblSearch = new System.Windows.Forms.Label();
			this.txtSearchFilter = new System.Windows.Forms.TextBox();
			this.dgvCustomers = new System.Windows.Forms.DataGridView();
			this.panel2 = new System.Windows.Forms.Panel();
			this.btnLast = new System.Windows.Forms.Button();
			this.btnPrev = new System.Windows.Forms.Button();
			this.btnNext = new System.Windows.Forms.Button();
			this.btnFirst = new System.Windows.Forms.Button();
			this.lblPageInfo = new System.Windows.Forms.Label();
			this.panelTopBar.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).BeginInit();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelTopBar
			// 
			this.panelTopBar.BackColor = System.Drawing.Color.White;
			this.panelTopBar.Controls.Add(this.btnExport);
			this.panelTopBar.Controls.Add(this.btnRefresh);
			this.panelTopBar.Controls.Add(this.btnManageCustomer);
			this.panelTopBar.Controls.Add(this.btnAddCustomer);
			this.panelTopBar.Controls.Add(this.lblSearch);
			this.panelTopBar.Controls.Add(this.txtSearchFilter);
			this.panelTopBar.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelTopBar.Location = new System.Drawing.Point(0, 0);
			this.panelTopBar.Name = "panelTopBar";
			this.panelTopBar.Size = new System.Drawing.Size(1219, 90);
			this.panelTopBar.TabIndex = 0;
			// 
			// btnExport
			// 
			this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
			this.btnExport.FlatAppearance.BorderSize = 0;
			this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnExport.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
			this.btnExport.ForeColor = System.Drawing.Color.White;
			this.btnExport.Location = new System.Drawing.Point(246, 26);
			this.btnExport.Name = "btnExport";
			this.btnExport.Size = new System.Drawing.Size(110, 35);
			this.btnExport.TabIndex = 6;
			this.btnExport.Text = "Export CSV";
			this.btnExport.UseVisualStyleBackColor = false;
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			// 
			// btnRefresh
			// 
			this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
			this.btnRefresh.FlatAppearance.BorderSize = 0;
			this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
			this.btnRefresh.ForeColor = System.Drawing.Color.White;
			this.btnRefresh.Location = new System.Drawing.Point(140, 26);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(100, 35);
			this.btnRefresh.TabIndex = 6;
			this.btnRefresh.Text = "Refresh";
			this.btnRefresh.UseVisualStyleBackColor = false;
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
			// 
			// btnManageCustomer
			// 
			this.btnManageCustomer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
			this.btnManageCustomer.FlatAppearance.BorderSize = 0;
			this.btnManageCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnManageCustomer.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
			this.btnManageCustomer.ForeColor = System.Drawing.Color.White;
			this.btnManageCustomer.Location = new System.Drawing.Point(362, 28);
			this.btnManageCustomer.Name = "btnManageCustomer";
			this.btnManageCustomer.Size = new System.Drawing.Size(130, 35);
			this.btnManageCustomer.TabIndex = 5;
			this.btnManageCustomer.Text = "Manage Client";
			this.btnManageCustomer.UseVisualStyleBackColor = false;
			this.btnManageCustomer.Visible = false;
			this.btnManageCustomer.Click += new System.EventHandler(this.btnManageCustomer_Click);
			// 
			// btnAddCustomer
			// 
			this.btnAddCustomer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
			this.btnAddCustomer.FlatAppearance.BorderSize = 0;
			this.btnAddCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAddCustomer.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
			this.btnAddCustomer.ForeColor = System.Drawing.Color.White;
			this.btnAddCustomer.Location = new System.Drawing.Point(20, 26);
			this.btnAddCustomer.Name = "btnAddCustomer";
			this.btnAddCustomer.Size = new System.Drawing.Size(115, 35);
			this.btnAddCustomer.TabIndex = 5;
			this.btnAddCustomer.Text = "+ Add Client";
			this.btnAddCustomer.UseVisualStyleBackColor = false;
			this.btnAddCustomer.Click += new System.EventHandler(this.btnAddCustomer_Click);
			// 
			// lblSearch
			// 
			this.lblSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblSearch.AutoSize = true;
			this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.lblSearch.Location = new System.Drawing.Point(925, 35);
			this.lblSearch.Name = "lblSearch";
			this.lblSearch.Size = new System.Drawing.Size(98, 20);
			this.lblSearch.TabIndex = 4;
			this.lblSearch.Text = "Search Client:";
			// 
			// txtSearchFilter
			// 
			this.txtSearchFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSearchFilter.Font = new System.Drawing.Font("Segoe UI", 9.5F);
			this.txtSearchFilter.Location = new System.Drawing.Point(1025, 32);
			this.txtSearchFilter.Name = "txtSearchFilter";
			this.txtSearchFilter.Size = new System.Drawing.Size(170, 29);
			this.txtSearchFilter.TabIndex = 3;
			this.txtSearchFilter.TextChanged += new System.EventHandler(this.txtSearchFilter_TextChanged);
			// 
			// dgvCustomers
			// 
			this.dgvCustomers.AllowUserToAddRows = false;
			this.dgvCustomers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgvCustomers.BackgroundColor = System.Drawing.Color.White;
			this.dgvCustomers.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dgvCustomers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgvCustomers.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvCustomers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dgvCustomers.ColumnHeadersHeight = 40;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.5F);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvCustomers.DefaultCellStyle = dataGridViewCellStyle2;
			this.dgvCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvCustomers.EnableHeadersVisualStyles = false;
			this.dgvCustomers.Location = new System.Drawing.Point(0, 90);
			this.dgvCustomers.Name = "dgvCustomers";
			this.dgvCustomers.ReadOnly = true;
			this.dgvCustomers.RowHeadersVisible = false;
			this.dgvCustomers.RowHeadersWidth = 51;
			this.dgvCustomers.RowTemplate.Height = 32;
			this.dgvCustomers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvCustomers.Size = new System.Drawing.Size(1219, 674);
			this.dgvCustomers.TabIndex = 1;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.btnLast);
			this.panel2.Controls.Add(this.btnPrev);
			this.panel2.Controls.Add(this.btnNext);
			this.panel2.Controls.Add(this.btnFirst);
			this.panel2.Controls.Add(this.lblPageInfo);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 689);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(1219, 75);
			this.panel2.TabIndex = 14;
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
			// uCustomer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.dgvCustomers);
			this.Controls.Add(this.panelTopBar);
			this.Name = "uCustomer";
			this.Size = new System.Drawing.Size(1219, 764);
			this.Load += new System.EventHandler(this.uCustomers_Load);
			this.panelTopBar.ResumeLayout(false);
			this.panelTopBar.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).EndInit();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelTopBar;
		private System.Windows.Forms.Label lblSearch;
		public System.Windows.Forms.TextBox txtSearchFilter;
		private System.Windows.Forms.Button btnAddCustomer;
		private System.Windows.Forms.Button btnManageCustomer;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Button btnExport;
		private System.Windows.Forms.DataGridView dgvCustomers;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button btnLast;
		private System.Windows.Forms.Button btnPrev;
		private System.Windows.Forms.Button btnNext;
		private System.Windows.Forms.Button btnFirst;
		private System.Windows.Forms.Label lblPageInfo;
	}
}