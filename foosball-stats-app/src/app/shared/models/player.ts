export interface Player {
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
}
