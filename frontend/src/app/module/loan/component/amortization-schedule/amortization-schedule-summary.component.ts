import { Component } from '@angular/core';
import { AmortizationScheduleSummary } from '../../interface/amortization-schedule-summary';
import { LoanService } from '../../service/loan.service';
import { AmortizationScheduleSummaryParam } from '../../interface/param/amortization-schedule-summary-param';
import { AbstractControl, FormControl, FormGroup, ReactiveFormsModule, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { DatePipe, DecimalPipe } from '@angular/common';
import { InputFieldComponent } from '../../../../shared/component/input-field/input-field.component';

@Component({
  selector: 'app-amortization-schedule-summary',
  standalone: true,
  imports: [InputFieldComponent, ReactiveFormsModule, DatePipe, DecimalPipe],
  templateUrl: './amortization-schedule-summary.component.html',
  styleUrl: './amortization-schedule-summary.component.css'
})
export class AmortizationScheduleSummaryComponent {
  constructor(private loanService : LoanService, public datePipe : DatePipe) {}
  summary: AmortizationScheduleSummary | null = null;
  form = new FormGroup({
    loanAmount: new FormControl<Number | null>(null, [Validators.required, Validators.min(1000), this.validateMaxDecimalPlaces(2)]),
    annualInterestRate: new FormControl<Number | null>(null, [Validators.required, Validators.min(2), Validators.max(99), this.validateMaxDecimalPlaces(2)]),
    loanTerm: new FormControl<Number | null>(null, [Validators.required, Validators.min(1), Validators.max(40), this.validateMaxDecimalPlaces(0)]),
    initialDateOfLoan: new FormControl<Date | null>(null, [Validators.required])
  })

  validateMaxDecimalPlaces(maxDecimalPlace: Number): ValidatorFn {
    return (control:AbstractControl): ValidationErrors | null =>{
      const value = control.value;
      if(value == null) return null;
      let parts = value.split(".");
      if(parts[1] == null) return null;
      return (parts[1].length > maxDecimalPlace) ? {validateMaxDecimalPlaces: {maxDecimalPlace: maxDecimalPlace}} : null;
    }
  }

  

  get loanAmountControl(): FormControl{
    return this.form.get('loanAmount') as FormControl;
  }

  get annualInterestRateControl(): FormControl{
    return this.form.get("annualInterestRate") as FormControl;
  }

  get loanTermControl(): FormControl{
    return this.form.get("loanTerm") as FormControl;
  }

  get initialDateOfLoanControl(): FormControl{
    return this.form.get("initialDateOfLoan") as FormControl;
  }
  
  disableButton: Boolean = false
  generateSchedule(){
    this.disableButton = true;
    this.form.markAllAsTouched();
    if(this.form.valid){
      let param : AmortizationScheduleSummaryParam = this.form.value as AmortizationScheduleSummaryParam;
      console.log(param);
      this.loanService.getAmortizationScheduleSummary(param)
      .subscribe(
        (data) => {
          this.summary = data;
          this.disableButton = false;
        },
        (error) => {
          this.disableButton = false;
        }
      )
    }else{
      this.disableButton = false;
    }
  }

}
