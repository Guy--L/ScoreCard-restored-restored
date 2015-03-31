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
        private static string _insertq = @"insert into score (yearending, lineid, q{0}, groupid) values (2014, {1}, {2}, 0); SELECT SCOPE_IDENTITY()";
        private static string _insertc = @"insert into score (yearending, lineid, comment, groupid) values (2014, {0}, {1}, 0); SELECT SCOPE_IDENTITY()";

        [ResultColumn] public int Total { get; set; }
        public string Group { get; set; }
        public int Decimal { get; set; }
        public bool CanEdit { get; set; }

        public Score()
        {
            Q1 = Q2 = Q3 = Q4 = 0;
            Target = 0;
        }

        public static int SaveQuarter(int scoreid, int quarter, int value)
        {
            using (scoreDB s = new scoreDB())
            {
                if (scoreid > 0)
                    s.Execute(string.Format(_update, quarter, value, scoreid));
                else
                    scoreid = -s.ExecuteScalar<int>(string.Format(_insertq, quarter, -scoreid, value));
            }
            return scoreid;
        }

        public static int SaveComment(int scoreid, string comment)
        {
            using (scoreDB s = new scoreDB())
            {
                if (scoreid > 0)
                    s.Execute(string.Format(_comment, comment, scoreid));
                else
                    scoreid = -s.ExecuteScalar<int>(string.Format(_insertc, -scoreid, comment));
            }             
            return scoreid;
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
            GroupId = 0;
            Group = "All";
            if (p.Count == 1 && p.First().GroupId == 0)
            {
                ScoreId = p.First().ScoreId;
                LineId = p.First().LineId;
                p.Clear();
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