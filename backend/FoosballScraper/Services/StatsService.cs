using System;
using System.Linq;
using Database;
using Microsoft.EntityFrameworkCore;

namespace FoosballScraper.Services;

public static class StatsService
{
    public static void CountStatisticsForPlayers(DatabaseContext databaseContext)
    {
        var allPlayers = databaseContext.Player.ToList();

        foreach (var player in allPlayers)
        {
            Console.WriteLine("Processing player: " + player.Name);
                
            var playersEvents = databaseContext.EventTeam.Include(e => e.Team)
                .ThenInclude(t => t.Players).Include(e => e.Event)
                .Where(et => et.Team.Players.FirstOrDefault(p => p.Id == player.Id) != null).ToList();

            var sortedEvents = playersEvents.OrderBy(e => e.Event.Date);
                
            player.PlayedEvents = playersEvents.Count();
            player.FirstPlacesCount = playersEvents.Count(e => e.Ranking == 1);
            player.FirstPlacePercentage = (double)player.FirstPlacesCount / player.PlayedEvents * 100.0;
                
            player.SecondPlacesCount = playersEvents.Count(e => e.Ranking == 2);
            player.SecondPlacePercentage = (double)player.SecondPlacesCount / player.PlayedEvents * 100.0;

            player.ThirdPlacesCount = playersEvents.Count(e => e.Ranking == 3);
            player.ThirdPlacePercentage = (double)player.ThirdPlacesCount / player.PlayedEvents * 100.0;

            player.Top3Percentage =
                ((double)(player.FirstPlacesCount + player.SecondPlacesCount + player.ThirdPlacesCount) /
                 player.PlayedEvents) * 100.0;

            player.YearOfFirstEvent = sortedEvents.FirstOrDefault().Event.Date.Year;
            
            var relevantPlayerEvents = playersEvents.Where(et => et.Event.RelevantForRanking).ToList();
            player.PlayedRelevantEvents = relevantPlayerEvents.Count();
            player.RelevantFirstPlacesCount = relevantPlayerEvents.Count(e => e.Ranking == 1);
            player.RelevantFirstPlacePercentage = (double)player.RelevantFirstPlacesCount / player.PlayedRelevantEvents * 100.0;
                
            player.RelevantSecondPlacesCount = relevantPlayerEvents.Count(e => e.Ranking == 2);
            player.RelevantSecondPlacePercentage = (double)player.RelevantSecondPlacesCount / player.PlayedRelevantEvents * 100.0;

            player.RelevantThirdPlacesCount = relevantPlayerEvents.Count(e => e.Ranking == 3);
            player.RelevantThirdPlacePercentage = (double)player.RelevantThirdPlacesCount / player.PlayedRelevantEvents * 100.0;

            player.RelevantTop3Percentage =
                ((double)(player.RelevantFirstPlacesCount + player.RelevantSecondPlacesCount + player.RelevantThirdPlacesCount) /
                 player.PlayedRelevantEvents) * 100.0;
        }

        databaseContext.Player.UpdateRange(allPlayers);
        databaseContext.SaveChanges();
    }
}