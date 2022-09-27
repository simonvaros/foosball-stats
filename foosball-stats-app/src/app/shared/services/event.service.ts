import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Event } from '../models/event';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root',
})
export class EventService extends BaseService {
  endpointUrl = `${environment.apiUrl}/event`;

  constructor(private _httpClient: HttpClient) {
    super();
  }

  getEvents(): Observable<Event[]> {
    return this._httpClient.get<Event[]>(this.endpointUrl, this.httpOptions);
  }

  getById(id: number): Observable<Event> {
    return this._httpClient.get<Event>(
      `${this.endpointUrl}/${id}`,
      this.httpOptions
    );
  }
}
