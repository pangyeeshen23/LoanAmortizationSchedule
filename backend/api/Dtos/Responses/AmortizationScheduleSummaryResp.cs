using LoanApi.Models;

namespace LoanApi.Dtos.Outputs
{
    public class AmortizationScheduleSummaryResp
    {
        public decimal MonthlyPaymentAmount { get; set; }
        public decimal TotalInterestPaid { get; set; }
        public IList<Payment> Payments { get; set; }
    }
}
