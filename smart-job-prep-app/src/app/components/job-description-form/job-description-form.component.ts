import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-job-description-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './job-description-form.component.html',
  styleUrls: ['./job-description-form.component.css']
})
export class JobDescriptionFormComponent {
  form: FormGroup;

  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.form = this.fb.group({
      title: ['', Validators.required],
      company: ['', Validators.required],
      descriptionText: ['', Validators.required],
    });
  }
  submit() {
    if (this.form.valid) {
      this.http.post('https://localhost:7082/api/jobdescription', this.form.value)
        .subscribe({
          next: response => {
            alert('Post successfully submitted');
            console.log('Job submitted successfully!', response);
          },
          error: error => {
            console.error('There was an error submitting the job.', error);
            alert('There was an error submitting the job. Please try again.');
          }
        });
    }
  }
}


