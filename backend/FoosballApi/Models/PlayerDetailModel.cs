using System.Collections.Generic;
using System.Linq;
using Database.Entities;

namespace FoosballApi.Models
{
    public class PlayerDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int PlayedEvents { get; set; }
        public int FirstPlacesCount { get; set; }
        public double FirstPlacePercentage { get; set; }
        public int SecondPlacesCount { get; set; }
        public double SecondPlacePercentage { get; set; }
        public int ThirdPlacesCount { get; set; }
        public double ThirdPlacePercentage { get; set; }
        
        public double Top3Percentage { get; set; }
        public int PlayedRelevantEvents { get; set; }
        public int RelevantFirstPlacesCount { get; set; }
        public double RelevantFirstPlacePercentage { get; set; }
        public int RelevantSecondPlacesCount { get; set; }
        public double RelevantSecondPlacePercentage { get; set; }
        public int RelevantThirdPlacesCount { get; set; }
        public double RelevantThirdPlacePercentage { get; set; }
        public double RelevantTop3Percentage { get; set; }

        public int YearOfFirstEvent { get; set; }
        public List<TeamModel> Teams { get; set; }
        public List<EventTeamModel> EventTeams { get; set; }

        public PlayerDetailModel(Player entity, List<EventTeamModel> eventTeams)
        {
            Id = entity.Id;
            Name = entity.Name;
            Url = entity.Url;
            PlayedEvents = entity.PlayedEvents;
            FirstPlacesCount = entity.FirstPlacesCount;
            FirstPlacePercentage = entity.FirstPlacePercentage;
            SecondPlacesCount = entity.SecondPlacesCount;
            SecondPlacePercentage = entity.SecondPlacePercentage;
            ThirdPlacesCount = entity.ThirdPlacesCount;
            ThirdPlacePercentage = entity.ThirdPlacePercentage;
            Top3Percentage = entity.Top3Percentage;
            PlayedRelevantEvents = entity.PlayedRelevantEvents;
            RelevantFirstPlacesCount = entity.RelevantFirstPlacesCount;
            RelevantFirstPlacePercentage = entity.RelevantFirstPlacePercentage;
            RelevantSecondPlacesCount = entity.RelevantSecondPlacesCount;
            RelevantSecondPlacePercentage = entity.RelevantSecondPlacePercentage;
            RelevantThirdPlacesCount = entity.RelevantThirdPlacesCount;
            RelevantThirdPlacePercentage = entity.RelevantThirdPlacePercentage;
            RelevantTop3Percentage = entity.RelevantTop3Percentage;
            YearOfFirstEvent = entity.YearOfFirstEvent;
            Teams = entity.Teams.Select(t => new TeamModel(t)).OrderByDescending(t => t.EventsCount).ToList();
            EventTeams = eventTeams.OrderByDescending(et => et.Event.Date).ToList();
        }
    }
}