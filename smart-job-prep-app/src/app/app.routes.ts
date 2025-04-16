import { Routes } from '@angular/router';
import { JobDescriptionFormComponent } from './components/job-description-form/job-description-form.component';
import { QuestionListComponent } from './components/question-list/question-list.component';

export const routes: Routes = [
    { path:'', component: JobDescriptionFormComponent },
    { path: 'questions', component: QuestionListComponent }
];
