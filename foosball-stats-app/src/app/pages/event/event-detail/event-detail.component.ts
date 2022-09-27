import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Event } from 'src/app/shared/models/event';
import { EventService } from 'src/app/shared/services/event.service';

@Component({
  selector: 'app-event-detail',
  templateUrl: './event-detail.component.html',
  styleUrls: ['./event-detail.component.scss'],
})
export class EventDetailComponent implements OnInit {
  eventDetail: Event;
  constructor(
    private _eventService: EventService,
    private _activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    const eventId = Number(this._activatedRoute.snapshot.paramMap.get('id'));
    this._eventService
      .getById(eventId)
      .subscribe((e) => (this.eventDetail = e));
  }
}
