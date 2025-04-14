import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobDescriptionFormComponent } from './job-description-form.component';

describe('JobDescriptionFormComponent', () => {
  let component: JobDescriptionFormComponent;
  let fixture: ComponentFixture<JobDescriptionFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [JobDescriptionFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(JobDescriptionFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
