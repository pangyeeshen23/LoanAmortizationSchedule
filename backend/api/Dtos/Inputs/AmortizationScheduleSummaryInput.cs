using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LoanApi.Dtos.Inputs
{
    public class AmortizationScheduleSummaryInput
    {
        [Required(ErrorMessage = "Loan Amount is required")]
        [Range(typeof(decimal), "1000", "79228162514264337593543950335")]
        [DefaultValue(10000)]
        public decimal LoanAmount { get; set; }

        [Required(ErrorMessage = "Annual Interest Rate is required")]
        [Range(2, 99)]
        [DefaultValue(3)]
        public decimal AnnualInterestRate { get; set; }

        [Required(ErrorMessage = "Loan Term is required")]
        [Range(1, 40)]
        [DefaultValue(4)]
        public int LoanTerm { get; set; }

        [Required(ErrorMessage = "Initial Date Of Loan is required")]
        public DateTime? InitialDateOfLoan { get; set; }
    }
}
