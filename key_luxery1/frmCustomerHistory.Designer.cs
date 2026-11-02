namespace key_luxery1
{
	partial class frmCustomerHistory
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.pnlTop = new System.Windows.Forms.Panel();
			this.lblCustomerName = new System.Windows.Forms.Label();
			this.lblTitle = new System.Windows.Forms.Label();
			this.pnlSummary = new System.Windows.Forms.Panel();
			this.lblTotalBalanceVal = new System.Windows.Forms.Label();
			this.lblTotalBalance = new System.Windows.Forms.Label();
			this.lblTotalPaidVal = new System.Windows.Forms.Label();
			this.lblTotalPaid = new System.Windows.Forms.Label();
			this.lblTotalSalesVal = new System.Windows.Forms.Label();
			this.lblTotalSales = new System.Windows.Forms.Label();
			this.lblInvoiceCountVal = new System.Windows.Forms.Label();
			this.lblInvoiceCount = new System.Windows.Forms.Label();
			this.splitMain = new System.Windows.Forms.SplitContainer();
			this.grpInvoices = new System.Windows.Forms.GroupBox();
			this.dgvCustomerInvoices = new System.Windows.Forms.DataGridView();
			this.splitSub = new System.Windows.Forms.SplitContainer();
			this.grpProducts = new System.Windows.Forms.GroupBox();
			this.dgvCustomerProducts = new System.Windows.Forms.DataGridView();
			this.grpPayments = new System.Windows.Forms.GroupBox();
			this.dgvCustomerPayments = new System.Windows.Forms.DataGridView();
			this.pnlTop.SuspendLayout();
			this.pnlSummary.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
			this.splitMain.Panel1.SuspendLayout();
			this.splitMain.Panel2.SuspendLayout();
			this.splitMain.SuspendLayout();
			this.grpInvoices.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvCustomerInvoices)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitSub)).BeginInit();
			this.splitSub.Panel1.SuspendLayout();
			this.splitSub.Panel2.SuspendLayout();
			this.splitSub.SuspendLayout();
			this.grpProducts.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvCustomerProducts)).BeginInit();
			this.grpPayments.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvCustomerPayments)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlTop
			// 
			this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.pnlTop.Controls.Add(this.lblCustomerName);
			this.pnlTop.Controls.Add(this.lblTitle);
			this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTop.Location = new System.Drawing.Point(0, 0);
			this.pnlTop.Name = "pnlTop";
			this.pnlTop.Size = new System.Drawing.Size(1329, 70);
			this.pnlTop.TabIndex = 0;
			// 
			// lblCustomerName
			// 
			this.lblCustomerName.AutoSize = true;
			this.lblCustomerName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
			this.lblCustomerName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
			this.lblCustomerName.Location = new System.Drawing.Point(20, 35);
			this.lblCustomerName.Name = "lblCustomerName";
			this.lblCustomerName.Size = new System.Drawing.Size(237, 28);
			this.lblCustomerName.TabIndex = 1;
			this.lblCustomerName.Text = "Client: [Customer Name]";
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold);
			this.lblTitle.ForeColor = System.Drawing.Color.White;
			this.lblTitle.Location = new System.Drawing.Point(20, 7);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(320, 31);
			this.lblTitle.TabIndex = 0;
			this.lblTitle.Text = "📊 Customer History Profile";
			// 
			// pnlSummary
			// 
			this.pnlSummary.BackColor = System.Drawing.Color.White;
			this.pnlSummary.Controls.Add(this.lblTotalBalanceVal);
			this.pnlSummary.Controls.Add(this.lblTotalBalance);
			this.pnlSummary.Controls.Add(this.lblTotalPaidVal);
			this.pnlSummary.Controls.Add(this.lblTotalPaid);
			this.pnlSummary.Controls.Add(this.lblTotalSalesVal);
			this.pnlSummary.Controls.Add(this.lblTotalSales);
			this.pnlSummary.Controls.Add(this.lblInvoiceCountVal);
			this.pnlSummary.Controls.Add(this.lblInvoiceCount);
			this.pnlSummary.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlSummary.Location = new System.Drawing.Point(0, 70);
			this.pnlSummary.Name = "pnlSummary";
			this.pnlSummary.Size = new System.Drawing.Size(1329, 95);
			this.pnlSummary.TabIndex = 1;
			// 
			// lblTotalBalanceVal
			// 
			this.lblTotalBalanceVal.AutoSize = true;
			this.lblTotalBalanceVal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
			this.lblTotalBalanceVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
			this.lblTotalBalanceVal.Location = new System.Drawing.Point(820, 42);
			this.lblTotalBalanceVal.Name = "lblTotalBalanceVal";
			this.lblTotalBalanceVal.Size = new System.Drawing.Size(77, 32);
			this.lblTotalBalanceVal.TabIndex = 7;
			this.lblTotalBalanceVal.Text = "$0.00";
			// 
			// lblTotalBalance
			// 
			this.lblTotalBalance.AutoSize = true;
			this.lblTotalBalance.Font = new System.Drawing.Font("Segoe UI", 9.5F);
			this.lblTotalBalance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
			this.lblTotalBalance.Location = new System.Drawing.Point(822, 16);
			this.lblTotalBalance.Name = "lblTotalBalance";
			this.lblTotalBalance.Size = new System.Drawing.Size(121, 21);
			this.lblTotalBalance.TabIndex = 6;
			this.lblTotalBalance.Text = "Balance Due ($):";
			// 
			// lblTotalPaidVal
			// 
			this.lblTotalPaidVal.AutoSize = true;
			this.lblTotalPaidVal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
			this.lblTotalPaidVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
			this.lblTotalPaidVal.Location = new System.Drawing.Point(560, 42);
			this.lblTotalPaidVal.Name = "lblTotalPaidVal";
			this.lblTotalPaidVal.Size = new System.Drawing.Size(77, 32);
			this.lblTotalPaidVal.TabIndex = 5;
			this.lblTotalPaidVal.Text = "$0.00";
			// 
			// lblTotalPaid
			// 
			this.lblTotalPaid.AutoSize = true;
			this.lblTotalPaid.Font = new System.Drawing.Font("Segoe UI", 9.5F);
			this.lblTotalPaid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
			this.lblTotalPaid.Location = new System.Drawing.Point(562, 16);
			this.lblTotalPaid.Name = "lblTotalPaid";
			this.lblTotalPaid.Size = new System.Drawing.Size(101, 21);
			this.lblTotalPaid.TabIndex = 4;
			this.lblTotalPaid.Text = "Total Paid ($):";
			// 
			// lblTotalSalesVal
			// 
			this.lblTotalSalesVal.AutoSize = true;
			this.lblTotalSalesVal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
			this.lblTotalSalesVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
			this.lblTotalSalesVal.Location = new System.Drawing.Point(300, 42);
			this.lblTotalSalesVal.Name = "lblTotalSalesVal";
			this.lblTotalSalesVal.Size = new System.Drawing.Size(77, 32);
			this.lblTotalSalesVal.TabIndex = 3;
			this.lblTotalSalesVal.Text = "$0.00";
			// 
			// lblTotalSales
			// 
			this.lblTotalSales.AutoSize = true;
			this.lblTotalSales.Font = new System.Drawing.Font("Segoe UI", 9.5F);
			this.lblTotalSales.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
			this.lblTotalSales.Location = new System.Drawing.Point(302, 16);
			this.lblTotalSales.Name = "lblTotalSales";
			this.lblTotalSales.Size = new System.Drawing.Size(108, 21);
			this.lblTotalSales.TabIndex = 2;
			this.lblTotalSales.Text = "Total Sales ($):";
			// 
			// lblInvoiceCountVal
			// 
			this.lblInvoiceCountVal.AutoSize = true;
			this.lblInvoiceCountVal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
			this.lblInvoiceCountVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.lblInvoiceCountVal.Location = new System.Drawing.Point(40, 42);
			this.lblInvoiceCountVal.Name = "lblInvoiceCountVal";
			this.lblInvoiceCountVal.Size = new System.Drawing.Size(28, 32);
			this.lblInvoiceCountVal.TabIndex = 1;
			this.lblInvoiceCountVal.Text = "0";
			// 
			// lblInvoiceCount
			// 
			this.lblInvoiceCount.AutoSize = true;
			this.lblInvoiceCount.Font = new System.Drawing.Font("Segoe UI", 9.5F);
			this.lblInvoiceCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
			this.lblInvoiceCount.Location = new System.Drawing.Point(42, 16);
			this.lblInvoiceCount.Name = "lblInvoiceCount";
			this.lblInvoiceCount.Size = new System.Drawing.Size(105, 21);
			this.lblInvoiceCount.TabIndex = 0;
			this.lblInvoiceCount.Text = "Total Invoices:";
			// 
			// splitMain
			// 
			this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitMain.Location = new System.Drawing.Point(0, 165);
			this.splitMain.Name = "splitMain";
			this.splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitMain.Panel1
			// 
			this.splitMain.Panel1.Controls.Add(this.grpInvoices);
			// 
			// splitMain.Panel2
			// 
			this.splitMain.Panel2.Controls.Add(this.splitSub);
			this.splitMain.Size = new System.Drawing.Size(1329, 548);
			this.splitMain.SplitterDistance = 179;
			this.splitMain.TabIndex = 2;
			// 
			// grpInvoices
			// 
			this.grpInvoices.Controls.Add(this.dgvCustomerInvoices);
			this.grpInvoices.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpInvoices.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
			this.grpInvoices.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.grpInvoices.Location = new System.Drawing.Point(0, 0);
			this.grpInvoices.Name = "grpInvoices";
			this.grpInvoices.Padding = new System.Windows.Forms.Padding(10);
			this.grpInvoices.Size = new System.Drawing.Size(1329, 179);
			this.grpInvoices.TabIndex = 0;
			this.grpInvoices.TabStop = false;
			this.grpInvoices.Text = "Invoice History";
			// 
			// dgvCustomerInvoices
			// 
			this.dgvCustomerInvoices.AllowUserToAddRows = false;
			this.dgvCustomerInvoices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgvCustomerInvoices.BackgroundColor = System.Drawing.Color.White;
			this.dgvCustomerInvoices.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dgvCustomerInvoices.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgvCustomerInvoices.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvCustomerInvoices.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dgvCustomerInvoices.ColumnHeadersHeight = 32;
			this.dgvCustomerInvoices.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvCustomerInvoices.EnableHeadersVisualStyles = false;
			this.dgvCustomerInvoices.Location = new System.Drawing.Point(10, 32);
			this.dgvCustomerInvoices.Name = "dgvCustomerInvoices";
			this.dgvCustomerInvoices.ReadOnly = true;
			this.dgvCustomerInvoices.RowHeadersVisible = false;
			this.dgvCustomerInvoices.RowHeadersWidth = 51;
			this.dgvCustomerInvoices.RowTemplate.Height = 28;
			this.dgvCustomerInvoices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvCustomerInvoices.Size = new System.Drawing.Size(1309, 137);
			this.dgvCustomerInvoices.TabIndex = 0;
			// 
			// splitSub
			// 
			this.splitSub.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitSub.Location = new System.Drawing.Point(0, 0);
			this.splitSub.Name = "splitSub";
			// 
			// splitSub.Panel1
			// 
			this.splitSub.Panel1.Controls.Add(this.grpProducts);
			// 
			// splitSub.Panel2
			// 
			this.splitSub.Panel2.Controls.Add(this.grpPayments);
			this.splitSub.Size = new System.Drawing.Size(1329, 365);
			this.splitSub.SplitterDistance = 664;
			this.splitSub.TabIndex = 0;
			// 
			// grpProducts
			// 
			this.grpProducts.Controls.Add(this.dgvCustomerProducts);
			this.grpProducts.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpProducts.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
			this.grpProducts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.grpProducts.Location = new System.Drawing.Point(0, 0);
			this.grpProducts.Name = "grpProducts";
			this.grpProducts.Padding = new System.Windows.Forms.Padding(10);
			this.grpProducts.Size = new System.Drawing.Size(664, 365);
			this.grpProducts.TabIndex = 0;
			this.grpProducts.TabStop = false;
			this.grpProducts.Text = "Sold Products (Invoice Items)";
			// 
			// dgvCustomerProducts
			// 
			this.dgvCustomerProducts.AllowUserToAddRows = false;
			this.dgvCustomerProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgvCustomerProducts.BackgroundColor = System.Drawing.Color.White;
			this.dgvCustomerProducts.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dgvCustomerProducts.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgvCustomerProducts.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvCustomerProducts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dgvCustomerProducts.ColumnHeadersHeight = 32;
			this.dgvCustomerProducts.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvCustomerProducts.EnableHeadersVisualStyles = false;
			this.dgvCustomerProducts.Location = new System.Drawing.Point(10, 32);
			this.dgvCustomerProducts.Name = "dgvCustomerProducts";
			this.dgvCustomerProducts.ReadOnly = true;
			this.dgvCustomerProducts.RowHeadersVisible = false;
			this.dgvCustomerProducts.RowHeadersWidth = 51;
			this.dgvCustomerProducts.RowTemplate.Height = 28;
			this.dgvCustomerProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvCustomerProducts.Size = new System.Drawing.Size(644, 323);
			this.dgvCustomerProducts.TabIndex = 0;
			// 
			// grpPayments
			// 
			this.grpPayments.Controls.Add(this.dgvCustomerPayments);
			this.grpPayments.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpPayments.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
			this.grpPayments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.grpPayments.Location = new System.Drawing.Point(0, 0);
			this.grpPayments.Name = "grpPayments";
			this.grpPayments.Padding = new System.Windows.Forms.Padding(10);
			this.grpPayments.Size = new System.Drawing.Size(661, 365);
			this.grpPayments.TabIndex = 0;
			this.grpPayments.TabStop = false;
			this.grpPayments.Text = "Payment Ledger History";
			// 
			// dgvCustomerPayments
			// 
			this.dgvCustomerPayments.AllowUserToAddRows = false;
			this.dgvCustomerPayments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgvCustomerPayments.BackgroundColor = System.Drawing.Color.White;
			this.dgvCustomerPayments.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dgvCustomerPayments.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgvCustomerPayments.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvCustomerPayments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dgvCustomerPayments.ColumnHeadersHeight = 32;
			this.dgvCustomerPayments.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvCustomerPayments.EnableHeadersVisualStyles = false;
			this.dgvCustomerPayments.Location = new System.Drawing.Point(10, 32);
			this.dgvCustomerPayments.Name = "dgvCustomerPayments";
			this.dgvCustomerPayments.ReadOnly = true;
			this.dgvCustomerPayments.RowHeadersVisible = false;
			this.dgvCustomerPayments.RowHeadersWidth = 51;
			this.dgvCustomerPayments.RowTemplate.Height = 28;
			this.dgvCustomerPayments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvCustomerPayments.Size = new System.Drawing.Size(641, 323);
			this.dgvCustomerPayments.TabIndex = 0;
			// 
			// frmCustomerHistory
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
			this.ClientSize = new System.Drawing.Size(1329, 713);
			this.Controls.Add(this.splitMain);
			this.Controls.Add(this.pnlSummary);
			this.Controls.Add(this.pnlTop);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmCustomerHistory";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Key Luxury - Customer History Profile";
			this.Load += new System.EventHandler(this.frmCustomerHistory_Load);
			this.pnlTop.ResumeLayout(false);
			this.pnlTop.PerformLayout();
			this.pnlSummary.ResumeLayout(false);
			this.pnlSummary.PerformLayout();
			this.splitMain.Panel1.ResumeLayout(false);
			this.splitMain.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
			this.splitMain.ResumeLayout(false);
			this.grpInvoices.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvCustomerInvoices)).EndInit();
			this.splitSub.Panel1.ResumeLayout(false);
			this.splitSub.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitSub)).EndInit();
			this.splitSub.ResumeLayout(false);
			this.grpProducts.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvCustomerProducts)).EndInit();
			this.grpPayments.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvCustomerPayments)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Panel pnlTop;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.Label lblCustomerName;
		private System.Windows.Forms.Panel pnlSummary;
		private System.Windows.Forms.Label lblInvoiceCount;
		private System.Windows.Forms.Label lblInvoiceCountVal;
		private System.Windows.Forms.Label lblTotalSales;
		private System.Windows.Forms.Label lblTotalSalesVal;
		private System.Windows.Forms.Label lblTotalPaid;
		private System.Windows.Forms.Label lblTotalPaidVal;
		private System.Windows.Forms.Label lblTotalBalance;
		private System.Windows.Forms.Label lblTotalBalanceVal;
		private System.Windows.Forms.SplitContainer splitMain;
		private System.Windows.Forms.SplitContainer splitSub;
		private System.Windows.Forms.GroupBox grpInvoices;
		private System.Windows.Forms.DataGridView dgvCustomerInvoices;
		private System.Windows.Forms.GroupBox grpProducts;
		private System.Windows.Forms.DataGridView dgvCustomerProducts;
		private System.Windows.Forms.GroupBox grpPayments;
		private System.Windows.Forms.DataGridView dgvCustomerPayments;
	}
}