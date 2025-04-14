import { bootstrapApplication } from '@angular/platform-browser';
import { provideHttpClient } from '@angular/common/http';
import { JobDescriptionFormComponent } from './app/components/job-description-form/job-description-form.component';

bootstrapApplication(JobDescriptionFormComponent, {
  providers: [provideHttpClient()]
}).catch(err => console.error(err));

