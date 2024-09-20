using LoanApi.Models;

namespace LoanApi.Dtos.Outputs
{
    public class AmortizationScheduleSummaryResp
    {
        public double MonthlyPaymentAmount { get; set; }
        public double TotalInterestPaid { get; set; }
        public IList<Payment> Payments { get; set; }
    }
}
