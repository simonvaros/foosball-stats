import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TeamsListComponent } from './teams-list/teams-list.component';
import { TeamDetailComponent } from './team-detail/team-detail.component';
import { TeamRoutingModule } from './team-routing.module';
import { DxDataGridModule } from 'devextreme-angular';



@NgModule({
  declarations: [
    TeamsListComponent,
    TeamDetailComponent
  ],
  imports: [
    CommonModule,
    TeamRoutingModule,
    DxDataGridModule
  ]
})
export class TeamModule { }
