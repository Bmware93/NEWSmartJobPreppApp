import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { JobDescription, JobService, JobSummary } from '../../services/job.service';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-previous-jobs',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './previous-jobs.component.html',
  styleUrl: './previous-jobs.component.css'
})
export class PreviousJobsComponent implements OnInit {
  jobs: JobSummary[] = [];

  constructor(private jobService: JobService, private router: Router) {}

  ngOnInit(): void {
      this.jobService.getAllJobs().subscribe({
        next: (res) => this.jobs = res,
        error: (err) => {
          console.error('Error loading job history', err);
          alert('Could not load job history');
        }
      });
  }
  viewJob(jobId: number, title: string, company: string) {
    this.jobService.getQuestionsByJobId(jobId).subscribe({
      next: (questions) => {
        this.router.navigate(['/questions'], {
          state: {
            title,
            company,
            questions
          }
        });
      },
      error: (err) => {
        console.error('Error loading questions', err);
        alert('Could not load questions for that job');
      }
    });
  }
}
