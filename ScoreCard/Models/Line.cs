﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using NPoco;

namespace ScoreCard.Models
{
    public class Lines
    {
        public List<Line> list { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class LineView
    {
        public List<Line> list { get; set; }
        public int[] owned { get; set; }
    }

    public partial class Line
    {
        public static string _lines = @"
            select  * from Line
        ";

        private static string _lineowners = @"
            select l.lineid, w.LastName, w.Firstname, w.WorkerId, w.IonName from line l
            join responsibility r on l.LineId = r.LineId
            join worker w on r.WorkerId = w.WorkerId
            order by w.LastName, w.Firstname
        ";

        private static string _linecardByYear = @"
            with linecard as (
                SELECT lineid,groupid,
                SUM([target]) as [target],
                sum(q1) as q1,
                sum(q2) as q2,
                sum(q3) as q3,
                sum(q4) as q4,
                sum(q1+q2+q3+q4) as total
                from score 
                where yearending = {0}
                GROUP BY lineid,groupid with ROLLUP)
            select s.scoreid, l.*, s.comment, g.[group]
            from score s
            join [group] g on g.groupid = s.groupid
            right join linecard l on s.lineid = l.lineid and s.groupid = l.groupid
            where s.yearending = {0} or s.yearending is null and l.lineid is not null
        ";

        // in the outline, item is the field containing the outline "number".  numbers are concatenated with points; whereas characters are not.  witness the like clause.
        private static string _linelist = @";with linelist as (
	            select cast(n.[order] as varchar(10)) as item, n.ParentLineId,
	            1 as [level], n.[description], n.[order], n.LineId, n.MeasureId, n.ShowTotal
	            from line n
	            where n.LineId = n.ParentLineId

	            union all

                select cast(p.[item] + (case when n.[order] not like '%[^0-9]%' then '.' else '' end) + CONVERT(varchar(10), n.[order]) as varchar(10)) as item, 
				n.ParentLineId,
	            p.[level]+1 as [level], n.[description], n.[order], n.LineId, n.MeasureId, n.ShowTotal
	            from line n
	            inner join linelist p on p.LineId = n.ParentLineId
	            where n.LineId != n.ParentLineId
            )";

        private static string _outline = _linelist + @"
            select q.LineId, q.item, m.symbol, m.DecimalPoint, m.VerticalAvg, m.HorizontalAvg, q.[description], q.ShowTotal, s.ScoreId, s.Comment,
            (
                select case 
                    when coalesce(q1,q2,q3,q4,-1) = -1 then null 
                    when m.symbol = 'recent' then coalesce(q4,q3,q2,q1)
                    else isnull(q1,0) + isnull(q2,0) + isnull(q3,0) + isnull(q4,0) 
                end
                from score
			    where yearending = {0}-1 and lineid = q.lineid and groupid = s.groupid and siteid = s.siteid and groupid != 0 and siteid != 0
            ) as [PriorTotal],
			s.[target], s.q1, s.q2, s.q3, s.q4,
            case
                when m.symbol = 'recent' then s.calcscore
                else s.sumscore
            end as [Total], 
            g.[Group], g.groupid, x.Site, x.SiteId
            from linelist q
			left join Hide h on h.LineId = q.LineId and {0} >= coalesce(h.StartYear, 0) and {0} <= coalesce(h.EndYear, 9999)
            join Measure m on m.MeasureId = q.MeasureId
			left join (
                select groupid, [target], q1, q2, q3, q4, 
                    case 
                        when coalesce(q1,q2,q3,q4,-1) = -1 then null 
                        else isnull(q1,0) + isnull(q2,0) + isnull(q3,0) + isnull(q4,0) 
                    end as sumscore, 
                    coalesce(q4,q3,q2,q1) as calcscore,
					comment, scoreid, lineid, siteid 
                from score
			    where yearending = {0}) s 
            on q.lineid = s.lineid
			left join [group] g on s.groupid = g.groupid
            left join site x on s.siteid = x.siteid
            where h.LineId is null
            order by item, groupid
        ";

        public static string _itemlist = _linelist + @"
			select q.LineId, q.item as item, q.[description]
			from linelist q
			join measure m on m.measureid = q.measureid
			where symbol != 'blank'
			order by item;
        ";

        private static string _linebyid = @"
            select * from line where lineid = {0}
            select * from Worker w
            left join Responsibility r on r.WorkerId = w.WorkerId
            where r.LineId = {0}
        ";

        public int? topPriorTotal { get { return this.top(x => x.PriorTotal); } }
        public int? topTarget { get { return this.top(x => x.Target); } }
        public int? topQ1 { get { return this.top(x => x.Q1 ); } }
        public int? topQ2 { get { return this.top(x => x.Q2 ); } }
        public int? topQ3 { get { return this.top(x => x.Q3 ); } }
        public int? topQ4 { get { return this.top(x => x.Q4 ); } }
        public int? topTotal
        {
            get
            {
                var data = topQ1.HasValue || topQ2.HasValue || topQ3.HasValue || topQ4.HasValue;
                if (!data) return null;
                int? sum =
                    (topQ1.HasValue ? topQ1.Value : 0) +
                    (topQ2.HasValue ? topQ2.Value : 0) +
                    (topQ3.HasValue ? topQ3.Value : 0) +
                    (topQ4.HasValue ? topQ4.Value : 0);

                if (symbol == "recent")
                    sum = topQ4 ?? topQ3 ?? topQ2 ?? topQ1;

                if (topdown != null && topdown.havg)
                {
                    var n = (topQ1.HasValue ? 1 : 0) + (topQ2.HasValue ? 1 : 0) + (topQ3.HasValue ? 1 : 0) + (topQ4.HasValue ? 1 : 0);
                    sum /= n == 0 ? 1 : n;
                }
                return sum;
            }
        }

        public List<Score> scores { get; set; }
        public Score topdown { get; set; }
        public Score bottomup { get; set; }
        public List<Worker> workers { get; set; }
        public string owntip { get; set; } 
        public string linetip { get; set; }
        public string owner { get; set; }
        public string ItemDesc { get { return item + " " + Description; } }

        public bool CanEdit { get; set; }

        public static bool NoScores { get; set; }

        [ResultColumn] public string item { get; set; }
        [ResultColumn] public string symbol { get; set; }
        [ResultColumn] public bool horizontalavg { get; set; }
        [ResultColumn] public bool verticalavg { get; set; }
        [ResultColumn] public int DecimalPoint { get; set; }

        public static List<Line> Card(int year, Worker login) 
        {
            NoScores = true;
            List<Line> lines = null;
            ILookup<int,Worker> owned;
            using (scoreDB s = new scoreDB()) {
                lines = s.Fetch<Line, Score, Group, Site, Line>(new LineOwners().Scores2Line, string.Format(_outline, year));
                owned = s.Fetch<Worker>(_lineowners).ToLookup(l => l.LineId);
            }
            lines.ForEach(l => {
                if (l.Description.Contains("["))
                {
                    var left = l.Description.IndexOf('[');
                    var right = l.Description.LastIndexOf(']');
                    l.linetip = l.Description.Substring(left + 1, right - left - 2);
                    l.Description = l.Description.Substring(0, left) + l.Description.Substring(right, l.Description.Length - right - 1);
                }
                else l.linetip = "";

                l.workers = owned[l.LineId].ToList();
                if (l.workers.Any())
                {
                    l.owntip = string.Join(" \\ ", l.workers.Select(w => w.FirstName[0] + ". " + w.LastName).ToArray());
                    var q = l.workers.First();
                    l.owner = q.FirstName[0] + ". " + q.LastName + (l.workers.Count>1?("+"+(l.workers.Count-1)):"");
                }
                var IsOwner = l.workers.Any(w => w.WorkerId == login.WorkerId);
                l.CanEdit = (IsOwner || login.IsAdmin) && (login.GroupId != 5) && (l.symbol != "%>=8");     // && (l.scores.Count == 0) 

                var groups = l.scores.ToLookup(s => s.GroupId.Value);               // partition site scores by group
                var groupScores = new List<Score>();                                // create group subtotals
                
                foreach (var g in groups)                                           // skip over the All site
                {
                    var list = groups[g.Key].ToList();                              // get site scores in group
                    var entries = list.Count(s => s.count(false) > 0);
                    var gScore = new Score(list);                                   // create new score for group subtotal
                    
                    gScore.LineId = l.LineId;                           
                    gScore.avg = l.symbol == "%" || l.symbol == "sum";
                    gScore.avgq = l.symbol == "%";
                    
                    gScore.havg = l.horizontalavg;
                    gScore.vavg = l.verticalavg;

                    gScore.t8 = l.symbol == "%>=8";
                    gScore.recent = l.symbol == "recent";
                    gScore.GroupId = g.Key;                                         // group subtotal has same groupid...
                    gScore.Group = g.First().Group;
                    gScore.Site = "All";                                            // its for all sites, a subtotal
                    gScore.SiteId = 0;
                    gScore.CanEdit = false;

                    var stat = gScore.t8 ? "scored" : (gScore.vavg ? "averaged" : "summed");
                    if (entries>0 && gScore.Group != "Shares")
                        gScore.Comment = " " + stat + " over " + entries + " site" + (entries == 1 ? "" : "s");
                    gScore.scores = list;                                           // have children sites be in subtotal 
                    groupScores.Add(gScore);
                }
                if (l.bottomup != null)
                    l.bottomup.Comment = " line differs from detail";
                l.scores = groupScores;                                             // have subtotals be in total for line
                l.scores.ForEach(s =>
                {
                    if (s.scores != null) s.scores.ForEach(t =>
                    {
                        t.CanEdit = IsOwner || login.IsAdmin || t.GroupId == login.GroupId || login.permits.Any(p => p.SiteId == t.SiteId);
                    });
                });
            });
            var scoreTemplatesMissing = lines.Where(l => l.topdown == null && l.symbol != "blank").Select(n => n.LineId);
            if (scoreTemplatesMissing.Any()) 
                throw new Exception("Contact the programmer:  missing score records for line(s): " + string.Join(", ", scoreTemplatesMissing));
            return lines;
        }

        public Line() { }

        public Line(int id)
        {
            Line ln = null;
            using (scoreDB s = new scoreDB())
            {
                ln = s.Fetch<Line, Worker, Line>(_linebyid, id).SingleOrDefault();
            }
        }
    }

    class LineOwners
    {
        public Line current;
        public Line Scores2Line(Line ln, Score sc, Group gr, Site st)
        {
            Line.NoScores &= (sc == null);
            Score s = null;
            if (ln != null && current != null && current.LineId == ln.LineId)
            {
                s = sc.link(ln, gr, st);                        // attach score to line, groups and sites to score
                if (s != null) current.scores.Add(s);
                return null;                                    // new score only
            }
            var p = current;                                    // seal up previous line
            if (p != null && p.scores.Count() > 0)              // if there are scores by site
                p.bottomup = new Score(p.scores);               // create subtotal line

            if (ln == null) return current;                     // last line

            current = ln;                                       // next line
            current.scores = new List<Score>();
            
            if (sc == null) return p;                           // title line
            s = sc.link(ln, gr, st);
            if (s != null) current.scores.Add(s);
            return p;
        }
    }
}