import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription, switchMap, timer } from 'rxjs';
import { CatchEvent } from './dtos';
import { TelemetryService } from './telemetry.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {
  subscription!: Subscription;

  catchEvents?: CatchEvent[];
  selectedCatch?: CatchEvent;

  constructor(private readonly telemetry: TelemetryService) { }

  ngOnInit(): void {
    this.subscription = timer(0, 1000)
      .pipe(
        switchMap(() => this.telemetry.getCatchEvents())
      )
      .subscribe(events => {
        this.catchEvents = events;
        if (this.selectedCatch?.catchEventId != this.catchEvents[0].catchEventId) {
          this.selectedCatch = this.catchEvents[0];
        }
      });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  triggerCatch(): void {
    this.telemetry.createCatchEvent().subscribe();
  }
}
