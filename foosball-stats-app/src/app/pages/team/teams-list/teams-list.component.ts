import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Team } from 'src/app/shared/models/team';
import { TeamService } from 'src/app/shared/services/team.service';

@Component({
  selector: 'app-teams-list',
  templateUrl: './teams-list.component.html',
  styleUrls: ['./teams-list.component.scss'],
})
export class TeamsListComponent implements OnInit {
  teams: Team[];
  constructor(private _teamService: TeamService, private _router: Router) {}

  ngOnInit(): void {
    this._teamService.getTeams().subscribe((t) => (this.teams = t));
  }

  openTeamDetail(e: any): void {
    this._router.navigate(['/team/detail/' + e.data.id]);
  }
}
