import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { TeamDetailComponent } from "./team-detail/team-detail.component";
import { TeamsListComponent } from "./teams-list/teams-list.component";

const routes: Routes = [
  {
    path: 'list',
    component: TeamsListComponent,
  },
  {
    path: 'detail/:id',
    component: TeamDetailComponent,
  },
  {
    path: '',
    redirectTo: '/list',
    pathMatch: 'full',
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TeamRoutingModule {}
