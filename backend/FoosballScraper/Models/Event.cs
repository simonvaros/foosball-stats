using System.Collections.Generic;

namespace FoosballScraper.Models;

public class Event
{
    public string EventName { get; set; }
    public string EventUrl { get; set; }
    public string EventDescription { get; set; }
    public string Type { get; set; }
    public string Date { get; set; }
    public int TeamsCount { get; set; }
    public List<Team> Teams { get; set; }
    public bool IsProcessed { get; set; }
}