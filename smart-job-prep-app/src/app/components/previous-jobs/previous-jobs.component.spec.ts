import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PreviousJobsComponent } from './previous-jobs.component';

describe('PreviousJobsComponent', () => {
  let component: PreviousJobsComponent;
  let fixture: ComponentFixture<PreviousJobsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PreviousJobsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PreviousJobsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
