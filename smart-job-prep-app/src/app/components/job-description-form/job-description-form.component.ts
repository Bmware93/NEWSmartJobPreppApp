import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { JobService } from '../../services/job.service';
import { JobDescriptionResponse } from '../../services/job.service';

@Component({
  selector: 'app-job-description-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './job-description-form.component.html',
  styleUrls: ['./job-description-form.component.css']
})
export class JobDescriptionFormComponent {
  form: FormGroup;
  generatedQuestions: string[] = [];

  constructor(private fb: FormBuilder, private jobService: JobService) {
    this.form = this.fb.group({
      title: ['', Validators.required],
      company: ['', Validators.required],
      descriptionText: ['', Validators.required],
    });
  }
  submit() {
    if (this.form.valid) {
      const { title, descriptionText } = this.form.value;

      this.jobService.submitJobDescription({ title, descriptionText }).subscribe({
        next: (response: JobDescriptionResponse) => {
          this.generatedQuestions = response.questions;
          alert('Job Submitted! Interview Questions have been Generated.');
          this.form.reset();
        },
        error: (error) => {
          console.error('Error submitting job:', error);
          alert('There was an error submitting the job. Please try again.');
        }
      });
    }
  }
}


