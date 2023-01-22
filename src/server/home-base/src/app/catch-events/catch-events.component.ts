import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription, switchMap, timer } from 'rxjs';
import { CatchEvent, TelemetryPoint } from '../dtos';
import { TelemetryService } from '../telemetry.service';

@Component({
  selector: 'app-catch-events',
  templateUrl: './catch-events.component.html',
  styleUrls: ['./catch-events.component.scss']
})
export class CatchEventsComponent implements OnInit, OnDestroy {
  displayedColumns = ['timestamp', 'airTime'];
  dataSource = new MatTableDataSource<CatchEvent>();
  subscription!: Subscription;
  selectedCatch?: CatchEvent;


  constructor(private telemetry: TelemetryService) { }

  ngOnInit(): void {
    this.subscription = timer(0, 1000)
      .pipe(
        switchMap(() => this.telemetry.getCatchEvents())
      )
      .subscribe(events => {
        this.dataSource.data = events;
        if (this.selectedCatch?.catchEventId != this.dataSource.data[0].catchEventId) {
          this.selectedCatch = this.dataSource.data[0];
        }
      });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  getAirTime(telemetryPoints: TelemetryPoint[]): number {
    const first = new Date(telemetryPoints[0].timestamp);
    const last = new Date(telemetryPoints[telemetryPoints.length - 1].timestamp);

    return last.getTime() - first.getTime();
  }
}
