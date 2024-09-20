using DecimalMath;
using LoanApi.Dtos.Inputs;
using LoanApi.Dtos.Outputs;
using LoanApi.Models;

namespace LoanApi.Handler
{
    public class LoanHandler : ILoanHandler
    {
        public AmortizationScheduleSummaryResp GenerateInstallmentSummary(AmortizationScheduleSummaryInput input)
        {
            AmortizationScheduleSummaryResp resp = new AmortizationScheduleSummaryResp();
            try
            {
                resp = CalculateProcess(resp, input);
            } 
            catch (Exception ex) 
            {
               Console.WriteLine(ex.Message);
            }
            return resp;
        }

        private static AmortizationScheduleSummaryResp CalculateProcess(AmortizationScheduleSummaryResp resp, AmortizationScheduleSummaryInput input)
        {
            decimal loanAmount = input.LoanAmount;
            int numOfPayment = input.LoanTerm * 12;
            decimal monthlyInterestRate = (input.AnnualInterestRate / 100) / 12;
            decimal monthlyPayment = CalcMontlyPayment(loanAmount, monthlyInterestRate, numOfPayment);
            decimal totalInterestPaid = 0;
            resp.MonthlyPaymentAmount = Math.Round(monthlyPayment, 2, MidpointRounding.ToEven);
            resp.Payments = new List<Payment>();
            for (int i = 1; i <= numOfPayment; i++)
            {
                if(i == numOfPayment && monthlyPayment > loanAmount) monthlyPayment = CalcLastMonthPayment(loanAmount, monthlyInterestRate);
                Payment payment = new Payment();
                payment.Month = i;
                payment.Amount = Math.Round(monthlyPayment, 2, MidpointRounding.ToEven);
                decimal interestPortionPayment = CalcInterestPortion(monthlyInterestRate, loanAmount);
                payment.InterestPortion = Math.Round(interestPortionPayment, 2, MidpointRounding.ToEven);
                payment.InterestPercentage = Math.Round(CalcInterestPortionPercentage(monthlyPayment, interestPortionPayment), 2, MidpointRounding.ToEven);
                decimal principalPortion = CalcPrincipalPortion(monthlyPayment, interestPortionPayment);
                payment.PrincipalPortion = Math.Round(principalPortion, 2, MidpointRounding.ToEven);
                payment.PrincipalPercentage = Math.Round(CalcPrincipalPortionPercentage(monthlyPayment, principalPortion), 2, MidpointRounding.ToEven);
                payment.Date = (input.InitialDateOfLoan != null) ? input.InitialDateOfLoan.Value.AddMonths(i) : new DateTime().AddMonths(i);
                loanAmount = CalcLoanAmount(loanAmount, principalPortion);
                payment.RemainingAmount = Math.Round(loanAmount, 2, MidpointRounding.ToEven);
                totalInterestPaid = CalcTotalInterestPaid(totalInterestPaid, interestPortionPayment);
                resp.Payments.Add(payment);
            }
            resp.TotalInterestPaid = Math.Round(totalInterestPaid, 2, MidpointRounding.ToEven);
            return resp;
        }
        
        private static decimal CalcMontlyPayment(decimal loanAmount, decimal monthlyInterestRate, int numOfPayment)
        {
            decimal interest = (1 + monthlyInterestRate);
            decimal poweredMonthlyInterestRate = DecimalEx.Pow(interest, numOfPayment);
            decimal monthlyPayment = (loanAmount * monthlyInterestRate * poweredMonthlyInterestRate) / (poweredMonthlyInterestRate - 1);
            return monthlyPayment;
        }

        private static decimal CalcInterestPortion(decimal monthlyInterestRate, decimal loanAmount)
        {
            decimal interestPortion = loanAmount * monthlyInterestRate;
            return interestPortion;
        }

        private static decimal CalcInterestPortionPercentage(decimal monthlyPayment, decimal interestPortionPayment)
        {
            decimal percentage = (interestPortionPayment / monthlyPayment) * 100;
            return percentage;
        }

        private static decimal CalcPrincipalPortion(decimal monthlyPayment, decimal interestPortionPayment)
        {
            decimal result = monthlyPayment - interestPortionPayment;
            return result; 
        }

        private static decimal CalcPrincipalPortionPercentage(decimal monthlyPayment, decimal principalPayment)
        {
            decimal percentage = (principalPayment / monthlyPayment) * 100;
            return percentage;
        }

        private static decimal CalcLoanAmount(decimal loanAmount, decimal principalPortion)
        {
            decimal result = loanAmount - principalPortion;
            return result;
        }

        private static decimal CalcTotalInterestPaid(decimal totalInterestPaid, decimal interestPortion) 
        {
            decimal result = totalInterestPaid + interestPortion;
            return result;
        }

        private static decimal CalcLastMonthPayment(decimal loanAmount, decimal monthlyInterestRate)
        {
            decimal result = loanAmount + (loanAmount * monthlyInterestRate);
            return result;
        }
    }
}
