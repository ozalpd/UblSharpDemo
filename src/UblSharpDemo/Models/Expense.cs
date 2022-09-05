namespace UblSharpDemo.Models
{
    public class Expense
    {
        public Expense()
        {
            _taxes = new List<ExpenseTax>();
        }

        public Expense(DateTimeOffset issueDate, string issueNumber, string taxNr, string uuid) : this()
        {
            IssueDate = issueDate;
            IssueNumber = issueNumber;
            TaxNr = taxNr;
            UUID = uuid;
        }

        public void AddTax(int percent, decimal taxableAmount, decimal taxAmount)
        {
            string taxCode = percent.ToString();
            if (Taxes.Any(t => t.TaxCode == taxCode))
                return;

            _taxes.Add(new ExpenseTax(percent, taxCode, taxableAmount, taxAmount));
        }

        public string GetTaxableAmountText(int taxRate) => GetTaxableAmountText(taxRate.ToString());
        public string GetTaxableAmountText(string taxCode)
        {
            var tax = Taxes.FirstOrDefault(t => t.TaxCode == taxCode);
            return tax == null ? string.Empty : tax.TaxableAmount.ToString("#.00");
        }

        public string GetTaxAmountText(int taxRate) => GetTaxAmountText(taxRate.ToString());
        public string GetTaxAmountText(string taxCode)
        {
            var tax = Taxes.FirstOrDefault(t => t.TaxCode == taxCode);
            return tax == null ? string.Empty : tax.TaxAmount.ToString("#.00");
        }

        public IEnumerable<ExpenseTax> Taxes => _taxes;
        private List<ExpenseTax> _taxes;

        public DateTimeOffset IssueDate { get; set; }
        public string IssueDateText => IssueDate.ToString("dd.MM.yyyy");
        public string IssueNumber { get; set; }
        public string TaxNr { get; set; }
        public string UUID { get; set; }
    }
}
