import { Component, Input, OnChanges } from '@angular/core';
import { Chart } from 'chart.js/auto';
import 'chartjs-adapter-moment';
import { CatchEvent } from '../dtos';

@Component({
  selector: 'app-catch-chart',
  templateUrl: './catch-chart.component.html',
  styleUrls: ['./catch-chart.component.scss']
})
export class CatchChartComponent implements OnChanges {
  @Input() catchEvent?: CatchEvent;

  chart?: Chart;

  ngOnChanges(): void {
    this.createChart();
  }

  createChart() {
    if (!this.catchEvent) {
      return;
    }

    this.chart?.destroy();

    this.chart = new Chart('chart', {
      type: 'line',
      data: {
        labels: this.catchEvent.telemetryPoints.map(p => new Date(p.timestamp)),
        datasets: [
          {
            label: "Acceleration X",
            data: this.catchEvent.telemetryPoints.map(p => p.accelerationX),
            backgroundColor: 'red',
            borderWidth: 5,
            borderColor: 'red',
          },
          {
            label: "Acceleration Y",
            data: this.catchEvent.telemetryPoints.map(p => p.accelerationY),
            backgroundColor: 'green',
            borderWidth: 5,
            borderColor: 'green',
          },
          {
            label: "Acceleration Z",
            data: this.catchEvent.telemetryPoints.map(p => p.accelerationZ),
            backgroundColor: 'blue',
            borderWidth: 5,
            borderColor: 'blue',
          },
        ],
      },
      options: {
        scales: {
          x: {
            type: 'time',
          },
        }
      }
    });
  }
}
