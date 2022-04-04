using System;
using System.Collections.Generic;
using Database.Enums;

namespace Database.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public EventType Type { get; set; }
        public DateTimeOffset Date { get; set; }
        public int TeamsCount { get; set; }
        public List<EventTeam> Teams { get; set; }
    }
}