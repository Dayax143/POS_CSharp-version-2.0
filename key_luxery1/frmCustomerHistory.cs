using System;
using System.Data;
using System.Windows.Forms;

namespace key_luxery1
{
	public partial class frmCustomerHistory : Form
	{
		// Properties to receive target customer details when opening the form
		public string CustomerName { get; set; } = "";

		public frmCustomerHistory()
		{
			InitializeComponent();
		}

		private void frmCustomerHistory_Load(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(CustomerName))
			{
				MessageBox.Show("No customer specified.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				this.Close();
				return;
			}

			lblCustomerName.Text = $"Client: {CustomerName}";
			LoadCustomerHistoryData();
		}

		private void LoadCustomerHistoryData()
		{
			try
			{
				string safeCustomerName = CustomerName.Replace("'", "''");

				// 1. Load Invoices for this customer
				string invoiceQuery = $@"SELECT i.invoice_no AS [Invoice No], 
								 i.invoice_date AS [Date], 
								 i.due_date AS [Due Date],
								 i.subtotal AS [Subtotal ($)], 
								 i.discount AS [Discount ($)], 
								 i.tax AS [Tax ($)], 
								 i.grand_total AS [Grand Total ($)], 
								 i.status AS [Status] 
						  FROM tblInvoice i
						  INNER JOIN tblCustomer c ON i.customer_id = c.customer_id
						  WHERE c.customer_name = '{safeCustomerName}' 
						    AND (i.deleted = 0 OR i.deleted IS NULL) 
						  ORDER BY i.invoice_date DESC";

				DataTable dtInvoices = sharedClass.GetData(invoiceQuery);
				dgvCustomerInvoices.DataSource = dtInvoices;

				// 2. Load Payments for this customer
				string paymentQuery = $@"SELECT p.reference_No AS [Reference No], 
								 p.payment_Date AS [Payment Date], 
								 p.payment_Method AS [Payment Method], 
								 p.amount_paid AS [Amount Paid ($)]
						  FROM tblPayment p
						  INNER JOIN tblInvoice i ON p.invoice_id = i.invoice_id
						  INNER JOIN tblCustomer c ON i.customer_id = c.customer_id
						  WHERE c.customer_name = '{safeCustomerName}' 
						  ORDER BY p.payment_Date DESC";

				//WHERE customer_Name = '{safeCustomerName}' 
				DataTable dtPayments = sharedClass.GetData(paymentQuery);
				dgvCustomerPayments.DataSource = dtPayments;

				// 3. Calculate Summary Metrics
				int invoiceCount = dtInvoices.Rows.Count;
				decimal totalSales = 0;
				foreach (DataRow row in dtInvoices.Rows)
				{
					if (row["Grand Total ($)"] != DBNull.Value)
					{
						decimal.TryParse(row["Grand Total ($)"].ToString(), out decimal val);
						totalSales += val;
					}
				}

				decimal totalPaid = 0;
				foreach (DataRow row in dtPayments.Rows)
				{
					if (row["Amount Paid ($)"] != DBNull.Value)
					{
						decimal.TryParse(row["Amount Paid ($)"].ToString(), out decimal val);
						totalPaid += val;
					}
				}

				decimal balanceDue = totalSales - totalPaid;
				if (balanceDue < 0) balanceDue = 0; // Avoid negative balances if overpaid

				// Update Summary UI Labels
				lblInvoiceCountVal.Text = invoiceCount.ToString();
				lblTotalSalesVal.Text = $"${totalSales:N2}";
				lblTotalPaidVal.Text = $"${totalPaid:N2}";
				lblTotalBalanceVal.Text = $"${balanceDue:N2}";
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error loading customer history: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}