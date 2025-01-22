import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { AppComponent } from './app.component';
import { TaxCalculationResult } from '../models/tax-calculation-result';

describe('AppComponent', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppComponent],
      imports: [
        HttpClientTestingModule,
        ReactiveFormsModule,
        NoopAnimationsModule, // Needed if you are using Angular Material animations
        MatInputModule,
        MatButtonModule
      ]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should create the app', () => {
    expect(component).toBeTruthy();
  });

  it('should send POST request to calculate salary tax when form is valid', () => {
    component.salaryForm.setValue({ salary: 50000 });
    const mockResult: TaxCalculationResult = {
      grossAnnualSalary: 50000,
      grossMonthlySalary: 4166.67,
      netAnnualSalary: 38000,
      netMonthlySalary: 3166.67,
      annualTaxPaid: 12000,
      monthlyTaxPaid: 1000
    };

    component.calculateSalaryTax();

    const req = httpMock.expectOne('/api/calculate');
    expect(req.request.method).toEqual('POST');
    req.flush(mockResult);

    expect(component.result).toEqual(mockResult);
  });

  it('should not send POST request if form is invalid', () => {
    component.salaryForm.setValue({ salary: '' }); // Invalid value
    component.calculateSalaryTax();
    expect(component.salaryForm.valid).toBeFalse();

    const req = httpMock.expectNone('/api/calculate');
  });
});
