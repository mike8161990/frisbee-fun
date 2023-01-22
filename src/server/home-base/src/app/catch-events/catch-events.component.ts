import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription, switchMap, timer } from 'rxjs';
import { CatchEvent } from '../dtos';
import { TelemetryService } from '../telemetry.service';

@Component({
  selector: 'app-catch-events',
  templateUrl: './catch-events.component.html',
  styleUrls: ['./catch-events.component.scss']
})
export class CatchEventsComponent implements OnInit, OnDestroy {
  displayedColumns = ['timestamp', 'telemetryPoints'];
  dataSource = new MatTableDataSource<CatchEvent>();

  subscription!: Subscription;

  constructor(private telemetry: TelemetryService) { }

  ngOnInit(): void {
    this.subscription = timer(0, 1000)
      .pipe(
        switchMap(() => this.telemetry.getCatchEvents())
      )
      .subscribe(events => this.dataSource.data = events);
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
