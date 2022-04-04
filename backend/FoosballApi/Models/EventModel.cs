using System;
using Database.Entities;
using Database.Enums;

namespace FoosballApi.Models
{
    public class EventModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EventType Type { get; set; }
        public DateTimeOffset Date { get; set; }
        public int TeamsCount { get; set; }

        public EventModel(Event entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
            Type = entity.Type;
            Date = entity.Date;
            TeamsCount = entity.TeamsCount;
        }
    }
}