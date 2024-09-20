using LoanApi.Dtos.Inputs;
using LoanApi.Dtos.Outputs;

namespace LoanApi.Handler
{
    public interface ILoanHandler
    {
        public AmortizationScheduleSummaryResp GenerateInstallmentSummary(AmortizationScheduleSummaryInput input);
    }
}
