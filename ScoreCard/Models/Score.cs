﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using NPoco;

namespace ScoreCard.Models
{
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

    public static class MyExtensions
    {
        public static int divisor(this IEnumerable<Score> list, Func<Score, int?> member)
        {
            var count = list.Where(s => member(s).HasValue).Sum(s => 1);
            return count == 0 ? 1 : count;
        }

        public static int? top(this Line ln, Func<Score, int?> m)
        {
            int? bottom = ln.bottomup == null ? null : m(ln.bottomup);
            return m(ln.topdown).HasValue ? m(ln.topdown).Value : bottom;
        }

        /// <summary>
        /// calculate over column (member) in list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public static int? calc(this IEnumerable<Score> list, Func<Score, int?> member)
        {
            var f = list.First();
            var avg = f.t8;

            var t8 = avg && (f.scores==null || f.scores.Count() == 0);      // complex to make 8-rule only apply to leaf nodes
            int? c;

            if (t8)
                c = list.Where(s => member(s) >= 800).Sum(s => 1);
            else
            {
                c = list.Any(s => member(s)!=null)?list.Sum(s => member(s)):null;
            }

            if (t8) c *= 100;
            if (avg || f.vavg) c /= list.divisor(s => member(s));
            if (t8) c *= 100;

            return c;
        }
    }

    public partial class Score
    {
        [ResultColumn] public int? PriorTotal { get; set; }
        public string Group { get; set; }
        public string Site { get; set; }
        public int Decimal { get; set; }
        public bool CanEdit { get; set; }
        public bool havg { get; set; }
        public bool vavg { get; set; }
        public bool avg { get; set; }
        public bool avgq { get; set; }
        public bool t8 { get; set; }
        public bool recent { get; set; }
        public bool ShowTotal { get; set; }
        public List<Score> scores { get; set; }

        public Score()
        {
        }

        public static List<int> yearsready;

        private static string _urupdate = @"update score set q{0} = {1} where scoreid = {2}";
        private static string _update = @"
            declare @@old int
            select @@old = q{0} from score where scoreid = {2} " + _urupdate + @"
            select @@old";

        public static int? SaveScore(int scoreid, int quarter, int? value)
        {
            dynamic val;
            if (value.HasValue)
                val = value.Value;
            else
                val = "null";

            int? old = null;
            using (scoreDB s = new scoreDB())
            {
                old = s.ExecuteScalar<int?>(string.Format(_update, quarter, val, scoreid));
            }
            return old;
        }

        private static string _urcomment = @"update score set comment = '{0}' where scoreid = {1}";
        private static string _comment = @"
            declare @@cmt varchar(50)
            select @@cmt = comment from score where scoreid = {1} "+_urcomment+@"
            select @@cmt";

        public static string SaveScore(int scoreid, string comment)
        {
            string old = null;
            using (scoreDB s = new scoreDB())
            {
                old = s.ExecuteScalar<string>(string.Format(_comment, comment, scoreid));
            }
            return old;
        }

        private static string _urtarget = @"update score set target = {0} where scoreid = {1} ";
        private static string _target = @"
            declare @@old int
            select @@old = target from score where scoreid = {1} "+_urtarget+@"
            select @@old";

        public static int? SaveScore(int scoreid, int? target)
        {
            dynamic val;
            if (target.HasValue)
                val = target.Value;
            else
                val = "null";

            int? old = null;
            using (scoreDB s = new scoreDB())
            {
                old = s.ExecuteScalar<string>(string.Format(_target, val, scoreid));
            }
            return old;
        }

