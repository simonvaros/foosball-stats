using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Database;
using Database.Entities;
using Database.Enums;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FoosballScraper
{
    class Event
    {
        public string EventName { get; set; }
        public string EventUrl { get; set; }
        public string EventDescription { get; set; }
        public string Type { get; set; }
        public string Date { get; set; }
        public int TeamsCount { get; set; }
        public List<Team> Teams { get; set; }
    }

    class Team
    {
        public int Ranking { get; set; }
        public List<Player> Players { get; set; }
    }

    class TeamComparer : IEqualityComparer<Team>
    {
        public bool Equals(Team x, Team y)
        {
            if (x.Players.Count != y.Players.Count)
            {
                return false;
            }

            if (x.Players.Count == 1)
            {
                var p1 = x.Players[0];
                var p2 = y.Players[0];

                return p1.Id == p2.Id;
            }

            var x1 = x.Players[0].Id;
            var x2 = x.Players[1].Id;
            var y1 = y.Players[0].Id;
            var y2 = y.Players[1].Id;

            if (x1 == y1 && x2 == y2)
            {
                return true;
            }

            if (x1 == y2 && x2 == y1)
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(Team team)
        {
            if (team.Players.Count == 1)
            {
                return team.Players[0].Id;
            }

            return team.Players[0].Id + team.Players[1].Id;
        }
    }

    class Player
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int Id { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var databaseContext = new DatabaseContext();
            CountStatisticsForPlayers(databaseContext);
        }

        static void AddPlayersFromJsonToDb(DatabaseContext databaseContext)
        {
            var jsonString = File.ReadAllText("TournamentsWithTeams.json");
            var totalEvents = JsonConvert.DeserializeObject<List<Event>>(jsonString);

            var players = totalEvents.SelectMany(e => e.Teams.SelectMany(t => t.Players))
                .GroupBy(p => p.Id).Select(g => g.First()).ToList();
            
            foreach (var p in players)
            {
                p.Name = p.Name.Trim();
            }
            
            databaseContext.Player.AddRange(players.Select(p => new Database.Entities.Player()
            {
                Id = p.Id,
                Name = p.Name,
                Url = p.Url
            }));
            
            databaseContext.SaveChanges();
        }

        static void AddEventsFromJsonToDb(DatabaseContext databaseContext)
        {
            var jsonString = File.ReadAllText("TournamentsWithTeams.json");
            var totalEvents = JsonConvert.DeserializeObject<List<Event>>(jsonString);
            
            const string format = "d.M.yy";
            
            var events = totalEvents.Select(e => new Database.Entities.Event()
            {
                Name = e.EventName,
                Description = e.EventDescription,
                Type = e.Type == "Dvojice" ? EventType.Doubles : EventType.Singles,
                Url = e.EventUrl,
                TeamsCount = e.TeamsCount,
                Date = DateTime.ParseExact(e.Date, format, CultureInfo.InvariantCulture)
            });
            
            databaseContext.Event.AddRange(events);
            databaseContext.SaveChanges();
        }

        static void AddTeamsFromJsonToDb(DatabaseContext databaseContext)
        {
            var jsonString = File.ReadAllText("TournamentsWithTeams.json");
            var totalEvents = JsonConvert.DeserializeObject<List<Event>>(jsonString);
            
            var teamsAll = totalEvents.SelectMany(e => e.Teams).ToList();
            var teamsGrouped = teamsAll.GroupBy(t => t, new TeamComparer()).ToList();
            var teamsUnique = teamsGrouped.Select(g => g.First()).ToList();
            
            var teams = teamsUnique.Select(t =>
            {
                var playerEntities = t.Players.Select(p => databaseContext.Player.Find(p.Id)).ToList();
                return new Database.Entities.Team()
                {
                    Players = playerEntities
                };
            });
            
            databaseContext.Team.AddRange(teams);
            databaseContext.SaveChanges();
        }

        static void AddEventTeamsFromJsonToDb(DatabaseContext databaseContext)
        {
            var jsonString = File.ReadAllText("TournamentsWithTeams.json");
            var totalEvents = JsonConvert.DeserializeObject<List<Event>>(jsonString);
            
            var eventTeams = new List<EventTeam>();
            var allTeams = databaseContext.Team.Include(t => t.Players).ToList();
            
            foreach (var e in totalEvents)
            {
                Console.WriteLine("Processing event: " + e.EventName);
                var dbEventId = databaseContext.Event.First(ev => ev.Url == e.EventUrl).Id;
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

        static void CountStatisticsForPlayers(DatabaseContext databaseContext)
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

            }

            databaseContext.Player.UpdateRange(allPlayers);
            databaseContext.SaveChanges();
        }

        static void ScrapeFoosball()
        {
            var regions = new List<string>{ "", "bratislavsky", "trnavsky",
                "nitriansky", "trenciansky", "zilinsky", "bystricky", "presovsky", "kosicky"};


            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var enc1250 = Encoding.GetEncoding(1250);

            HtmlWeb web = new HtmlWeb()
            {
                AutoDetectEncoding = false,
                OverrideEncoding = enc1250,
            };

            // var baseUrl1 = "http://www.foosball.sk/sk/";
            // var baseUrl2 = "/turnaje/?filter=&page_no=";
            //
            // var totalEvents = new List<Event>();
            //
            // foreach (var region in regions)
            // {
            //     var baseUrl = baseUrl1 + region + baseUrl2;
            //
            //     var events = ProcessRegion(baseUrl, web);
            //
            //     totalEvents.AddRange(events);
            // }

            var jsonString = File.ReadAllText("Tournaments.json");
            var totalEvents = JsonConvert.DeserializeObject<List<Event>>(jsonString);

            foreach (var e in totalEvents)
            {
                Console.WriteLine("Processing event: " + e.EventName);
                ProcessEvent(e, web);
            }

            jsonString = JsonConvert.SerializeObject(totalEvents);
            File.WriteAllText("TournamentsWithTeams.json", jsonString);
        }

        static void ProcessEvent(Event e, HtmlWeb client)
        {
            const string playerBaseUrl = "http://www.foosball.sk/sk/hledej/?action=player_detail&player_id=";
            var pageDocument = client.Load(e.EventUrl);
            var tableRows = pageDocument.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/div[2]/div[2]/div[2]/table[1]/tr");

            e.Teams = new List<Team>();

            foreach (var item in tableRows)
            {
                if (!item.HasClass("even") && !item.HasClass("odd"))
                {
                    continue;
                }

                var team = new Team();
                
                var childNodes = item.ChildNodes.Where(n => n.Name != "#text").ToList();

                var ranking = childNodes[0].InnerHtml.Replace(".", "");
                team.Ranking = int.Parse(ranking);

                var aNodes = childNodes[1].ChildNodes.Where(n => n.Name == "a").ToList();
                
                var player = new Player();
                player.Url = aNodes[0].GetAttributeValue("href", string.Empty).Replace("amp;", "");
                player.Name = aNodes[0].InnerHtml;
                player.Id = int.Parse(player.Url.Replace(playerBaseUrl, ""));

                team.Players = new List<Player>();
                
                team.Players.Add(player);

                if (aNodes.Count == 2)
                {
                    var player2 = new Player();
                    player2.Url = aNodes[1].GetAttributeValue("href", string.Empty).Replace("amp;", "");
                    player2.Name = aNodes[1].InnerHtml;
                    player2.Id = int.Parse(player2.Url.Replace(playerBaseUrl, ""));
                    
                    team.Players.Add(player2);
                }
                
                e.Teams.Add(team);
            }
        }

        static List<Event> ProcessRegion(string baseUrl, HtmlWeb client)
        {
            var totalEvents = new List<Event>();

            for (var i = 0; i < 50; i++)
            {
                Console.WriteLine("Going to process page number: " + i);

                var url = baseUrl + i.ToString();

                var events = ProcessRegionPage(url, client);

                if (events.Count == 0)
                {
                    break;
                }

                totalEvents.AddRange(events);
            }

            return totalEvents;
        }

        static List<Event> ProcessRegionPage(string url, HtmlWeb client)
        {
            var pageDocument = client.Load(url);
            var tableRows = pageDocument.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/div[2]/div[2]/div[3]/table[1]/tr");

            var events = new List<Event>();

            foreach (var item in tableRows)
            {
                if (!item.HasClass("even") && !item.HasClass("odd"))
                {
                    continue;
                }

                var childNodes = item.ChildNodes.Where(n => n.Name != "#text").ToList();
                var childChildNodes = childNodes[0].ChildNodes.Where(n => n.Name != "#text").ToList();

                var eventName = Regex.Replace(childChildNodes[0].InnerHtml, @"\s+", " ");
                var eventUrl = childChildNodes[0].GetAttributeValue("href", string.Empty).Replace("amp;", "");
                var eventDesc = Regex.Replace(childChildNodes[2].InnerHtml, @"\s+", " "); ;
                var type = childNodes[1].InnerHtml;
                var date = childNodes[2].InnerHtml;
                var teamsCount = childNodes[3].InnerHtml;

                var e = new Event()
                {
                    EventName = eventName,
                    EventUrl = eventUrl,
                    EventDescription = eventDesc,
                    Type = type,
                    Date = date,
                    TeamsCount = int.Parse(teamsCount)
                };

                events.Add(e);
            }

            return events;
        }
    }
}
