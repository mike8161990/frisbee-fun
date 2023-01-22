import { Component } from '@angular/core';
import { TelemetryService } from './telemetry.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(private readonly telemetry: TelemetryService) { }

  triggerCatch(): void {
    this.telemetry.createCatchEvent().subscribe();
  }
}
