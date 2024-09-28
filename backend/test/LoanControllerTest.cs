using LoanApi.Controllers;
using LoanApi.Dtos.Inputs;
using LoanApi.Dtos.Outputs;
using LoanApi.Handler;
using LoanApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LoanApiTest
{
    public class LoanControllerTest
    {
        private readonly LoanController _controller;
        private readonly ILoanHandler _handler;
        private readonly string _exampleResultFilePath = System.IO.Directory.GetCurrentDirectory() + 
            @"\Example\AmortizationScheduleResultExample.json";
        public LoanControllerTest()
        {
            _handler = new LoanHandler();
            _controller = new LoanController(_handler);
        }

        [Fact]
        public void AmortizationScheduleSummary_BasicTest()
        {
            AmortizationScheduleSummaryInput input = new AmortizationScheduleSummaryInput();
            Random rnd = new Random();
            input.LoanAmount = rnd.Next(1, 1000000);
            input.AnnualInterestRate = rnd.Next(1, 99);
            input.LoanTerm = rnd.Next(1, 40);
            input.InitialDateOfLoan = DateTime.Parse("2024-09-14T00:00:00");
            AmortizationScheduleSummaryResp response = _controller.GetAmortizationScheduleSummary(input).Value;
            int totalPeriod = input.LoanTerm * 12;
            IList<Payment> schdules = response.Payments;
            decimal remainingAmount = schdules.Last().RemainingAmount;
            Assert.NotNull(response);
            Assert.Equal(totalPeriod, response.Payments.Count);
            Assert.Equal(0, remainingAmount);
        }

        [Fact]
        public void AmortizationScheduleSummary_ExmpleTest()
        {
            AmortizationScheduleSummaryInput input = new AmortizationScheduleSummaryInput();
            Random rnd = new Random();
            input.LoanAmount = 10000;
            input.AnnualInterestRate = 3;
            input.LoanTerm = 4;
            input.InitialDateOfLoan = DateTime.Parse("2024-09-16T02:54:57");
            AmortizationScheduleSummaryResp response = _controller.GetAmortizationScheduleSummary(input).Value;
            AmortizationScheduleSummaryResp exampleResponse = new AmortizationScheduleSummaryResp();
            using (StreamReader r = new StreamReader(_exampleResultFilePath)) {
                string json = r.ReadToEnd();
                exampleResponse = JsonSerializer.Deserialize<AmortizationScheduleSummaryResp>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }

            Assert.True(ValidateResponse(response, exampleResponse));
        }

        private bool ValidateResponse(AmortizationScheduleSummaryResp response, AmortizationScheduleSummaryResp exampleResponse)
        {
            EqualityComparer<decimal> decimalComparer = EqualityComparer<decimal>.Default;
            if (!decimalComparer.Equals(response.MonthlyPaymentAmount, exampleResponse.MonthlyPaymentAmount)) return false;
            if (!decimalComparer.Equals(response.TotalInterestPaid, exampleResponse.TotalInterestPaid)) return false;
            foreach (Payment schedule in exampleResponse.Payments)
            {
                Payment exampleSchedule = exampleResponse.Payments.FirstOrDefault(eschedule => eschedule.Month == schedule.Month);
                if (exampleSchedule == null) return false;
                if (schedule.Month != exampleSchedule.Month) return false;
                if (!decimalComparer.Equals(schedule.Amount, exampleSchedule.Amount)) return false;
                if (!decimalComparer.Equals(schedule.InterestPortion, exampleSchedule.InterestPortion)) return false;
                if (!decimalComparer.Equals(schedule.PrincipalPortion, exampleSchedule.PrincipalPortion)) return false;
                if (!decimalComparer.Equals(schedule.InterestPercentage, exampleSchedule.InterestPercentage)) return false;
                if (!decimalComparer.Equals(schedule.PrincipalPercentage, exampleSchedule.PrincipalPercentage)) return false;
                if (!decimalComparer.Equals(schedule.RemainingAmount, exampleSchedule.RemainingAmount)) return false;
                if (!schedule.Date.Equals(exampleSchedule.Date)) return false;
            }
            return true;
        }

    }
}