using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FoosballScraper.Models;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace FoosballScraper.Services;

public static class ScrapingService
{
    public static void ScrapeFoosball(List<Event> totalEvents)
    {
        var regions = new List<string>
        { "", "bratislavsky", "trnavsky",
            "nitriansky", "trenciansky", "zilinsky", "bystricky", "presovsky", "kosicky"};


        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var enc1250 = Encoding.GetEncoding(1250);

        HtmlWeb web = new HtmlWeb()
        {
            AutoDetectEncoding = false,
            OverrideEncoding = enc1250,
        };
        
        totalEvents.ForEach(e => e.IsProcessed = true);

        var baseUrl1 = "http://www.foosball.sk/sk/";
        var baseUrl2 = "/turnaje/?filter=&page_no=";
        
        foreach (var region in regions)
        {
            var baseUrl = baseUrl1 + region + baseUrl2;
            
            var events = ProcessRegion(baseUrl, web);
            var newEvents = events.Where(e => !totalEvents.Exists(te => te.EventUrl == e.EventUrl));
            
            totalEvents.AddRange(newEvents);
        }

        foreach (var e in totalEvents.Where(e => !e.IsProcessed))
        {
            Console.WriteLine("Processing event: " + e.EventName);
            ProcessEvent(e, web);
        }

        var jsonString = JsonConvert.SerializeObject(totalEvents);
        File.WriteAllText("TournamentsWithTeams.json", jsonString);
    }
    
    static List<Event> ProcessRegion(string baseUrl, HtmlWeb client)
    {
        var totalEvents = new List<Event>();
        
        Console.WriteLine("Processing region: " + baseUrl);

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

    private static void ProcessEvent(Event e, HtmlWeb client)
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
}