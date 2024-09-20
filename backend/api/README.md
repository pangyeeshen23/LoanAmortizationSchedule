# LoanAmortizationCalculator

Setup Instruction

Step 1: open project in visual studio
Step 2: click run button


### **Steps taken to calculate the amortization schedule**
The steps taken are:
1. Create a AmortizationScheduleSummaryResp Dto that will hold the fields like the Monthly Payment Amount, Total Interest Paid and Payments
1. Calculate the numOfPayment based on the loan term multiple by 12
        ```math
            nP = (lT * 12)
        ```
        Where:
            - nP = total number of payments
            - lT = loan term
2. Calculate the monthly interest rate by using the formula below
        ```math
            r = (aR / 100)/12
        ```
        Where:
            - r = monthly interest rate
            - aR = anuual interest rate

3. Calculate the monthly payment using the formula that had been given
4. Assign the monthly payment to AmortizationScheduleSummaryResp's field MonthlyPaymentAmount
5. Loop thru the total number of payments one by one until it reaches the last of total number of payments 
    5.1. Check whether the current payment is the last of total number of payments and the loan amount is lesser than the usual amount
        5.1.1 if it is then we recalculate the last month payment to close the loan. The formual for this calculation will be 
                ```math
                    M = rLM + (rLM * mR)
                ```
                Where:
                    - M = monthly payment
                    - rP = remaing loan amount (the loan amount after the deduction of each passing payment's principal portion)
                    - r = monthly interest rate
    5.2. Create a Payment Model that hold information like Month, Amount, Interest Portion, Principal Portion, Interest Percentage, Principal Percentage, Remaining Amount and Date
    5.3. Assign the the current loop's index into the Payment's Month field
    5.4. Calculate the interest portion from the monthly payment by multiplying the remaining loan amount and 
            ```math
                iP = (rLM / 100)/12
            ```
            Where:
                - iP = interest portion
                - rLM = remaing loan amount (the loan amount after the deduction of each passing payment's principal portion)
                - r = anuual interest rate
    5.5. Assign interest portion into the Payment Model's Interest Portion field
    5.6. Calculate the interest portion percentage from the monthly payment 
            ```math
                iPP = (iP / M) * 100
            ```
            Where:
                - iPP = interest portion percentage
                - iP = interest portion
                - M = monthly payment
    5.7. Assign interest portion percentage into the Payment Model's Interest Portion Percentage field
    5.8. Calculate the principal portion from the monthly payment 
            ```math
                pP = M - iP
            ```
            Where:
                - pP = principal portion
                - iP = interest portion
                - M = monthly payment
                
    5.9. Assign principal portion into the Payment Model's Principal Portion field
    5.10. Calculate the principal portion percentage from the monthly payment 
            ```math
                pPP = (pP / M) * 100
            ```
            Where:
                - pPP = principal portion percentage
                - pP = principal portion
                - M = monthly payment
    5.11. Assign principal portion percentage into the Payment Model's Principal Portion Percentage field
    5.12. Calculate current month by adding month to the initial date of loan in each loop
    5.13. Assign current month into the Payment Model's Date field
    5.14. Calculate the remaining loan amount 
            ```math
                rLM = (lA - pP)
            ```
            Where:
                - rLM = remaing loan amount (the loan amount after the deduction of each passing payment's principal portion)
                - lA = current loan Amount
                - pP = principal portion

    5.15. Assign remaining loan amount into the Payment Model's Remaining Amount field
    5.16. Calculate the total interest paid
            ```math
                tIP = tIP + iP
            ```
            Where:
                - tIP = total interest paid
                - iP = interest portion
    5.17. Assign total interest paid into the Payment Model's Total Interest Paid field
    5.18. Add the Payment Model to the AmortizationScheduleSummaryResp's Payments Field

### **Assumptions**
There are some assumptions that i had made during the development of this software application
1. Validation Rules of Each Input Fields
    Fields - Amount, Annual Interest Rate, Loan Term, Initial Date Of Loan
    Referred to site https://www.bankrate.com/loans/loan-calculator/
2. The frontend schedule's table header fields - 
    Fields - Month, Date, Payment, Interest Portion, Principal Portion, Interest Percentage, Principal Percentage, Remaining
3. The last month payment amount - To close the loan, i had to recalculate the last month's payment amount when the amount is lesser than the usual monthly payment amount