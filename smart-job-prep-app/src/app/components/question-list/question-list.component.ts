import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { JobService } from '../../services/job.service';

@Component({
  selector: 'app-question-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl:  './question-list.component.html',
  styleUrls: ['./question-list.component.css']
})
export class QuestionListComponent {
  questions: { id: number, questionText: string }[] = [];
  answers: { [key: number]: string } = {};
  feedback: { [key: number]: string } = {};
  expanded: { [key: number]: boolean } = {};

  constructor(private route: ActivatedRoute, private jobService: JobService) {
    const nav = history.state as { questions:  {id: number, questionText: string}[]};
    this.questions = nav.questions ?? [];
  }

  submitAnswer(questionId: number) {
    const answerText = this.answers[questionId];
    if(!answerText) {
      alert('Please answer before submitting.');
      return;
    }
    this.jobService.submitInterviewAnswer({
      questionId: questionId,
      answer: answerText
    }).subscribe({
      next: (res)=> {
        this.feedback[questionId] = res.feedback;
      },
      error: (err) => {
        console.error('Submission Error.', err);
        alert('Something went wrong submitting your answer.');
      }
    });
  }
}
