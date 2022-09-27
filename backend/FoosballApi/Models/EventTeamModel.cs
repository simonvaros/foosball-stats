using Database.Entities;

namespace FoosballApi.Models
{
    public class EventTeamModel
    {
        public int Id { get; set; }
        public int Ranking { get; set; }
        public EventModel Event { get; set; }
        
        public TeamModel Team { get; set; }

        public EventTeamModel(EventTeam entity, bool setEvent = true)
        {
            Id = entity.Id;
            Ranking = entity.Ranking;
            Event = setEvent ? new EventModel(entity.Event) : null;
            Team = new TeamModel(entity.Team, false);
        }
    }
}