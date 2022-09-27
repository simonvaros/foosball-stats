import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';

const routes: Routes = [
  {
    path: 'home',
    component: HomeComponent,
  },
  {
    path: 'player',
    loadChildren: () =>
      import('./pages/player/player.module').then((m) => m.PlayerModule),
  },
  {
    path: 'event',
    loadChildren: () =>
      import('./pages/event/event.module').then((m) => m.EventModule),
  },
  {
    path: 'team',
    loadChildren: () =>
      import('./pages/team/team.module').then((m) => m.TeamModule),
  },
  {
    path: '**',
    redirectTo: 'home',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule],
  declarations: [HomeComponent],
})
export class AppRoutingModule {}
