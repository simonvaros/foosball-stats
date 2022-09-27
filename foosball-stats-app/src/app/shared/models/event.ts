import { EventTeam } from "./eventTeam";
import { EventType } from "./eventType";

export interface Event {
  id: number;
  name: string;
  description: string;
  type: EventType;
  date: Date;
  teamsCount: number;
  teams: EventTeam[];
  relevantForRanking: boolean;
}