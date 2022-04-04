import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { PlayerDetailComponent } from "./player-detail/player-detail.component";
import { PlayersListComponent } from "./players-list/players-list.component";

const routes: Routes = [
  {
    path: 'list',
    component: PlayersListComponent,
  },
  {
    path: 'detail/:id',
    component: PlayerDetailComponent,
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
export class PlayerRoutingModule {}
