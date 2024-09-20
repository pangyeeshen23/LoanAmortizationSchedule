import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AmortizationScheduleSummaryComponent } from './amortization-schedule-summary.component';

describe('AmortizationScheduleSummaryComponent', () => {
  let component: AmortizationScheduleSummaryComponent;
  let fixture: ComponentFixture<AmortizationScheduleSummaryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AmortizationScheduleSummaryComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AmortizationScheduleSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
