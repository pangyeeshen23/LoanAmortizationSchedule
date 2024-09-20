namespace LoanApi.Models
{
    public class Payment
    {
        public int Month { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestPortion { get; set; }
        public decimal PrincipalPortion { get; set; }
        public decimal InterestPercentage { get; set; }
        public decimal PrincipalPercentage { get; set; }
        public decimal RemainingAmount { get; set; }
        public DateTime Date { get; set; }
    }
}
