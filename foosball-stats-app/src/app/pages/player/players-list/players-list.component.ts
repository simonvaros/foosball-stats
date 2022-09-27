import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Player } from 'src/app/shared/models/player';
import { PlayerService } from 'src/app/shared/services/player.service';

@Component({
  selector: 'app-players-list',
  templateUrl: './players-list.component.html',
  styleUrls: ['./players-list.component.scss'],
})
export class PlayersListComponent implements OnInit {
  players: Player[];
  showRelevant = false;

  constructor(private _playerService: PlayerService, private _router: Router) {
    this.displayFirstPlacePercentage =
      this.displayFirstPlacePercentage.bind(this);
    this.displaySecondPlacePercentage =
      this.displaySecondPlacePercentage.bind(this);
    this.displayThirdPlacePercentage =
      this.displayThirdPlacePercentage.bind(this);
    this.displayTop3Percentage = this.displayTop3Percentage.bind(this);
  }

  ngOnInit(): void {
    this._playerService.getPlayers().subscribe((p) => {
      this.players = p;
    });
  }

  openPlayerDetail(e: any): void {
    this._router.navigate(['/player/detail/' + e.data.id]);
  }

  displayFirstPlacePercentage(e: any): string {
    return this.displayPercent(this.showRelevant ? e.relevantFirstPlacePercentage : e.firstPlacePercentage);
  }

  displaySecondPlacePercentage(e: any): string {
    return this.displayPercent(this.showRelevant ? e.relevantSecondPlacePercentage : e.secondPlacePercentage);
  }

  displayThirdPlacePercentage(e: any): string {
    return this.displayPercent(this.showRelevant ? e.relevantThirdPlacePercentage : e.thirdPlacePercentage);
  }

  displayTop3Percentage(e: any): string {
    return this.displayPercent(this.showRelevant ? e.relevantTop3Percentage : e.top3Percentage);
  }

  private displayPercent(data: number): string {
    return `${data.toPrecision(4)} %`;
  }
}