        public Score link(Line ln, Group gr, Site st)
        {
            GroupId = gr.GroupId;
            Group = gr._Group;
            SiteId = st.SiteId;
            Site = st._Site;

            LineId = ln.LineId;
            ShowTotal = ln.ShowTotal;

            Decimal = ln.DecimalPoint;
            havg = ln.horizontalavg;
            vavg = ln.verticalavg;

            recent = ln.symbol == "recent";
            avg = ln.symbol == "%" || ln.symbol == "sum";
            avgq = ln.symbol == "%";
            t8 = ln.symbol == "%>=8";

            if (havg)
            {
                Total /= count(true);
                PriorTotal /= 4;
            }
            else if (recent)
            {
                Total = Q4 ?? Q3 ?? Q2 ?? Q1;
            }

            if (GroupId == 0 && SiteId == 0)
            {
                ln.topdown = this;
                return null;                    // overriding line-level score
            }

            return this;                        // site-level score
        }

        
        public int count(bool normal)
        {
            var n = (Q1.HasValue?1:0) + (Q2.HasValue?1:0) + (Q3.HasValue?1:0) + (Q4.HasValue?1:0);
            return (n == 0 && normal) ? 1 : n;
        }

        //public string cue
        //{
        //    get
        //    {
        //        var q = 0;
        //        if (Q4 != 0) q = 4;
        //        else if (Q3 != 0) q = 3;
        //        else if (Q2 != 0) q = 2;
        //        else if (Q1 != 0) q = 1;
        //        if (Total == 0) return "";
        //        double perform = ((double)Target * q) / (4.0 * (double)Total);
        //        if (perform > 1.0) return " text-danger";
        //        if (perform < 1.0) return " text-success";
        //        return " text-primary";
        //    }
        //}

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
            ShowTotal = t.ShowTotal;
            Target = t.Target;
            Q1 = t.Q1;
            Q2 = t.Q2;
            Q3 = t.Q3;
            Q4 = t.Q4;
            Total = Q1 + Q2 + Q3 + Q4;
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

            ShowTotal = t.ShowTotal;
            Target = t.Target;
            Q1 = t.Q1;
            Q2 = t.Q2;
            Q3 = t.Q3;
            Q4 = t.Q4;
            Total = Q1 + Q2 + Q3 + Q4;
            Debug.WriteLine("2Total " + Total + ".");
            Comment = t.Comment;
        }

        public Score(List<Score> p)
        {
            Score ln = null;
            if (p == null || p.Any(q => q.GroupId == null || q.SiteId == null))
                ln = new Score() { LineId = (p == null) ? 0 : p.First().LineId };

            var lns = p.Where(q => q.GroupId == 0 && q.SiteId == 0);                    // get top level line numbers entered on top of any detail
            var r = p.Where(q => q.GroupId != 0 || q.SiteId != 0);                      // get group and site numbers

            if (lns == null || lns.Count() == 0)
                ln = new Score() { 
                    Q1 = null, 
                    Q2 = null, 
                    Q3 = null, 
                    Q4 = null, 
                    Target = null, 
                    PriorTotal = null, 
                    vavg = r.First().vavg,
                    havg = r.First().havg, 
                    ScoreId = -r.First().ScoreId, 
                    Decimal = r.First().Decimal,
                    LineId = r.First().LineId,
                    ShowTotal = r.First().ShowTotal
                };
            else
            {
                if (lns.Count() > 1)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("Multiple summary scoreids " + string.Join(",", lns.Select(n => n.ScoreId.ToString()).ToArray()) + " encountered for lineid " + lns.First().LineId));
                }
                ln = lns.First();
            }

            ShowTotal = ln.ShowTotal;
            LineId = ln.LineId;
            ScoreId = ln.ScoreId;
            Decimal = ln.Decimal;
            havg = ln.havg;
            vavg = ln.vavg;

            avg = ln.avg;
            avgq = ln.avgq;

            Q1 = ln.Q1 ?? r.calc(s => s.Q1);
            Q2 = ln.Q2 ?? r.calc(s => s.Q2);
            Q3 = ln.Q3 ?? r.calc(s => s.Q3);
            Q4 = ln.Q4 ?? r.calc(s => s.Q4);
            Total = ln.Total ?? r.calc(s => s.Total);

            Target = ln.Target ?? r.calc(s => s.Target);
            PriorTotal = ln.PriorTotal ?? r.calc(s => s.PriorTotal);
        }

        public static string _blankyear = @"
