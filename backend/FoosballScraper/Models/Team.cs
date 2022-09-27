using System.Collections.Generic;

namespace FoosballScraper.Models;

public class Team
{
    public int Ranking { get; set; }
    public List<Player> Players { get; set; }

    public string Id
    {
        get
        {
            if (Players.Count == 1)
            {
                return Players[0].Id.ToString();
            }
            else
            {
                return Players[0].Id > Players[1].Id
                    ? Players[0].Id.ToString() + Players[1].Id.ToString()
                    : Players[1].Id.ToString() + Players[0].Id.ToString();
            }
        }
    }
}