import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { Player } from "../models/player";
import { PlayerDetail } from "../models/playerDetail";
import { BaseService } from "./base.service";

@Injectable({
  providedIn: "root",
})
export class PlayerService extends BaseService {
  endpointUrl = `${environment.apiUrl}/player`;

  constructor(private _httpClient: HttpClient) {
    super();
  }

  getPlayers(): Observable<Player[]> {
    return this._httpClient.get<Player[]>(this.endpointUrl, this.httpOptions);
  }

  getById(id: number): Observable<PlayerDetail> {
    return this._httpClient.get<PlayerDetail>(
      `${this.endpointUrl}/${id}`,
      this.httpOptions
    );
  }
}
