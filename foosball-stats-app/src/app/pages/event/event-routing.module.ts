import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { EventDetailComponent } from "./event-detail/event-detail.component";
import { EventsListComponent } from "./events-list/events-list.component";

const routes: Routes = [
  {
    path: 'list',
    component: EventsListComponent,
  },
  {
    path: 'detail/:id',
    component: EventDetailComponent,
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
export class EventRoutingModule {}
