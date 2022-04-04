using Database.Entities;

namespace FoosballApi.Models
{
    public class EventTeamModel
    {
        public int Id { get; set; }
        public int Ranking { get; set; }
        public EventModel Event { get; set; }
        
        public TeamModel Team { get; set; }

        public EventTeamModel(EventTeam entity)
        {
            Id = entity.Id;
            Ranking = entity.Ranking;
            Event = new EventModel(entity.Event);
            Team = new TeamModel(entity.Team, false);
        }
    }
}