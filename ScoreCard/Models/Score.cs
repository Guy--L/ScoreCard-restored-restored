using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPoco;

namespace ScoreCard.Models
{
    public partial class Score
    {
        private static string _update = @"update score set q{0} = {1} where scoreid = {2}";
        private static string _comment = @"update score set comment = '{0}' where scoreid = {1}";

        [ResultColumn] public int Total { get; set; }
        public string Group { get; set; }
        public int Decimal { get; set; }

        public Score()
        {
            Q1 = Q2 = Q3 = Q4 = 0;
            Target = 0;
        }

        public static void UpdateQuarter(int scoreid, int quarter, int value)
        {
            using (scoreDB s = new scoreDB())
            {
                s.Execute(string.Format(_update, quarter, value, scoreid));
            }
        }

        public static void UpdateComment(int scoreid, string comment)
        {
            using (scoreDB s = new scoreDB())
            {
                s.Execute(string.Format(_comment, comment, scoreid));
            }             
        }

        public Score(int id)
        {
            if (id == 0)
                return;

            Score t = null;
            using (scoreDB s = new scoreDB())
            {
                t = s.Fetch<Score>("where ScoreId = @0", id).FirstOrDefault();
            }
            if (t == null)
                return;
            LineId = t.LineId;
            YearEnding = t.YearEnding;
            Target = t.Target;
            Q1 = t.Q1;
            Q2 = t.Q2;
            Q3 = t.Q3;
            Q4 = t.Q4;
            Comment = t.Comment;
        }

        public Score(int id, int year)
        {
            Score t = null;
            using (scoreDB s = new scoreDB())
            {
                t = s.Fetch<Score>("where LineId = @0 and Year = @1", id, year).FirstOrDefault();
            }
            LineId = id;
            YearEnding = year;
            if (t == null)
                return;

            Target = t.Target;
            Q1 = t.Q1;
            Q2 = t.Q2;
            Q3 = t.Q3;
            Q4 = t.Q4;
            Comment = t.Comment;
        }

        public Score(List<Score> p)
        {
            Q1 = p.Sum(s => s.Q1);
            Q2 = p.Sum(s => s.Q2);
            Q3 = p.Sum(s => s.Q3);
            Q4 = p.Sum(s => s.Q4);
            Total = p.Sum(s => s.Total);
            Target = p.Sum(s => s.Target);
            Decimal = p.First().Decimal;
            if (p.Count > 1)
            {
                GroupId = 0;
                Group = "All";
            }
            else
            {
                GroupId = p.First().GroupId;
                Group = p.First().Group;
            }
        }
    }

    public class ScoreView
    {
        public Line line { get; set; }
        public Score score { get; set; }
 
        public ScoreView(int id, int year)
        {
            score = new Score(id, year);
        }

        public ScoreView(Card card, int id)
        {
            line = card.Lines.Single(l => l.LineId == id);
            var sv = new ScoreView(id, card.Year);
            score = sv.score;
        }

        public int Save()
        {
            score.Save();
            return score.ScoreId;
        }
    }
}