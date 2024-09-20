import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AmortizationScheduleSummary } from '../interface/amortization-schedule-summary';
import { AmortizationScheduleSummaryParam } from '../interface/param/amortization-schedule-summary-param';
import { URL_CONFIG } from '../../../shared/token/api-url';
import { UrlConfig } from '../../../shared/interface/url-config';

@Injectable({
  providedIn: 'root'
})
export class LoanService {
  constructor(@Inject(URL_CONFIG)public urlConfig: UrlConfig, private http:HttpClient) { }
  getAmortizationScheduleSummary(input: AmortizationScheduleSummaryParam): Observable<AmortizationScheduleSummary>{
    return this.http.post<AmortizationScheduleSummary>(this.urlConfig.baseApiUrl+"/Loan/AmortizationScheduleSummary", input);
  }
}
