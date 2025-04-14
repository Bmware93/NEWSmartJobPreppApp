import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface JobDescription {
  title: string;
  descriptionText: string;
}

export interface JobDescriptionResponse {
  title: string;
  descriptionText: string;
  questions: string[];
}

@Injectable({
  providedIn: 'root'
})
export class JobService {
  private apiUrl = 'https://localhost:7082/api/jobdescription';

  constructor(private http: HttpClient) { }

  submitJobDescription(job: JobDescription): Observable<JobDescriptionResponse> {
    return this.http.post<JobDescriptionResponse>(this.apiUrl, job)
  }
}
