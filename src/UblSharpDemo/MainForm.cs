using System.Xml.Serialization;
using UblSharp;
using UblSharp.CommonAggregateComponents;
using UblSharpDemo.Models;

namespace UblSharpDemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Expenses = new List<Expense>();
        }

        public List<Expense> Expenses { get; set; }

        private void btnListFiles_Click(object sender, EventArgs e)
        {
            var folderBrowserDlg = new FolderBrowserDialog();
            if (folderBrowserDlg.ShowDialog() == DialogResult.OK)
            {
                txtDirPath.Text = folderBrowserDlg.SelectedPath;
            }

            var files = Directory.GetFiles(txtDirPath.Text, "*.xml");
            lstFiles.Items.Clear();
            lstFiles.Items.AddRange(files);
        }

        private void lstFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstFiles.Items.Count == 0)
                return;

            var selectedFile = lstFiles.SelectedItem as string ?? lstFiles.Items[0].ToString();
            if (string.IsNullOrEmpty(selectedFile))
                return;

            var invoice = OpenInvoice(selectedFile);
            if (invoice != null)
                ReadInvoice(invoice);
        }

        public InvoiceType? OpenInvoice(string filePath)
        {
            InvoiceType? invoice = null;
            XmlSerializer serializer = new XmlSerializer(typeof(InvoiceType));
            using (StreamReader reader = new StreamReader(filePath))
            {
                invoice = (InvoiceType?)serializer.Deserialize(reader);
            }

            return invoice;
        }

        public void ReadInvoice(InvoiceType invoice)
        {
            var invoiceId = invoice.ID.Value;
            if (Expenses.Any(e => e.IssueNumber == invoiceId))
                return;

            var biller = invoice.AccountingSupplierParty.Party;
            var billerIds = biller.PartyIdentification;
            var taxNr = billerIds.FirstOrDefault(pi => pi.ID.schemeID.Equals("VKN"));
            if (taxNr == null)
            {
                taxNr = billerIds.FirstOrDefault(pi => pi.ID.schemeID.Equals("TCKN"));
            }

            var billerParty = biller != null ? biller.PartyName.FirstOrDefault() : null;
            var expense = new Expense(invoiceId, invoice.UUID.Value)
            {
                BillerName = billerParty != null ? billerParty.Name : string.Empty,
                IssueDate = invoice.IssueDate.Value,
                TaxNr = taxNr != null ? taxNr.ID.Value : string.Empty
            };
            Expenses.Add(expense);

            txtStatus.Text += $"{expense.IssueDateText}\t{expense.IssueNumber}\t{expense.TaxNr}";

            var taxes = invoice.TaxTotal;
            foreach (var tax in taxes)
            {
                AppendTax(expense, tax);
            }
            txtStatus.Text += $"\t{expense.GetTaxableAmountText(20)}" +
                $"\t{expense.GetTaxableAmountText(10)}\t{expense.GetTaxableAmountText(1)}" +
                $"\t{expense.GetTaxableAmountText(0)}" +
                $"\t{expense.BillerName}\t{expense.UUID}";

            txtStatus.Text += $"\t\t\t\t{expense.GetTaxAmountText(1)}" +
                $"\t\t{expense.GetTaxAmountText(10)}\t\t{expense.GetTaxAmountText(20)}";

            txtStatus.Text += "\r\n";
        }

        private void AppendTax(Expense expense, TaxTotalType? tax)
        {
            //txtStatus.Text += $"\r\nTotal Tax: {tax.TaxAmount.Value} {tax.TaxAmount.currencyID}";
            foreach (var subTax in tax.TaxSubtotal)
            {
                AppendTax(expense, subTax);
            }
        }

        private void AppendTax(Expense expense, TaxSubtotalType tax)
        {
            var taxAmount = tax.TaxAmount.Value;
            var taxableAmount = tax.TaxableAmount.Value;
            int taxRate = (int)tax.Percent.Value;

            expense.AddTax(taxRate, taxableAmount, taxAmount);
        }
    }
}
