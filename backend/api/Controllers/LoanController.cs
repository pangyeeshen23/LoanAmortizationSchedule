using Microsoft.AspNetCore.Mvc;
using LoanApi.Dtos.Inputs;
using LoanApi.Dtos.Outputs;
using LoanApi.Handler;

namespace LoanApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanController : ControllerBase
    {
        private ILoanHandler _loanHandler;
        public LoanController(ILoanHandler loanHandler)
        {
            _loanHandler = loanHandler;
        }
        [HttpPost]
        [Route("AmortizationScheduleSummary")]
        public ActionResult<AmortizationScheduleSummaryResp> GetAmortizationScheduleSummary(AmortizationScheduleSummaryInput input)
        {
            return _loanHandler.GenerateInstallmentSummary(input);
        }
    }
}
