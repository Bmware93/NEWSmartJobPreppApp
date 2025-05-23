import { Routes } from '@angular/router';
import { TabsComponent } from './components/tabs/tabs.component';
import { JobDescriptionFormComponent } from './components/job-description-form/job-description-form.component';
import { QuestionListComponent } from './components/question-list/question-list.component';
import { Component } from '@angular/core';
import { PreviousJobsComponent } from './components/previous-jobs/previous-jobs.component';
import { JobAnswersComponent } from './components/job-answers/job-answers.component';

export const routes: Routes = [
    {
        path: '',
        component: TabsComponent,
        children: [
            { path:'', redirectTo: 'form', pathMatch: 'full' },
            { path:'form', component: JobDescriptionFormComponent },
            { path: 'questions', component: QuestionListComponent },
            { path: 'history', component: PreviousJobsComponent },
            { path: 'job/:id/answers', component: JobAnswersComponent },
            { path: 'job/:id/questions', component: QuestionListComponent }
        ]
    } 
];
