import { Team } from "./team";

export interface EventTeam {
  id: number;
  ranking: number;
  event: Event;
  team: Team;
}