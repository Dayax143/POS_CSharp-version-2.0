namespace key_luxery1
{
	partial class frmReport
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
			this.pnlHeader = new System.Windows.Forms.Panel();
			this.lblFormSubtitle = new System.Windows.Forms.Label();
			this.lblFormTitle = new System.Windows.Forms.Label();
			this.pnlFilters = new System.Windows.Forms.Panel();
			this.txtSearchKeyword = new System.Windows.Forms.TextBox();
			this.lblSearch = new System.Windows.Forms.Label();
			this.btnPrint = new System.Windows.Forms.Button();
			this.btnExportExcel = new System.Windows.Forms.Button();
			this.btnSearch = new System.Windows.Forms.Button();
			this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
			this.lblToDate = new System.Windows.Forms.Label();
			this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
			this.lblFromDate = new System.Windows.Forms.Label();
			this.cmbReportType = new System.Windows.Forms.ComboBox();
			this.lblReportType = new System.Windows.Forms.Label();
			this.pnlSummaryCards = new System.Windows.Forms.Panel();
			this.pnlCard4 = new System.Windows.Forms.Panel();
			this.lblTotalTransactionsVal = new System.Windows.Forms.Label();
			this.lblTotalTransactionsTitle = new System.Windows.Forms.Label();
			this.pnlCard3 = new System.Windows.Forms.Panel();
			this.lblTotalProfitVal = new System.Windows.Forms.Label();
			this.lblTotalProfitTitle = new System.Windows.Forms.Label();
			this.pnlCard2 = new System.Windows.Forms.Panel();
			this.lblTotalExpensesVal = new System.Windows.Forms.Label();
			this.lblTotalExpensesTitle = new System.Windows.Forms.Label();
			this.pnlCard1 = new System.Windows.Forms.Panel();
			this.lblTotalSalesVal = new System.Windows.Forms.Label();
			this.lblTotalSalesTitle = new System.Windows.Forms.Label();
			this.pnlGridContainer = new System.Windows.Forms.Panel();
			this.dgvReports = new System.Windows.Forms.DataGridView();
			this.pnlHeader.SuspendLayout();
			this.pnlFilters.SuspendLayout();
			this.pnlSummaryCards.SuspendLayout();
			this.pnlCard4.SuspendLayout();
			this.pnlCard3.SuspendLayout();
			this.pnlCard2.SuspendLayout();
			this.pnlCard1.SuspendLayout();
			this.pnlGridContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvReports)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlHeader
			// 
			this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(43)))), ((int)(((byte)(73)))));
			this.pnlHeader.Controls.Add(this.lblFormSubtitle);
			this.pnlHeader.Controls.Add(this.lblFormTitle);
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlHeader.Location = new System.Drawing.Point(0, 0);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Size = new System.Drawing.Size(1184, 60);
			this.pnlHeader.TabIndex = 0;
			// 
			// lblFormSubtitle
			// 
			this.lblFormSubtitle.AutoSize = true;
			this.lblFormSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFormSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(185)))), ((int)(((byte)(210)))));
			this.lblFormSubtitle.Location = new System.Drawing.Point(16, 33);
			this.lblFormSubtitle.Name = "lblFormSubtitle";
			this.lblFormSubtitle.Size = new System.Drawing.Size(326, 15);
			this.lblFormSubtitle.TabIndex = 1;
			this.lblFormSubtitle.Text = "Generate, filter, print, and export official operational records";
			// 
			// lblFormTitle
			// 
			this.lblFormTitle.AutoSize = true;
			this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFormTitle.ForeColor = System.Drawing.Color.White;
			this.lblFormTitle.Location = new System.Drawing.Point(14, 8);
			this.lblFormTitle.Name = "lblFormTitle";
			this.lblFormTitle.Size = new System.Drawing.Size(268, 25);
			this.lblFormTitle.TabIndex = 0;
			this.lblFormTitle.Text = "Executive Financial & Sales Hub";
			// 
			// pnlFilters
			// 
			this.pnlFilters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
			this.pnlFilters.Controls.Add(this.txtSearchKeyword);
			this.pnlFilters.Controls.Add(this.lblSearch);
			this.pnlFilters.Controls.Add(this.btnPrint);
			this.pnlFilters.Controls.Add(this.btnExportExcel);
			this.pnlFilters.Controls.Add(this.btnSearch);
			this.pnlFilters.Controls.Add(this.dtpEndDate);
			this.pnlFilters.Controls.Add(this.lblToDate);
			this.pnlFilters.Controls.Add(this.dtpStartDate);
			this.pnlFilters.Controls.Add(this.lblFromDate);
			this.pnlFilters.Controls.Add(this.cmbReportType);
			this.pnlFilters.Controls.Add(this.lblReportType);
			this.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlFilters.Location = new System.Drawing.Point(0, 60);
			this.pnlFilters.Name = "pnlFilters";
			this.pnlFilters.Padding = new System.Windows.Forms.Padding(10);
			this.pnlFilters.Size = new System.Drawing.Size(1184, 70);
			this.pnlFilters.TabIndex = 1;
			// 
			// txtSearchKeyword
			// 
			this.txtSearchKeyword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSearchKeyword.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSearchKeyword.Location = new System.Drawing.Point(710, 31);
			this.txtSearchKeyword.Name = "txtSearchKeyword";
			this.txtSearchKeyword.Size = new System.Drawing.Size(155, 25);
			this.txtSearchKeyword.TabIndex = 10;
			this.txtSearchKeyword.TextChanged += new System.EventHandler(this.txtSearchKeyword_TextChanged);
			// 
			// lblSearch
			// 
			this.lblSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblSearch.AutoSize = true;
			this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(60)))), ((int)(((byte)(75)))));
			this.lblSearch.Location = new System.Drawing.Point(707, 12);
			this.lblSearch.Name = "lblSearch";
			this.lblSearch.Size = new System.Drawing.Size(81, 15);
			this.lblSearch.TabIndex = 9;
			this.lblSearch.Text = "Filter In Grid:";
			// 
			// btnPrint
			// 
			this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
			this.btnPrint.FlatAppearance.BorderSize = 0;
			this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnPrint.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnPrint.ForeColor = System.Drawing.Color.White;
			this.btnPrint.Location = new System.Drawing.Point(1074, 26);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(95, 32);
			this.btnPrint.TabIndex = 8;
			this.btnPrint.Text = "🖨️ Print";
			this.btnPrint.UseVisualStyleBackColor = false;
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// btnExportExcel
			// 
			this.btnExportExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExportExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
			this.btnExportExcel.FlatAppearance.BorderSize = 0;
			this.btnExportExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnExportExcel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnExportExcel.ForeColor = System.Drawing.Color.White;
			this.btnExportExcel.Location = new System.Drawing.Point(972, 26);
			this.btnExportExcel.Name = "btnExportExcel";
			this.btnExportExcel.Size = new System.Drawing.Size(95, 32);
			this.btnExportExcel.TabIndex = 7;
			this.btnExportExcel.Text = "📄 Export";
			this.btnExportExcel.UseVisualStyleBackColor = false;
			this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
			// 
			// btnSearch
			// 
			this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(43)))), ((int)(((byte)(73)))));
			this.btnSearch.FlatAppearance.BorderSize = 0;
			this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSearch.ForeColor = System.Drawing.Color.White;
			this.btnSearch.Location = new System.Drawing.Point(872, 26);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(93, 32);
			this.btnSearch.TabIndex = 6;
			this.btnSearch.Text = "🔍 Load";
			this.btnSearch.UseVisualStyleBackColor = false;
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// dtpEndDate
			// 
			this.dtpEndDate.CustomFormat = "yyyy-MM-dd";
			this.dtpEndDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpEndDate.Location = new System.Drawing.Point(465, 31);
			this.dtpEndDate.Name = "dtpEndDate";
			this.dtpEndDate.Size = new System.Drawing.Size(130, 25);
			this.dtpEndDate.TabIndex = 5;
			// 
			// lblToDate
			// 
			this.lblToDate.AutoSize = true;
			this.lblToDate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblToDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(60)))), ((int)(((byte)(75)))));
			this.lblToDate.Location = new System.Drawing.Point(462, 12);
			this.lblToDate.Name = "lblToDate";
			this.lblToDate.Size = new System.Drawing.Size(54, 15);
			this.lblToDate.TabIndex = 4;
			this.lblToDate.Text = "To Date:";
			// 
			// dtpStartDate
			// 
			this.dtpStartDate.CustomFormat = "yyyy-MM-dd";
			this.dtpStartDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpStartDate.Location = new System.Drawing.Point(325, 31);
			this.dtpStartDate.Name = "dtpStartDate";
			this.dtpStartDate.Size = new System.Drawing.Size(130, 25);
			this.dtpStartDate.TabIndex = 3;
			// 
			// lblFromDate
			// 
			this.lblFromDate.AutoSize = true;
			this.lblFromDate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFromDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(60)))), ((int)(((byte)(75)))));
			this.lblFromDate.Location = new System.Drawing.Point(322, 12);
			this.lblFromDate.Name = "lblFromDate";
			this.lblFromDate.Size = new System.Drawing.Size(69, 15);
			this.lblFromDate.TabIndex = 2;
			this.lblFromDate.Text = "From Date:";
			// 
			// cmbReportType
			// 
			this.cmbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbReportType.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbReportType.FormattingEnabled = true;
			this.cmbReportType.Location = new System.Drawing.Point(19, 31);
			this.cmbReportType.Name = "cmbReportType";
			this.cmbReportType.Size = new System.Drawing.Size(295, 25);
			this.cmbReportType.TabIndex = 1;
			// 
			// lblReportType
			// 
			this.lblReportType.AutoSize = true;
			this.lblReportType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblReportType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(60)))), ((int)(((byte)(75)))));
			this.lblReportType.Location = new System.Drawing.Point(16, 12);
			this.lblReportType.Name = "lblReportType";
			this.lblReportType.Size = new System.Drawing.Size(126, 15);
			this.lblReportType.TabIndex = 0;
			this.lblReportType.Text = "Select Report Stream:";
			// 
			// pnlSummaryCards
			// 
			this.pnlSummaryCards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(235)))), ((int)(((byte)(242)))));
			this.pnlSummaryCards.Controls.Add(this.pnlCard4);
			this.pnlSummaryCards.Controls.Add(this.pnlCard3);
			this.pnlSummaryCards.Controls.Add(this.pnlCard2);
			this.pnlSummaryCards.Controls.Add(this.pnlCard1);
			this.pnlSummaryCards.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlSummaryCards.Location = new System.Drawing.Point(0, 130);
			this.pnlSummaryCards.Name = "pnlSummaryCards";
			this.pnlSummaryCards.Padding = new System.Windows.Forms.Padding(10);
			this.pnlSummaryCards.Size = new System.Drawing.Size(1184, 80);
			this.pnlSummaryCards.TabIndex = 2;
			// 
			// pnlCard4
			// 
			this.pnlCard4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.pnlCard4.BackColor = System.Drawing.Color.White;
			this.pnlCard4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlCard4.Controls.Add(this.lblTotalTransactionsVal);
			this.pnlCard4.Controls.Add(this.lblTotalTransactionsTitle);
			this.pnlCard4.Location = new System.Drawing.Point(885, 10);
			this.pnlCard4.Name = "pnlCard4";
			this.pnlCard4.Size = new System.Drawing.Size(284, 60);
			this.pnlCard4.TabIndex = 3;
			// 
			// lblTotalTransactionsVal
			// 
			this.lblTotalTransactionsVal.Font = new System.Drawing.Font("Segoe UI Bold", 13.5F, System.Drawing.FontStyle.Bold);
			this.lblTotalTransactionsVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.lblTotalTransactionsVal.Location = new System.Drawing.Point(8, 28);
			this.lblTotalTransactionsVal.Name = "lblTotalTransactionsVal";
			this.lblTotalTransactionsVal.Size = new System.Drawing.Size(265, 25);
			this.lblTotalTransactionsVal.TabIndex = 1;
			this.lblTotalTransactionsVal.Text = "0";
			// 
			// lblTotalTransactionsTitle
			// 
			this.lblTotalTransactionsTitle.AutoSize = true;
			this.lblTotalTransactionsTitle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTotalTransactionsTitle.ForeColor = System.Drawing.Color.Gray;
			this.lblTotalTransactionsTitle.Location = new System.Drawing.Point(8, 8);
			this.lblTotalTransactionsTitle.Name = "lblTotalTransactionsTitle";
			this.lblTotalTransactionsTitle.Size = new System.Drawing.Size(107, 13);
			this.lblTotalTransactionsTitle.TabIndex = 0;
			this.lblTotalTransactionsTitle.Text = "FILTERED RECORDS";
			// 
			// pnlCard3
			// 
			this.pnlCard3.BackColor = System.Drawing.Color.White;
			this.pnlCard3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlCard3.Controls.Add(this.lblTotalProfitVal);
			this.pnlCard3.Controls.Add(this.lblTotalProfitTitle);
			this.pnlCard3.Location = new System.Drawing.Point(595, 10);
			this.pnlCard3.Name = "pnlCard3";
			this.pnlCard3.Size = new System.Drawing.Size(280, 60);
			this.pnlCard3.TabIndex = 2;
			// 
			// lblTotalProfitVal
			// 
			this.lblTotalProfitVal.Font = new System.Drawing.Font("Segoe UI Bold", 13.5F, System.Drawing.FontStyle.Bold);
			this.lblTotalProfitVal.ForeColor = System.Drawing.Color.ForestGreen;
			this.lblTotalProfitVal.Location = new System.Drawing.Point(8, 28);
			this.lblTotalProfitVal.Name = "lblTotalProfitVal";
			this.lblTotalProfitVal.Size = new System.Drawing.Size(260, 25);
			this.lblTotalProfitVal.TabIndex = 1;
			this.lblTotalProfitVal.Text = "$0.00";
			// 
			// lblTotalProfitTitle
			// 
			this.lblTotalProfitTitle.AutoSize = true;
			this.lblTotalProfitTitle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTotalProfitTitle.ForeColor = System.Drawing.Color.Gray;
			this.lblTotalProfitTitle.Location = new System.Drawing.Point(8, 8);
			this.lblTotalProfitTitle.Name = "lblTotalProfitTitle";
			this.lblTotalProfitTitle.Size = new System.Drawing.Size(128, 13);
			this.lblTotalProfitTitle.TabIndex = 0;
			this.lblTotalProfitTitle.Text = "NET PROFIT / BALANCE";
			// 
			// pnlCard2
			// 
			this.pnlCard2.BackColor = System.Drawing.Color.White;
			this.pnlCard2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlCard2.Controls.Add(this.lblTotalExpensesVal);
			this.pnlCard2.Controls.Add(this.lblTotalExpensesTitle);
			this.pnlCard2.Location = new System.Drawing.Point(305, 10);
			this.pnlCard2.Name = "pnlCard2";
			this.pnlCard2.Size = new System.Drawing.Size(280, 60);
			this.pnlCard2.TabIndex = 1;
			// 
			// lblTotalExpensesVal
			// 
			this.lblTotalExpensesVal.Font = new System.Drawing.Font("Segoe UI Bold", 13.5F, System.Drawing.FontStyle.Bold);
			this.lblTotalExpensesVal.ForeColor = System.Drawing.Color.Firebrick;
			this.lblTotalExpensesVal.Location = new System.Drawing.Point(8, 28);
			this.lblTotalExpensesVal.Name = "lblTotalExpensesVal";
			this.lblTotalExpensesVal.Size = new System.Drawing.Size(260, 25);
			this.lblTotalExpensesVal.TabIndex = 1;
			this.lblTotalExpensesVal.Text = "$0.00";
			// 
			// lblTotalExpensesTitle
			// 
			this.lblTotalExpensesTitle.AutoSize = true;
			this.lblTotalExpensesTitle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTotalExpensesTitle.ForeColor = System.Drawing.Color.Gray;
			this.lblTotalExpensesTitle.Location = new System.Drawing.Point(8, 8);
			this.lblTotalExpensesTitle.Name = "lblTotalExpensesTitle";
			this.lblTotalExpensesTitle.Size = new System.Drawing.Size(146, 13);
			this.lblTotalExpensesTitle.TabIndex = 0;
			this.lblTotalExpensesTitle.Text = "TOTAL EXPENSES / COUNTS";
			// 
			// pnlCard1
			// 
			this.pnlCard1.BackColor = System.Drawing.Color.White;
			this.pnlCard1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlCard1.Controls.Add(this.lblTotalSalesVal);
			this.pnlCard1.Controls.Add(this.lblTotalSalesTitle);
			this.pnlCard1.Location = new System.Drawing.Point(15, 10);
			this.pnlCard1.Name = "pnlCard1";
			this.pnlCard1.Size = new System.Drawing.Size(280, 60);
			this.pnlCard1.TabIndex = 0;
			// 
			// lblTotalSalesVal
			// 
			this.lblTotalSalesVal.Font = new System.Drawing.Font("Segoe UI Bold", 13.5F, System.Drawing.FontStyle.Bold);
			this.lblTotalSalesVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(43)))), ((int)(((byte)(73)))));
			this.lblTotalSalesVal.Location = new System.Drawing.Point(8, 28);
			this.lblTotalSalesVal.Name = "lblTotalSalesVal";
			this.lblTotalSalesVal.Size = new System.Drawing.Size(260, 25);
			this.lblTotalSalesVal.TabIndex = 1;
			this.lblTotalSalesVal.Text = "$0.00";
			// 
			// lblTotalSalesTitle
			// 
			this.lblTotalSalesTitle.AutoSize = true;
			this.lblTotalSalesTitle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTotalSalesTitle.ForeColor = System.Drawing.Color.Gray;
			this.lblTotalSalesTitle.Location = new System.Drawing.Point(8, 8);
			this.lblTotalSalesTitle.Name = "lblTotalSalesTitle";
			this.lblTotalSalesTitle.Size = new System.Drawing.Size(133, 13);
			this.lblTotalSalesTitle.TabIndex = 0;
			this.lblTotalSalesTitle.Text = "TOTAL REVENUE / VALUE";
			// 
			// pnlGridContainer
			// 
			this.pnlGridContainer.Controls.Add(this.dgvReports);
			this.pnlGridContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlGridContainer.Location = new System.Drawing.Point(0, 210);
			this.pnlGridContainer.Name = "pnlGridContainer";
			this.pnlGridContainer.Padding = new System.Windows.Forms.Padding(12);
			this.pnlGridContainer.Size = new System.Drawing.Size(1184, 451);
			this.pnlGridContainer.TabIndex = 3;
			// 
			// dgvReports
			// 
			this.dgvReports.AllowUserToAddRows = false;
			this.dgvReports.AllowUserToDeleteRows = false;
			this.dgvReports.AllowUserToResizeRows = false;
			this.dgvReports.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgvReports.BackgroundColor = System.Drawing.Color.White;
			this.dgvReports.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dgvReports.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgvReports.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(43)))), ((int)(((byte)(73)))));
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(43)))), ((int)(((byte)(73)))));
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvReports.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dgvReports.ColumnHeadersHeight = 35;
			this.dgvReports.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(240)))), ((int)(((byte)(254)))));
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(43)))), ((int)(((byte)(73)))));
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvReports.DefaultCellStyle = dataGridViewCellStyle2;
			this.dgvReports.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvReports.EnableHeadersVisualStyles = false;
			this.dgvReports.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(238)))), ((int)(((byte)(242)))));
			this.dgvReports.Location = new System.Drawing.Point(12, 12);
			this.dgvReports.MultiSelect = false;
			this.dgvReports.Name = "dgvReports";
			this.dgvReports.ReadOnly = true;
			this.dgvReports.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dgvReports.RowHeadersVisible = false;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
			this.dgvReports.RowsDefaultCellStyle = dataGridViewCellStyle3;
			this.dgvReports.RowTemplate.Height = 28;
			this.dgvReports.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvReports.Size = new System.Drawing.Size(1160, 427);
			this.dgvReports.TabIndex = 0;
			// 
			// frmReport
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
			this.ClientSize = new System.Drawing.Size(1184, 661);
			this.Controls.Add(this.pnlGridContainer);
			this.Controls.Add(this.pnlSummaryCards);
			this.Controls.Add(this.pnlFilters);
			this.Controls.Add(this.pnlHeader);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MinimumSize = new System.Drawing.Size(1000, 600);
			this.Name = "frmReport";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Key Luxury Reports & Business Analytics";
			this.Load += new System.EventHandler(this.frmReport_Load);
			this.pnlHeader.ResumeLayout(false);
			this.pnlHeader.PerformLayout();
			this.pnlFilters.ResumeLayout(false);
			this.pnlFilters.PerformLayout();
			this.pnlSummaryCards.ResumeLayout(false);
			this.pnlCard4.ResumeLayout(false);
			this.pnlCard4.PerformLayout();
			this.pnlCard3.ResumeLayout(false);
			this.pnlCard3.PerformLayout();
			this.pnlCard2.ResumeLayout(false);
			this.pnlCard2.PerformLayout();
			this.pnlCard1.ResumeLayout(false);
			this.pnlCard1.PerformLayout();
			this.pnlGridContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvReports)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlHeader;
		private System.Windows.Forms.Label lblFormSubtitle;
		private System.Windows.Forms.Label lblFormTitle;
		private System.Windows.Forms.Panel pnlFilters;
		private System.Windows.Forms.ComboBox cmbReportType;
		private System.Windows.Forms.Label lblReportType;
		private System.Windows.Forms.DateTimePicker dtpEndDate;
		private System.Windows.Forms.Label lblToDate;
		private System.Windows.Forms.DateTimePicker dtpStartDate;
		private System.Windows.Forms.Label lblFromDate;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.Button btnExportExcel;
		private System.Windows.Forms.TextBox txtSearchKeyword;
		private System.Windows.Forms.Label lblSearch;
		private System.Windows.Forms.Panel pnlSummaryCards;
		private System.Windows.Forms.Panel pnlCard1;
		private System.Windows.Forms.Label lblTotalSalesVal;
		private System.Windows.Forms.Label lblTotalSalesTitle;
		private System.Windows.Forms.Panel pnlCard4;
		private System.Windows.Forms.Label lblTotalTransactionsVal;
		private System.Windows.Forms.Label lblTotalTransactionsTitle;
		private System.Windows.Forms.Panel pnlCard3;
		private System.Windows.Forms.Label lblTotalProfitVal;
		private System.Windows.Forms.Label lblTotalProfitTitle;
		private System.Windows.Forms.Panel pnlCard2;
		private System.Windows.Forms.Label lblTotalExpensesVal;
		private System.Windows.Forms.Label lblTotalExpensesTitle;
		private System.Windows.Forms.Panel pnlGridContainer;
		private System.Windows.Forms.DataGridView dgvReports;
	}
}