declare @@year integer

select @@year = @0

INSERT INTO [dbo].[Score]  ([YearEnding],[LineId],[GroupId],[SiteId]) VALUES 
	(@@year,12,0,0),
	(@@year,13,0,0),
	(@@year,14,0,0),
	(@@year,14,1,1),
	(@@year,14,1,2),
	(@@year,14,1,3),
	(@@year,14,1,4),
	(@@year,14,1,5),
	(@@year,14,1,6),
	(@@year,14,1,7),
	(@@year,14,1,8),
	(@@year,14,1,9),
	(@@year,14,1,10),
	(@@year,14,1,11),
	(@@year,14,1,12),
	(@@year,14,1,13),
	(@@year,14,1,14),
	(@@year,14,1,15),
	(@@year,14,1,16),
	(@@year,14,1,17),
	(@@year,14,1,18),
	(@@year,14,1,19),
	(@@year,14,1,20),
	(@@year,14,1,21),
	(@@year,15,0,0),
	(@@year,15,1,1),
	(@@year,15,1,2),
	(@@year,15,1,3),
	(@@year,15,1,4),
	(@@year,15,1,5),
	(@@year,15,1,6),
	(@@year,15,1,7),
	(@@year,15,1,8),
	(@@year,15,1,9),
	(@@year,15,1,10),
	(@@year,15,1,11),
	(@@year,15,1,12),
	(@@year,15,1,13),
	(@@year,15,1,14),
	(@@year,15,1,15),
	(@@year,15,1,16),
	(@@year,15,1,17),
	(@@year,15,1,18),
	(@@year,15,1,19),
	(@@year,15,1,20),
	(@@year,15,1,21),
	(@@year,15,2,1),
	(@@year,15,2,2),
	(@@year,15,2,3),
	(@@year,15,2,4),
	(@@year,15,2,5),
	(@@year,15,2,6),
	(@@year,15,2,7),
	(@@year,15,2,8),
	(@@year,15,2,9),
	(@@year,15,2,10),
	(@@year,15,2,11),
	(@@year,15,2,12),
	(@@year,15,2,13),
	(@@year,15,2,14),
	(@@year,15,2,15),
	(@@year,15,2,16),
	(@@year,15,2,17),
	(@@year,15,2,18),
	(@@year,15,2,19),
	(@@year,15,2,20),
	(@@year,15,2,21),
	(@@year,15,3,1),
	(@@year,15,3,2),
	(@@year,15,3,3),
	(@@year,15,3,4),
	(@@year,15,3,5),
	(@@year,15,3,6),
	(@@year,15,3,7),
	(@@year,15,3,8),
	(@@year,15,3,9),
	(@@year,15,3,10),
	(@@year,15,3,11),
	(@@year,15,3,12),
	(@@year,15,3,13),
	(@@year,15,3,14),
	(@@year,15,3,15),
	(@@year,15,3,16),
	(@@year,15,3,17),
	(@@year,15,3,18),
	(@@year,15,3,19),
	(@@year,15,3,20),
	(@@year,15,3,21),
	(@@year,16,0,0),
	(@@year,16,1,1),
	(@@year,16,1,2),
	(@@year,16,1,3),
	(@@year,16,1,4),
	(@@year,16,1,5),
	(@@year,16,1,6),
	(@@year,16,1,7),
	(@@year,16,1,8),
	(@@year,16,1,9),
	(@@year,16,1,10),
	(@@year,16,1,11),
	(@@year,16,1,12),
	(@@year,16,1,13),
	(@@year,16,1,14),
	(@@year,16,1,15),
	(@@year,16,1,16),
	(@@year,16,1,17),
	(@@year,16,1,18),
	(@@year,16,1,19),
	(@@year,16,1,20),
	(@@year,16,1,21),
	(@@year,16,2,1),
	(@@year,16,2,2),
	(@@year,16,2,3),
	(@@year,16,2,4),
	(@@year,16,2,5),
	(@@year,16,2,6),
	(@@year,16,2,7),
	(@@year,16,2,8),
	(@@year,16,2,9),
	(@@year,16,2,10),
	(@@year,16,2,11),
	(@@year,16,2,12),
	(@@year,16,2,13),
	(@@year,16,2,14),
	(@@year,16,2,15),
	(@@year,16,2,16),
	(@@year,16,2,17),
	(@@year,16,2,18),
	(@@year,16,2,19),
	(@@year,16,2,20),
	(@@year,16,2,21),
	(@@year,16,3,1),
	(@@year,16,3,2),
	(@@year,16,3,3),
	(@@year,16,3,4),
	(@@year,16,3,5),
	(@@year,16,3,6),
	(@@year,16,3,7),
	(@@year,16,3,8),
	(@@year,16,3,9),
	(@@year,16,3,10),
	(@@year,16,3,11),
	(@@year,16,3,12),
	(@@year,16,3,13),
	(@@year,16,3,14),
	(@@year,16,3,15),
	(@@year,16,3,16),
	(@@year,16,3,17),
	(@@year,16,3,18),
	(@@year,16,3,19),
	(@@year,16,3,20),
	(@@year,16,3,21),
	(@@year,17,0,0),
	(@@year,17,3,1),
	(@@year,17,3,2),
	(@@year,17,3,3),
	(@@year,17,3,4),
	(@@year,17,3,5),
	(@@year,17,3,6),
	(@@year,17,3,7),
	(@@year,17,3,8),
	(@@year,17,3,9),
	(@@year,17,3,10),
	(@@year,17,3,11),
	(@@year,17,3,12),
	(@@year,17,3,13),
	(@@year,17,3,14),
	(@@year,17,3,15),
	(@@year,17,3,16),
	(@@year,17,3,17),
	(@@year,17,3,18),
	(@@year,17,3,19),
	(@@year,17,3,20),
	(@@year,17,3,21),
	(@@year,18,0,0),
	(@@year,18,3,1),
	(@@year,18,3,2),
	(@@year,18,3,3),
	(@@year,18,3,4),
	(@@year,18,3,5),
	(@@year,18,3,6),
	(@@year,18,3,7),
	(@@year,18,3,8),
	(@@year,18,3,9),
	(@@year,18,3,10),
	(@@year,18,3,11),
	(@@year,18,3,12),
	(@@year,18,3,13),
	(@@year,18,3,14),
	(@@year,18,3,15),
	(@@year,18,3,16),
	(@@year,18,3,17),
	(@@year,18,3,18),
	(@@year,18,3,19),
	(@@year,18,3,20),
	(@@year,18,3,21),
	(@@year,19,0,0),
	(@@year,19,1,1),
	(@@year,19,1,2),
	(@@year,19,1,3),
	(@@year,19,1,4),
	(@@year,19,1,5),
	(@@year,19,1,6),
	(@@year,19,1,7),
	(@@year,19,1,8),
	(@@year,19,1,9),
	(@@year,19,1,10),
	(@@year,19,1,11),
	(@@year,19,1,12),
	(@@year,19,1,13),
	(@@year,19,1,14),
	(@@year,19,1,15),
	(@@year,19,1,16),
	(@@year,19,1,17),
	(@@year,19,1,18),
	(@@year,19,1,19),
	(@@year,19,1,20),
	(@@year,19,1,21),
	(@@year,20,0,0),
	(@@year,20,1,1),
	(@@year,20,1,2),
	(@@year,20,1,3),
	(@@year,20,1,4),
	(@@year,20,1,5),
	(@@year,20,1,6),
	(@@year,20,1,7),
	(@@year,20,1,8),
	(@@year,20,1,9),
	(@@year,20,1,10),
	(@@year,20,1,11),
	(@@year,20,1,12),
	(@@year,20,1,13),
	(@@year,20,1,14),
	(@@year,20,1,15),
	(@@year,20,1,16),
	(@@year,20,1,17),
	(@@year,20,1,18),
	(@@year,20,1,19),
	(@@year,20,1,20),
	(@@year,20,1,21),
	(@@year,21,0,0),
	(@@year,21,1,1),
	(@@year,21,1,2),
	(@@year,21,1,3),
	(@@year,21,1,4),
	(@@year,21,1,5),
	(@@year,21,1,6),
	(@@year,21,1,7),
	(@@year,21,1,8),
	(@@year,21,1,9),
	(@@year,21,1,10),
	(@@year,21,1,11),
	(@@year,21,1,12),
	(@@year,21,1,13),
	(@@year,21,1,14),
	(@@year,21,1,15),
	(@@year,21,1,16),
	(@@year,21,1,17),
	(@@year,21,1,18),
	(@@year,21,1,19),
	(@@year,21,1,20),
	(@@year,21,1,21),
	(@@year,21,2,1),
	(@@year,21,2,2),
	(@@year,21,2,3),
	(@@year,21,2,4),
	(@@year,21,2,5),
	(@@year,21,2,6),
	(@@year,21,2,7),
	(@@year,21,2,8),
	(@@year,21,2,9),
	(@@year,21,2,10),
	(@@year,21,2,11),
	(@@year,21,2,12),
	(@@year,21,2,13),
	(@@year,21,2,14),
	(@@year,21,2,15),
	(@@year,21,2,16),
	(@@year,21,2,17),
	(@@year,21,2,18),
	(@@year,21,2,19),
	(@@year,21,2,20),
	(@@year,21,2,21),
	(@@year,21,3,1),
	(@@year,21,3,2),
	(@@year,21,3,3),
	(@@year,21,3,4),
	(@@year,21,3,5),
	(@@year,21,3,6),
	(@@year,21,3,7),
	(@@year,21,3,8),
	(@@year,21,3,9),
	(@@year,21,3,10),
	(@@year,21,3,11),
	(@@year,21,3,12),
	(@@year,21,3,13),
	(@@year,21,3,14),
	(@@year,21,3,15),
	(@@year,21,3,16),
	(@@year,21,3,17),
	(@@year,21,3,18),
	(@@year,21,3,19),
	(@@year,21,3,20),
	(@@year,21,3,21),
	(@@year,22,0,0),
	(@@year,22,1,1),
	(@@year,22,1,2),
	(@@year,22,1,3),
	(@@year,22,1,4),
	(@@year,22,1,5),
	(@@year,22,1,6),
	(@@year,22,1,7),
	(@@year,22,1,8),
	(@@year,22,1,9),
	(@@year,22,1,10),
	(@@year,22,1,11),
	(@@year,22,1,12),
	(@@year,22,1,13),
	(@@year,22,1,14),
	(@@year,22,1,15),
	(@@year,22,1,16),
	(@@year,22,1,17),
	(@@year,22,1,18),
	(@@year,22,1,19),
	(@@year,22,1,20),
	(@@year,22,1,21),
	(@@year,22,2,1),
	(@@year,22,2,2),
	(@@year,22,2,3),
	(@@year,22,2,4),
	(@@year,22,2,5),
	(@@year,22,2,6),
	(@@year,22,2,7),
	(@@year,22,2,8),
	(@@year,22,2,9),
	(@@year,22,2,10),
	(@@year,22,2,11),
	(@@year,22,2,12),
	(@@year,22,2,13),
	(@@year,22,2,14),
	(@@year,22,2,15),
	(@@year,22,2,16),
	(@@year,22,2,17),
	(@@year,22,2,18),
	(@@year,22,2,19),
	(@@year,22,2,20),
	(@@year,22,2,21),
	(@@year,22,3,1),
	(@@year,22,3,2),
	(@@year,22,3,3),
	(@@year,22,3,4),
	(@@year,22,3,5),
	(@@year,22,3,6),
	(@@year,22,3,7),
	(@@year,22,3,8),
	(@@year,22,3,9),
	(@@year,22,3,10),
	(@@year,22,3,11),
	(@@year,22,3,12),
	(@@year,22,3,13),
	(@@year,22,3,14),
	(@@year,22,3,15),
	(@@year,22,3,16),
	(@@year,22,3,17),
	(@@year,22,3,18),
	(@@year,22,3,19),
	(@@year,22,3,20),
	(@@year,22,3,21),
	(@@year,23,0,0),
	(@@year,23,2,1),
	(@@year,23,2,2),
	(@@year,23,2,3),
	(@@year,23,2,4),
	(@@year,23,2,5),
	(@@year,23,2,6),
	(@@year,23,2,7),
	(@@year,23,2,8),
	(@@year,23,2,9),
	(@@year,23,2,10),
	(@@year,23,2,11),
	(@@year,23,2,12),
	(@@year,23,2,13),
	(@@year,23,2,14),
	(@@year,23,2,15),
	(@@year,23,2,16),
	(@@year,23,2,17),
	(@@year,23,2,18),
	(@@year,23,2,19),
	(@@year,23,2,20),
	(@@year,23,2,21),
	(@@year,24,0,0),
	(@@year,24,2,1),
	(@@year,24,2,2),
	(@@year,24,2,3),
	(@@year,24,2,4),
	(@@year,24,2,5),
	(@@year,24,2,6),
	(@@year,24,2,7),
	(@@year,24,2,8),
	(@@year,24,2,9),
	(@@year,24,2,10),
	(@@year,24,2,11),
	(@@year,24,2,12),
	(@@year,24,2,13),
	(@@year,24,2,14),
	(@@year,24,2,15),
	(@@year,24,2,16),
	(@@year,24,2,17),
	(@@year,24,2,18),
	(@@year,24,2,19),
	(@@year,24,2,20),
	(@@year,24,2,21),
	(@@year,25,0,0),
	(@@year,25,2,1),
	(@@year,25,2,2),
	(@@year,25,2,3),
	(@@year,25,2,4),
	(@@year,25,2,5),
	(@@year,25,2,6),
	(@@year,25,2,7),
	(@@year,25,2,8),
	(@@year,25,2,9),
	(@@year,25,2,10),
	(@@year,25,2,11),
	(@@year,25,2,12),
	(@@year,25,2,13),
	(@@year,25,2,14),
	(@@year,25,2,15),
	(@@year,25,2,16),
	(@@year,25,2,17),
	(@@year,25,2,18),
	(@@year,25,2,19),
	(@@year,25,2,20),
	(@@year,25,2,21),
	(@@year,26,0,0),
	(@@year,26,2,1),
	(@@year,26,2,2),
	(@@year,26,2,3),
	(@@year,26,2,4),
	(@@year,26,2,5),
	(@@year,26,2,6),
	(@@year,26,2,7),
	(@@year,26,2,8),
	(@@year,26,2,9),
	(@@year,26,2,10),
	(@@year,26,2,11),
	(@@year,26,2,12),
	(@@year,26,2,13),
	(@@year,26,2,14),
	(@@year,26,2,15),
	(@@year,26,2,16),
	(@@year,26,2,17),
	(@@year,26,2,18),
	(@@year,26,2,19),
	(@@year,26,2,20),
	(@@year,26,2,21),
	(@@year,27,0,0),
	(@@year,27,2,1),
	(@@year,27,2,2),
	(@@year,27,2,3),
	(@@year,27,2,4),
	(@@year,27,2,5),
	(@@year,27,2,6),
	(@@year,27,2,7),
	(@@year,27,2,8),
	(@@year,27,2,9),
	(@@year,27,2,10),
	(@@year,27,2,11),
	(@@year,27,2,12),
	(@@year,27,2,13),
	(@@year,27,2,14),
	(@@year,27,2,15),
	(@@year,27,2,16),
	(@@year,27,2,17),
	(@@year,27,2,18),
	(@@year,27,2,19),
	(@@year,27,2,20),
	(@@year,27,2,21),
	(@@year,28,0,0),
	(@@year,28,4,1),
	(@@year,28,4,2),
	(@@year,28,4,3),
	(@@year,28,4,4),
	(@@year,28,4,5),
	(@@year,28,4,6),
	(@@year,28,4,7),
	(@@year,28,4,8),
	(@@year,28,4,9),
	(@@year,28,4,10),
	(@@year,28,4,11),
	(@@year,28,4,12),
	(@@year,28,4,13),
	(@@year,28,4,14),
	(@@year,28,4,15),
	(@@year,28,4,16),
	(@@year,28,4,17),
	(@@year,28,4,18),
	(@@year,28,4,19),
	(@@year,28,4,20),
	(@@year,28,4,21),
	(@@year,29,0,0),
	(@@year,29,4,1),
	(@@year,29,4,2),
	(@@year,29,4,3),
	(@@year,29,4,4),
	(@@year,29,4,5),
	(@@year,29,4,6),
	(@@year,29,4,7),
	(@@year,29,4,8),
	(@@year,29,4,9),
	(@@year,29,4,10),
	(@@year,29,4,11),
	(@@year,29,4,12),
	(@@year,29,4,13),
	(@@year,29,4,14),
	(@@year,29,4,15),
	(@@year,29,4,16),
	(@@year,29,4,17),
	(@@year,29,4,18),
	(@@year,29,4,19),
	(@@year,29,4,20),
	(@@year,29,4,21),
	(@@year,30,0,0),
	(@@year,30,4,1),
	(@@year,30,4,2),
	(@@year,30,4,3),
	(@@year,30,4,4),
	(@@year,30,4,5),
	(@@year,30,4,6),
	(@@year,30,4,7),
	(@@year,30,4,8),
	(@@year,30,4,9),
	(@@year,30,4,10),
	(@@year,30,4,11),
	(@@year,30,4,12),
	(@@year,30,4,13),
	(@@year,30,4,14),
	(@@year,30,4,15),
	(@@year,30,4,16),
	(@@year,30,4,17),
	(@@year,30,4,18),
	(@@year,30,4,19),
	(@@year,30,4,20),
	(@@year,30,4,21),
	(@@year,31,0,0),
	(@@year,32,0,0),
	(@@year,33,0,0),
	(@@year,34,0,0),
	(@@year,35,0,0),
	(@@year,35,7,22),
	(@@year,35,7,23),
	(@@year,35,7,24),
	(@@year,35,7,25),
	(@@year,35,7,26),
	(@@year,36,0,0),
	(@@year,36,1,1),
	(@@year,36,1,2),
	(@@year,36,1,3),
	(@@year,36,1,4),
	(@@year,36,1,5),
	(@@year,36,1,6),
	(@@year,36,1,7),
	(@@year,36,1,8),
	(@@year,36,1,9),
	(@@year,36,1,10),
	(@@year,36,1,11),
	(@@year,36,1,12),
	(@@year,36,1,13),
	(@@year,36,1,14),
	(@@year,36,1,15),
	(@@year,36,1,16),
	(@@year,36,1,17),
	(@@year,36,1,18),
	(@@year,36,1,19),
	(@@year,36,1,20),
	(@@year,36,1,21),
	(@@year,37,0,0),
	(@@year,37,1,1),
	(@@year,37,1,2),
	(@@year,37,1,3),
	(@@year,37,1,4),
	(@@year,37,1,5),
	(@@year,37,1,6),
	(@@year,37,1,7),
	(@@year,37,1,8),
	(@@year,37,1,9),
	(@@year,37,1,10),
	(@@year,37,1,11),
	(@@year,37,1,12),
	(@@year,37,1,13),
	(@@year,37,1,14),
	(@@year,37,1,15),
	(@@year,37,1,16),
	(@@year,37,1,17),
	(@@year,37,1,18),
	(@@year,37,1,19),
	(@@year,37,1,20),
	(@@year,37,1,21),
	(@@year,37,2,1),
	(@@year,37,2,2),
	(@@year,37,2,3),
	(@@year,37,2,4),
	(@@year,37,2,5),
	(@@year,37,2,6),
	(@@year,37,2,7),
	(@@year,37,2,8),
	(@@year,37,2,9),
	(@@year,37,2,10),
	(@@year,37,2,11),
	(@@year,37,2,12),
	(@@year,37,2,13),
	(@@year,37,2,14),
	(@@year,37,2,15),
	(@@year,37,2,16),
	(@@year,37,2,17),
	(@@year,37,2,18),
	(@@year,37,2,19),
	(@@year,37,2,20),
	(@@year,37,2,21),
	(@@year,37,3,1),
	(@@year,37,3,2),
	(@@year,37,3,3),
	(@@year,37,3,4),
	(@@year,37,3,5),
	(@@year,37,3,6),
	(@@year,37,3,7),
	(@@year,37,3,8),
	(@@year,37,3,9),
	(@@year,37,3,10),
	(@@year,37,3,11),
	(@@year,37,3,12),
	(@@year,37,3,13),
	(@@year,37,3,14),
	(@@year,37,3,15),
	(@@year,37,3,16),
	(@@year,37,3,17),
	(@@year,37,3,18),
	(@@year,37,3,19),
	(@@year,37,3,20),
	(@@year,37,3,21),
	(@@year,38,0,0),
	(@@year,38,4,1),
	(@@year,38,4,2),
	(@@year,38,4,3),
	(@@year,38,4,4),
	(@@year,38,4,5),
	(@@year,38,4,6),
	(@@year,38,4,7),
	(@@year,38,4,8),
	(@@year,38,4,9),
	(@@year,38,4,10),
	(@@year,38,4,11),
	(@@year,38,4,12),
	(@@year,38,4,13),
	(@@year,38,4,14),
	(@@year,38,4,15),
	(@@year,38,4,16),
	(@@year,38,4,17),
	(@@year,38,4,18),
	(@@year,38,4,19),
	(@@year,38,4,20),
	(@@year,38,4,21),
	(@@year,39,0,0),
	(@@year,39,2,1),
	(@@year,39,2,2),
	(@@year,39,2,3),
	(@@year,39,2,4),
	(@@year,39,2,5),
	(@@year,39,2,6),
	(@@year,39,2,7),
	(@@year,39,2,8),
	(@@year,39,2,9),
	(@@year,39,2,10),
	(@@year,39,2,11),
	(@@year,39,2,12),
	(@@year,39,2,13),
	(@@year,39,2,14),
	(@@year,39,2,15),
	(@@year,39,2,16),
	(@@year,39,2,17),
	(@@year,39,2,18),
	(@@year,39,2,19),
	(@@year,39,2,20),
	(@@year,39,2,21),
	(@@year,40,0,0),
	(@@year,40,3,1),
	(@@year,40,3,2),
	(@@year,40,3,3),
	(@@year,40,3,4),
	(@@year,40,3,5),
	(@@year,40,3,6),
	(@@year,40,3,7),
	(@@year,40,3,8),
	(@@year,40,3,9),
	(@@year,40,3,10),
	(@@year,40,3,11),
	(@@year,40,3,12),
	(@@year,40,3,13),
	(@@year,40,3,14),
	(@@year,40,3,15),
	(@@year,40,3,16),
	(@@year,40,3,17),
	(@@year,40,3,18),
	(@@year,40,3,19),
	(@@year,40,3,20),
	(@@year,40,3,21)
    ";
    }


}