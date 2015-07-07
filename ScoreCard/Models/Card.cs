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
        public bool NoScores { get; set; }

        public Card() { }

        public Card(int year, Worker w)
        {
            Year = year;
            Lines = Line.Card(Year, w);
        }
    }
}