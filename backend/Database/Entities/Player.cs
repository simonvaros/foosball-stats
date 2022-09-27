using System.Collections.Generic;
using System.Dynamic;

namespace Database.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public List<Team> Teams { get; set; }
        
        public int PlayedEvents { get; set; }
        public int FirstPlacesCount { get; set; }
        public double FirstPlacePercentage { get; set; }
        public int SecondPlacesCount { get; set; }
        public double SecondPlacePercentage { get; set; }
        public int ThirdPlacesCount { get; set; }
        public double ThirdPlacePercentage { get; set; }
        public double Top3Percentage { get; set; }
        
        public int PlayedRelevantEvents { get; set; }
        public int RelevantFirstPlacesCount { get; set; }
        public double RelevantFirstPlacePercentage { get; set; }
        public int RelevantSecondPlacesCount { get; set; }
        public double RelevantSecondPlacePercentage { get; set; }
        public int RelevantThirdPlacesCount { get; set; }
        public double RelevantThirdPlacePercentage { get; set; }
        public double RelevantTop3Percentage { get; set; }

        public int YearOfFirstEvent { get; set; }
    }
}