import { Component } from '@angular/core';
import { AmortizationScheduleSummaryComponent } from './module/loan/component/amortization-schedule/amortization-schedule-summary.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [AmortizationScheduleSummaryComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = "loan-amortization-calculator"
}
