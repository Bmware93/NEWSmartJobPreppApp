import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { JobService } from '../../services/job.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-job-answers',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './job-answers.component.html',
  styleUrl: './job-answers.component.css'
})
export class JobAnswersComponent implements OnInit {
  answers: any[] = [];

  constructor(private route: ActivatedRoute, private jobService: JobService) {}

  ngOnInit(): void {
      const jobId = this.route.snapshot.params['id'];
      this.jobService.getInterviewAnswersByJobId(jobId).subscribe({
        next: (res) => {
          this.answers = res;
        },
        error: (err) => {
          console.error('Error loading interview feedback', err);
        },
      });
  }

}
