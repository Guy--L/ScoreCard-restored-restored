using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoreCard.Models
{
    public class Card
    {
        public int Year { get; set; }
        public List<Line> Lines { get; set; }

        public Card() { }

        public Card(int? year, Worker w)
        {
            Year = year.HasValue ? year.Value : DateTime.Now.AddMonths(6).Year;
            Lines = Line.Card(Year, w);
        }
    }
}