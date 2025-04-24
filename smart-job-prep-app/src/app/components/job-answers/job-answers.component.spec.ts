import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobAnswersComponent } from './job-answers.component';

describe('JobAnswersComponent', () => {
  let component: JobAnswersComponent;
  let fixture: ComponentFixture<JobAnswersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [JobAnswersComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(JobAnswersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
