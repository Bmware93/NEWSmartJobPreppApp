import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-question-list',
  standalone: true,
  imports: [CommonModule],
  template:  `
  <div class="questions-card" *ngIf="questions.length">
    <h3>Generated Interview Questions</h3>
    <ul>
      <li *ngFor="let question of questions">{{ question }}</li>
    </ul>
  </div>
`,
styles: [`
  .questions-card {
    padding: 20px;
    border: 1px solid #ddd;
    border-radius: 12px;
    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.05);
    margin: 40px;
    background: #fff;
  }
  h3 {
    margin-bottom: 16px;
  }
  li {
    margin-bottom: 12px;
  }
`]
})
export class QuestionListComponent {
  questions: string[] = [];

  constructor(private route: ActivatedRoute) {
    const nav = history.state as { questions: string[] };
    this.questions = nav.questions ?? [];
  }
}
