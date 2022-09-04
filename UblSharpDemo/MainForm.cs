using System.Xml.Serialization;
using UblSharp;
using UblSharp.CommonAggregateComponents;

namespace UblSharpDemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnListFiles_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDirPath.Text))
            {
                MessageBox.Show("A directory path is needed to list some XML files!",
                                "Empty Directory Path",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return;
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
            var invoiceId = invoice.ID;
            var lines = invoice.InvoiceLine;
            var currencyCode = invoice.DocumentCurrencyCode;


            var isseuDate = invoice.IssueDate.Value.ToString("dd.MM.yyyy");
            var biller = invoice.AccountingSupplierParty.Party;
            var billerIds = biller.PartyIdentification;
            var taxNr = billerIds.FirstOrDefault(pi => pi.ID.schemeID.Equals("VKN"));
            if (taxNr == null)
            {
                taxNr = billerIds.FirstOrDefault(pi => pi.ID.schemeID.Equals("TCKN"));
            }

            var agent = biller.AgentParty;
            var uuid = invoice.UUID.Value;

            txtStatus.Text = $"Tax Nr: {taxNr.ID.Value} \r\nIssue Date: {isseuDate} \r\nUUID: {uuid} ";
            var taxes = invoice.TaxTotal;

            foreach (var tax in taxes)
            {
                DisplayTax(tax);
            }
        }

        private void DisplayTax(TaxTotalType? tax)
        {
            txtStatus.Text += $"\r\nTotal Tax: {tax.TaxAmount.Value} {tax.TaxAmount.currencyID}";
            foreach (var item in tax.TaxSubtotal)
            {
                DisplayTax(item);
            }
        }

        private void DisplayTax(TaxSubtotalType tax)
        {
            var taxAmount = tax.TaxAmount.Value;
            var taxableAmount = tax.TaxableAmount.Value;
            var taxRate = tax.Percent.Value;

            txtStatus.Text += $"\r\nTax {taxRate}% {taxAmount} {tax.TaxAmount.currencyID}";
            txtStatus.Text += $",  Taxable {taxRate}: {taxableAmount} {tax.TaxAmount.currencyID}";
        }
    }
}
