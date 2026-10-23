namespace key_luxery1
{
	partial class uInvoice
	{
		private System.ComponentModel.IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		private void InitializeComponent()
		{
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			this.dgvInvoices = new System.Windows.Forms.DataGridView();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnLast = new System.Windows.Forms.Button();
			this.btnNext = new System.Windows.Forms.Button();
			this.lblPageInfo = new System.Windows.Forms.Label();
			this.btnPrev = new System.Windows.Forms.Button();
			this.btnFirst = new System.Windows.Forms.Button();
			this.btnManageInvoice = new System.Windows.Forms.Button();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.txtSearchInvoice = new System.Windows.Forms.TextBox();
			this.lblSearch = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).BeginInit();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// dgvInvoices
			// 
			this.dgvInvoices.AllowUserToAddRows = false;
			this.dgvInvoices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgvInvoices.BackgroundColor = System.Drawing.Color.White;
			this.dgvInvoices.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dgvInvoices.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgvInvoices.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvInvoices.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dgvInvoices.ColumnHeadersHeight = 35;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9.5F);
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvInvoices.DefaultCellStyle = dataGridViewCellStyle4;
			this.dgvInvoices.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvInvoices.EnableHeadersVisualStyles = false;
			this.dgvInvoices.Location = new System.Drawing.Point(0, 100);
			this.dgvInvoices.Name = "dgvInvoices";
			this.dgvInvoices.ReadOnly = true;
			this.dgvInvoices.RowHeadersVisible = false;
			this.dgvInvoices.RowHeadersWidth = 51;
			this.dgvInvoices.RowTemplate.Height = 30;
			this.dgvInvoices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvInvoices.Size = new System.Drawing.Size(1067, 589);
			this.dgvInvoices.TabIndex = 0;
			this.dgvInvoices.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInvoices_CellDoubleClick);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnManageInvoice);
			this.panel1.Controls.Add(this.btnRefresh);
			this.panel1.Controls.Add(this.txtSearchInvoice);
			this.panel1.Controls.Add(this.lblSearch);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1067, 100);
			this.panel1.TabIndex = 4;
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
			// btnManageInvoice
			// 
			this.btnManageInvoice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
			this.btnManageInvoice.FlatAppearance.BorderSize = 0;
			this.btnManageInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnManageInvoice.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.btnManageInvoice.ForeColor = System.Drawing.Color.White;
			this.btnManageInvoice.Location = new System.Drawing.Point(904, 15);
			this.btnManageInvoice.Name = "btnManageInvoice";
			this.btnManageInvoice.Size = new System.Drawing.Size(100, 31);
			this.btnManageInvoice.TabIndex = 6;
			this.btnManageInvoice.Text = "Manage";
			this.btnManageInvoice.UseVisualStyleBackColor = false;
			this.btnManageInvoice.Click += new System.EventHandler(this.btnManageInvoice_Click);
			// 
			// btnRefresh
			// 
			this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
			this.btnRefresh.FlatAppearance.BorderSize = 0;
			this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.btnRefresh.ForeColor = System.Drawing.Color.White;
			this.btnRefresh.Location = new System.Drawing.Point(798, 15);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(100, 31);
			this.btnRefresh.TabIndex = 6;
			this.btnRefresh.Text = "Refresh";
			this.btnRefresh.UseVisualStyleBackColor = false;
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
			// 
			// txtSearchInvoice
			// 
			this.txtSearchInvoice.Font = new System.Drawing.Font("Segoe UI", 9.5F);
			this.txtSearchInvoice.Location = new System.Drawing.Point(530, 16);
			this.txtSearchInvoice.Name = "txtSearchInvoice";
			this.txtSearchInvoice.Size = new System.Drawing.Size(250, 29);
			this.txtSearchInvoice.TabIndex = 5;
			this.txtSearchInvoice.TextChanged += new System.EventHandler(this.txtSearchInvoice_TextChanged);
			// 
			// lblSearch
			// 
			this.lblSearch.AutoSize = true;
			this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
			this.lblSearch.Location = new System.Drawing.Point(395, 19);
			this.lblSearch.Name = "lblSearch";
			this.lblSearch.Size = new System.Drawing.Size(129, 21);
			this.lblSearch.TabIndex = 4;
			this.lblSearch.Text = "Search Invoice :";
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
			this.panel2.Size = new System.Drawing.Size(1067, 75);
			this.panel2.TabIndex = 13;
			// 
			// uInvoice
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
			this.Controls.Add(this.dgvInvoices);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel2);
			this.Name = "uInvoice";
			this.Size = new System.Drawing.Size(1067, 764);
			this.Load += new System.EventHandler(this.uInvoice_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.DataGridView dgvInvoices;
		private System.Windows.Forms.Panel panel1;
		public System.Windows.Forms.Button btnRefresh;
		public System.Windows.Forms.TextBox txtSearchInvoice;
		private System.Windows.Forms.Label lblSearch;
		public System.Windows.Forms.Button btnManageInvoice;
		private System.Windows.Forms.Button btnFirst;
		private System.Windows.Forms.Button btnPrev;
		private System.Windows.Forms.Label lblPageInfo;
		private System.Windows.Forms.Button btnNext;
		private System.Windows.Forms.Button btnLast;
		private System.Windows.Forms.Panel panel2;
	}
}