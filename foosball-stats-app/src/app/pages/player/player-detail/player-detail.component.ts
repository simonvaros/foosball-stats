import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PlayerDetail } from 'src/app/shared/models/playerDetail';
import { PlayerService } from 'src/app/shared/services/player.service';

@Component({
  selector: 'app-player-detail',
  templateUrl: './player-detail.component.html',
  styleUrls: ['./player-detail.component.scss'],
})
export class PlayerDetailComponent implements OnInit {
  playerDetail: PlayerDetail;
  eventsColumns: string[] = [
    'eventName',
    'eventDate',
    'ranking',
    'player1',
    'player2',
  ];

  teamsColumns: string[] = ['player1', 'player2', 'playedEvents'];

  constructor(
    private _playerService: PlayerService,
    private _activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    const playerId = Number(this._activatedRoute.snapshot.paramMap.get('id'));
    this._playerService
      .getById(playerId)
      .subscribe((p) => (this.playerDetail = p));
  }
}
