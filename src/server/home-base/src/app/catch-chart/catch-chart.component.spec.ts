import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CatchChartComponent } from './catch-chart.component';

describe('CatchChartComponent', () => {
  let component: CatchChartComponent;
  let fixture: ComponentFixture<CatchChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CatchChartComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CatchChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
