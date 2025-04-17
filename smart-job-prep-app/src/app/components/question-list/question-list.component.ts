import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-question-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl:  './question-list.component.html',
  styleUrls: ['./question-list.component.css']
})
export class QuestionListComponent {
  questions: {id: number, questionText: string}[] = [];

  constructor(private route: ActivatedRoute) {
    const nav = history.state as { questions:  {id: number, questionText: string}[]};
    this.questions = nav.questions ?? [];
  }
}
