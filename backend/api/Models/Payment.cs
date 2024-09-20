namespace LoanApi.Models
{
    public class Payment
    {
        public int Month { get; set; }
        public double Amount { get; set; }
        public double InterestPortion { get; set; }
        public double PrincipalPortion { get; set; }
        public double InterestPercentage { get; set; }
        public double PrincipalPercentage { get; set; }
        public double RemainingAmount { get; set; }
        public DateTime Date { get; set; }
    }
}
