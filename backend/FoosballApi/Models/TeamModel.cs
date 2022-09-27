using System.Collections.Generic;
using System.Linq;
using Database.Entities;

namespace FoosballApi.Models
{
    public class TeamModel
    {
        public int Id { get; set; }
        public int EventsCount { get; set; }
        public List<PlayerModel> Players { get; set; }
        public List<EventTeamModel> EventTeams { get; set; }

        public TeamModel(Team entity, bool setEventTeams = true)
        {
            Id = entity.Id;
            Players = entity.Players.Select(p => new PlayerModel(p)).ToList();
            EventsCount = entity.EventTeams.Count;

            if (setEventTeams)
            {
                EventTeams = entity.EventTeams.OrderByDescending(et => et.Event.Date)
                    .Select(et => new EventTeamModel(et)).ToList();
            }
        }
    }
}