import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Team } from 'src/app/shared/models/team';
import { TeamService } from 'src/app/shared/services/team.service';

@Component({
  selector: 'app-team-detail',
  templateUrl: './team-detail.component.html',
  styleUrls: ['./team-detail.component.scss'],
})
export class TeamDetailComponent implements OnInit {
  teamDetail: Team;

  constructor(
    private _teamService: TeamService,
    private _activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    const teamId = Number(this._activatedRoute.snapshot.paramMap.get('id'));
    this._teamService.getById(teamId).subscribe((t) => (this.teamDetail = t));
  }
}
