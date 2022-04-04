using System.Collections.Generic;

namespace Database.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public List<Player> Players { get; set; }
        public List<EventTeam> EventTeams { get; set; }
    }
}