import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { finalize } from 'rxjs/operators';
import { TaxCalculationResult } from '../models/tax-calculation-result';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent {
  salaryForm: FormGroup;
  isCalculating = false;
  result?: TaxCalculationResult;

  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.salaryForm = this.fb.group({
      salary: ['', [Validators.required, Validators.min(1)]]
    });
  }

  calculateSalaryTax(): void {
    if (this.salaryForm.valid) {
      this.isCalculating = true;
      this.http.post('/api/calculate', this.salaryForm.value)
      .pipe(
        finalize(() => this.isCalculating = false)
      )
      .subscribe({
        next: (response) => this.result = response as TaxCalculationResult,
        error: (error) => console.error('Error occurred while calculating salary tax:', error)
      });
    }
  }
}
