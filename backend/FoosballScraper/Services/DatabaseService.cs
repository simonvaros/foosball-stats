using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Database;
using Database.Entities;
using Database.Enums;
using Microsoft.EntityFrameworkCore;
using Event = FoosballScraper.Models.Event;

namespace FoosballScraper.Services;

public static class DatabaseService
{
    public static void CleanDatabase(DatabaseContext databaseContext)
    {
        databaseContext.Player.RemoveRange(databaseContext.Player);
        databaseContext.Event.RemoveRange(databaseContext.Event);
        databaseContext.Team.RemoveRange(databaseContext.Team);
        databaseContext.EventTeam.RemoveRange(databaseContext.EventTeam);
        databaseContext.SaveChanges();
    }
    
    public static void AddPlayersFromJsonToDb(DatabaseContext databaseContext, IEnumerable<Event> totalEvents)
    {
        var players = totalEvents.SelectMany(e => e.Teams.SelectMany(t => t.Players))
            .GroupBy(p => p.Id).Select(g => g.First()).ToList();
            
        foreach (var p in players)
        {
            p.Name = p.Name.Trim();
        }
            
        databaseContext.Player.AddRange(players.Select(p => new Player()
        {
            Id = p.Id,
            Name = p.Name,
            Url = p.Url
        }));
            
        databaseContext.SaveChanges();
    }
    
    public static void AddEventsFromJsonToDb(DatabaseContext databaseContext, IEnumerable<Event> totalEvents)
    {
        const string format = "d.M.yy";
            
        var events = totalEvents.Select(e => new Database.Entities.Event()
        {
            Name = e.EventName,
            Description = e.EventDescription,
            Type = e.Type == "Dvojice" ? EventType.Doubles : EventType.Singles,
            Url = e.EventUrl,
            TeamsCount = e.TeamsCount,
            Date = DateTime.ParseExact(e.Date, format, CultureInfo.InvariantCulture)
        }).ToList();

        var irrelevant = new string[] { "ROSENGART CUP", "Amatérsky", "SŠ", "UTORKOVÉ", "RELAX", "TURNAJ V KLUBE" };
        var relevant = new string[] { "Open doubles", "Open singles", "Open dvojice", "Open jednotlivci", "Ženy dvojice", "Ženy jednotlivci", "Women doubles", "Women singles"};

        foreach (var e in events)
        {
            if (irrelevant.Any(i => e.Name.ToLower().Contains(i.ToLower())))
            {
                e.RelevantForRanking = false;
                Console.WriteLine(e.Name + "is not relevant");
                continue;
            }

            if (relevant.Any(r => e.Description.ToLower().Contains(r.ToLower())))
            {
                Console.WriteLine(e.Name + "is relevant");
                e.RelevantForRanking = true;
            }
        }
            
        databaseContext.Event.AddRange(events);
        databaseContext.SaveChanges();
    }

    public static void AddTeamsFromJsonToDb(DatabaseContext databaseContext, IEnumerable<Event> totalEvents)
    {
        var teamsAll = totalEvents.SelectMany(e => e.Teams).ToList();
        var teamsGrouped = teamsAll.GroupBy(t => t.Id).ToList();
        var teamsUnique = teamsGrouped.Select(g => g.First()).ToList();
            
        var teams = teamsUnique.Select(t =>
        {
            var playerEntities = t.Players.Select(p => databaseContext.Player.Find(p.Id)).ToList();
            return new Team()
            {
                Players = playerEntities
            };
        });
            
        databaseContext.Team.AddRange(teams);
        databaseContext.SaveChanges();
    }

    public static void AddEventTeamsFromJsonToDb(DatabaseContext databaseContext, IEnumerable<Event> totalEvents)
    {
        var eventTeams = new List<EventTeam>();
        var allTeams = databaseContext.Team.Include(t => t.Players).ToList();
        var allEvents = databaseContext.Event.ToList();
            
        foreach (var e in totalEvents)
        {
            Console.WriteLine("Processing event: " + e.EventName);
            var dbEventId = allEvents.First(ev => ev.Url == e.EventUrl).Id;
            foreach (var team in e.Teams)
            {
                var dbTeamId = 0;
                    
                if (team.Players.Count == 1)
                {
                    dbTeamId = allTeams.First(t => t.Players.Count == 1 && 
                                                   t.Players.First().Id == team.Players.First().Id).Id;
                }
                else
                {
                    var p1Id = team.Players[0].Id;
                    var p2Id = team.Players[1].Id;
            
                    dbTeamId = allTeams.First(t => t.Players.FirstOrDefault(p => p.Id == p1Id) != null &&
                                                   t.Players.FirstOrDefault(p => p.Id == p2Id) != null).Id;
                }
                    
                var eventTeam = new EventTeam()
                {
                    Ranking = team.Ranking,
                    TeamId = dbTeamId,
                    EventId = dbEventId
                };
                    
                eventTeams.Add(eventTeam);
            }
        }
            
        databaseContext.AddRange(eventTeams);
        databaseContext.SaveChanges();
    }
}