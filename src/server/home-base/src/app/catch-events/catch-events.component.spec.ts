import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CatchEventsComponent } from './catch-events.component';

describe('CatchEventsComponent', () => {
  let component: CatchEventsComponent;
  let fixture: ComponentFixture<CatchEventsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CatchEventsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CatchEventsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
