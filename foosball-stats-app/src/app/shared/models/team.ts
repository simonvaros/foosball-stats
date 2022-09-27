import { EventTeam } from "./eventTeam";
import { Player } from "./player";

export interface Team {
  id: number;
  eventsCount: number;
  players: Player[];
  eventTeams: EventTeam[];
}