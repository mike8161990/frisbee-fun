import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { CatchEvent } from '../dtos';
import { TelemetryService } from '../telemetry.service';

@Component({
  selector: 'app-catch-events',
  templateUrl: './catch-events.component.html',
  styleUrls: ['./catch-events.component.scss']
})
export class CatchEventsComponent implements OnInit {
  displayedColumns = ['timestamp', 'telemetryPoints'];
  dataSource = new MatTableDataSource<CatchEvent>();

  constructor(private telemetry: TelemetryService) { }

  ngOnInit(): void {
    this.telemetry.getCatchEvents()
      .subscribe(events => this.dataSource.data = events);
  }
}
