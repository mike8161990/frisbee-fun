import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { CatchEvent, TelemetryPoint } from '../dtos';

@Component({
  selector: 'app-catch-events',
  templateUrl: './catch-events.component.html',
  styleUrls: ['./catch-events.component.scss']
})
export class CatchEventsComponent implements OnChanges {
  @Input() catchEvents?: CatchEvent[];
  displayedColumns = ['catchEventId', 'timestamp', 'hangTime'];
  dataSource = new MatTableDataSource<CatchEvent>();

  constructor() { }

  ngOnChanges(changes: SimpleChanges): void {
    if (!this.catchEvents) {
      return;
    }

    this.dataSource.data = this.catchEvents;
  }

  getHangTime(telemetryPoints: TelemetryPoint[]): number {
    const first = new Date(telemetryPoints[0].timestamp);
    const last = new Date(telemetryPoints[telemetryPoints.length - 1].timestamp);

    return last.getTime() - first.getTime();
  }
}
