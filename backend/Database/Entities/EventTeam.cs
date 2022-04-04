namespace Database.Entities
{
    public class EventTeam
    {
        public int Id { get; set; }
        public int Ranking { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}