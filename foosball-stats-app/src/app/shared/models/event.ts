import { EventType } from "./eventType";

export interface Event {
  id: number;
  name: string;
  description: string;
  type: EventType;
  date: Date;
  teamsCount: number;
}