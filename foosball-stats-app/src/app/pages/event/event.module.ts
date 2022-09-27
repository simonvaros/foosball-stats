import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EventsListComponent } from './events-list/events-list.component';
import { EventDetailComponent } from './event-detail/event-detail.component';
import { EventRoutingModule } from './event-routing.module';
import { DxDataGridModule } from 'devextreme-angular';



@NgModule({
  declarations: [
    EventsListComponent,
    EventDetailComponent
  ],
  imports: [
    CommonModule,
    EventRoutingModule,
    DxDataGridModule
  ]
})
export class EventModule { }
