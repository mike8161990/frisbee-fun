import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CatchEvent } from './dtos';

@Injectable({
  providedIn: 'root'
})
export class TelemetryService {
  constructor(private readonly http: HttpClient) { }

  getCatchEvents(): Observable<CatchEvent[]> {
    return this.http.get<CatchEvent[]>('/api/catch-events');
  }
}
