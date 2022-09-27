import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { EventService } from 'src/app/shared/services/event.service';
import { Event } from '../../../shared/models/event';

@Component({
  selector: 'app-events-list',
  templateUrl: './events-list.component.html',
  styleUrls: ['./events-list.component.scss'],
})
export class EventsListComponent implements OnInit {
  events: Event[];

  constructor(private _eventService: EventService, private _router: Router) {}

  ngOnInit(): void {
    this._eventService.getEvents().subscribe((e) => (this.events = e));
  }

  openEventDetail(e: any): void {
    this._router.navigate(['/event/detail/' + e.data.id]);
  }
}
