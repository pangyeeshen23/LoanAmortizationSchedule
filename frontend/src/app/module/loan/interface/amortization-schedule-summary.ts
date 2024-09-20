import { Payment } from "./payment";

export interface AmortizationScheduleSummary {
    monthlyPaymentAmount: number,
    totalInterestPaid: number,
    payments: Payment[]
}
