import { EventTeam } from "./eventTeam";
import { Player } from "./player";

export interface Team {
  id: number;
  players: Player[];
  eventTeams: EventTeam[];
}