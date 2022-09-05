namespace UblSharpDemo.Models
{
    public class ExpenseTax
    {
        public ExpenseTax() { }

        public ExpenseTax(int percent, string taxCode, decimal taxableAmount, decimal taxAmount)
        {
            Percent = percent;
            TaxCode = taxCode;
            TaxableAmount = taxableAmount;
            TaxAmount = taxAmount;
        }

        public int Id { get; set; }
        public int Percent { get; set; }
        public string TaxCode { get; set; }
        public decimal TaxableAmount { get; set; }
        public decimal TaxAmount { get; set; }
    }
}
