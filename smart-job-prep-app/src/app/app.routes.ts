import { Routes } from '@angular/router';
import { TabsComponent } from './components/tabs/tabs.component';
import { JobDescriptionFormComponent } from './components/job-description-form/job-description-form.component';
import { QuestionListComponent } from './components/question-list/question-list.component';
import { Component } from '@angular/core';

export const routes: Routes = [
    {
        path: '',
        component: TabsComponent,
        children: [
            { path:'', redirectTo: 'form', pathMatch: 'full' },
            { path:'form', component: JobDescriptionFormComponent },
            { path: 'questions', component: QuestionListComponent }
        ]
    } 
];
