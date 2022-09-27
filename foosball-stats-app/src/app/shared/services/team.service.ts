import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Team } from '../models/team';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root',
})
export class TeamService extends BaseService {
  endpointUrl = `${environment.apiUrl}/team`;

  constructor(private _httpClient: HttpClient) {
    super();
  }

  getTeams(): Observable<Team[]> {
    return this._httpClient.get<Team[]>(this.endpointUrl, this.httpOptions);
  }

  getById(id: number): Observable<Team> {
    return this._httpClient.get<Team>(
      `${this.endpointUrl}/${id}`,
      this.httpOptions
    );
  }
}
