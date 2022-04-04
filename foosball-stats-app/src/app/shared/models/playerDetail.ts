import { EventTeam } from "./eventTeam";
import { Team } from "./team";

export interface PlayerDetail {
  id: number;
  name: string;
  url: string;
  playedEvents: number;
  firstPlacesCount: number;
  firstPlacePercentage: number;
  secondPlacesCount: number;
  secondPlacePercentage: number;
  thirdPlacesCount: number;
  thirdPlacePercentage: number;
  top3Percentage: number;
  yearOfFirstEvent: number;

  teams: Team[];
  eventTeams: EventTeam[];

}
