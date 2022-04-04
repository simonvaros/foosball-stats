import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
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
  dataSource: MatTableDataSource<Player>;
  displayedColumns: string[] = [
    'name',
    'playedEvents',
    'firstPlacesCount',
    'firstPlacePercentage',
    'secondPlacesCount',
    'secondPlacePercentage',
    'thirdPlacesCount',
    'thirdPlacePercentage',
    'top3percentage',
    'yearOfFirstEvent',
  ];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private _playerService: PlayerService, private _router: Router) {
    this.dataSource = new MatTableDataSource<Player>();
  }

  ngOnInit(): void {
    this._playerService.getPlayers().subscribe((p) => {
      this.players = p;
      this.dataSource.data = p;
    });
  }
  ngAfterViewInit() {
    console.log(this.sort);
    this.dataSource.sort = this.sort;
  }

  openPlayerDetail(row: any): void {
    this._router.navigate(['/player/detail/' + row.id]);
  }
}
