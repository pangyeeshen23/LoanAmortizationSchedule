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
            double loanAmount = input.LoanAmount;
            int numOfPayment = input.LoanTerm * 12;
            double monthlyInterestRate = (input.AnnualInterestRate / 100) / 12;
            double monthlyPayment = CalcMontlyPayment(loanAmount, monthlyInterestRate, numOfPayment);
            resp.MonthlyPaymentAmount = Math.Round(monthlyPayment, 2, MidpointRounding.ToEven);
            resp.Payments = new List<Payment>();
            for (int i = 1; i <= numOfPayment; i++)
            {
                if(i == numOfPayment && monthlyPayment > loanAmount) monthlyPayment = CalcLastMonthPayment(loanAmount, monthlyInterestRate);
                Payment payment = new Payment();
                payment.Month = i;
                payment.Amount = Math.Round(monthlyPayment, 2, MidpointRounding.ToEven);
                double interestPortionPayment = CalcInterestPortion(monthlyInterestRate, loanAmount);
                payment.InterestPortion = Math.Round(interestPortionPayment, 2, MidpointRounding.ToEven);
                payment.InterestPercentage = Math.Round(CalcInterestPortionPercentage(monthlyPayment, interestPortionPayment), 2, MidpointRounding.ToEven);
                double principalPortion = CalcPrincipalPortion(monthlyPayment, interestPortionPayment);
                payment.PrincipalPortion = Math.Round(principalPortion, 2, MidpointRounding.ToEven);
                payment.PrincipalPercentage = Math.Round(CalcPrincipalPortionPercentage(monthlyPayment, principalPortion), 2, MidpointRounding.ToEven);
                payment.Date = (input.InitialDateOfLoan != null) ? input.InitialDateOfLoan.Value.AddMonths(i) : new DateTime().AddMonths(i);
                loanAmount = CalcLoanAmount(loanAmount, principalPortion);
                payment.RemainingAmount = Math.Round(loanAmount, 2, MidpointRounding.ToEven);
                resp.TotalInterestPaid = Math.Round(CalcTotalInterestPaid(resp.TotalInterestPaid, interestPortionPayment), 2, MidpointRounding.ToEven);
                resp.Payments.Add(payment);
            }

            return resp;
        }
        
        private static double CalcMontlyPayment(double loanAmount, double monthlyInterestRate, int numOfPayment)
        {
            double interest = (1 + monthlyInterestRate);
            double poweredMonthlyInterestRate = Math.Pow(interest, numOfPayment);
            double monthlyPayment = (loanAmount * monthlyInterestRate * poweredMonthlyInterestRate) / (poweredMonthlyInterestRate - 1);
            return monthlyPayment;
        }

        private static double CalcInterestPortion(double monthlyInterestRate, double loanAmount)
        {
            double interestPortion = loanAmount * monthlyInterestRate;
            return interestPortion;
        }

        private static double CalcInterestPortionPercentage(double monthlyPayment, double interestPortionPayment)
        {
            double percentage = (interestPortionPayment / monthlyPayment) * 100;
            return percentage;
        }

        private static double CalcPrincipalPortion(double monthlyPayment, double interestPortionPayment)
        {
            double result = monthlyPayment - interestPortionPayment;
            return result; 
        }

        private static double CalcPrincipalPortionPercentage(double monthlyPayment, double principalPayment)
        {
            double percentage = (principalPayment / monthlyPayment) * 100;
            return percentage;
        }

        private static double CalcLoanAmount(double loanAmount, double principalPortion)
        {
            double result = loanAmount - principalPortion;
            return result;
        }

        private static double CalcTotalInterestPaid(double totalInterestPaid, double interestPortion) 
        {
            double result = totalInterestPaid + interestPortion;
            return result;
        }

        private static double CalcLastMonthPayment(double loanAmount, double monthlyInterestRate)
        {
            double result = loanAmount + (loanAmount * monthlyInterestRate);
            return result;
        }
    }
}
