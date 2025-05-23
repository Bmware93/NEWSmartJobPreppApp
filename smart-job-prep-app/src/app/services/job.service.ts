import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface JobDescription {
  title: string;
  company: string;
  descriptionText: string;
}

export interface JobSummary {
  id: number;
  title: string;
  company: string;
  createdAt: string;
}

export interface JobDescriptionResponse {
  title: string;
  company: string;
  descriptionText: string;
  questions: string[];
}

export interface InterviewAnswerRequest {
  questionId: number;
  answer: string;
}

export interface FeedbackResponse {
  feedback: string;
}

export interface AnswerFeedback {
  questionText: string;
  answer: string;
  feedback: string;
  submittedAt: string;
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
  submitInterviewAnswer(data: InterviewAnswerRequest): Observable<FeedbackResponse> {
    return this.http.post<FeedbackResponse>('https:/localhost:7082/api/interview/feedback', data)
  }
  getAllJobs(): Observable<JobSummary[]> {
    return this.http.get<JobSummary[]>(this.apiUrl);
  }
  getJobById(id: Number): Observable<JobDescription> {
    return this.http.get<JobDescription>(`${this.apiUrl}/${id}`);
  }
  getQuestionsByJobId(jobId: number): Observable<{id: number, questionText: string}[]> {
    return this.http.get<{id: number, questionText: string}[]>(`${this.apiUrl}/${jobId}/questions`);
  }
  getInterviewAnswersByJobId(jobId: number): Observable<AnswerFeedback[]> {
    return this.http.get<AnswerFeedback[]>(`https://localhost:7082/api/Interview/job/${jobId}/answers`);
  }
}
