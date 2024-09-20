export interface Payment {
    month: number,
    amount: number,
    interestPortion: number,
    principalPortion: number,
    interestPercentage: number,
    principalPercentage: number,
    remainingAmount: number,
    date: Date
}
