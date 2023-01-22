import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CatchEvent } from './dtos';

@Injectable({
  providedIn: 'root'
})
export class TelemetryService {
  constructor(private readonly http: HttpClient) { }

  createCatchEvent(): Observable<unknown> {
    return this.http.post<unknown>('/api/catch-events', null);
  }

  getCatchEvents(): Observable<CatchEvent[]> {
    return this.http.get<CatchEvent[]>('/api/catch-events');
  }
}
