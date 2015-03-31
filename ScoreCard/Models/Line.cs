using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using NPoco;

namespace ScoreCard.Models
{
    public class LineView
    {
        public Line ln {get; set;}
        public string owners { get; set; }
        public int? total { get; set; }
    }

    public partial class Line
    {
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

        private static string _outline = @"
            with linelist as (
	            select cast(n.[order] as varchar(10)) as item, n.ParentLineId,
	            1 as [level], n.[description], n.[order], n.LineId, n.MeasureId
	            from line n
	            where n.LineId = n.ParentLineId
	            union all
	            select cast(p.[item] + '.' + CONVERT(varchar(10), n.[order]) as varchar(10)) as item, 
				n.ParentLineId,
	            p.[level]+1 as [level], n.[description], n.[order], n.LineId, n.MeasureId
	            from line n
	            inner join linelist p on p.LineId = n.ParentLineId
	            where n.LineId != n.ParentLineId
            )
            select q.LineId, q.item, m.symbol, m.DecimalPoint, q.[description], s.Comment, s.ScoreId,
			s.[target], s.q1, s.q2, s.q3, s.q4, s.sumscore as [Total], g.[Group], g.groupid
            from linelist q
            join Measure m on m.MeasureId = q.MeasureId
			left join (
            select groupid, [target], q1, q2, q3, q4, q1+q2+q3+q4 as sumscore, comment, scoreid, lineid 
            from score
			where yearending = {0}) s on q.lineid = s.lineid
			left join [group] g on s.groupid = g.groupid
            order by item
        ";

        private static string _linebyid = @"
            select * from line where lineid = {0}
            select * from Worker w
            left join Responsibility r on r.WorkerId = w.WorkerId
            where r.LineId = {0}
        ";

        public List<Score> scores { get; set; }
        public Score sub { get; set; }
        public List<Worker> workers { get; set; }
        public string owntip { get; set; }
        public string owner { get; set; }

        public bool CanEdit { get; set; }

        [ResultColumn] public string item { get; set; }
        [ResultColumn] public string symbol { get; set; }
        [ResultColumn] public int DecimalPoint { get; set; }

        public static List<Line> Card(Worker login) 
        {
            List<Line> lines = null;
            ILookup<int,Worker> owned;
            using (scoreDB s = new scoreDB()) {
                lines = s.Fetch<Line, Score, Group, Line>(new LineOwners().Scores2Line, string.Format(_outline, 2014));
                owned = s.Fetch<Worker>(_lineowners).ToLookup(l => l.LineId);
            }
            lines.ForEach(l => {
                l.workers = owned[l.LineId].ToList();
                if (l.workers.Any())
                {
                    l.owntip = string.Join(" \\ ", l.workers.Select(w => w.FirstName[0] + ". " + w.LastName).ToArray());
                    var q = l.workers.First();
                    l.owner = q.FirstName[0] + ". " + q.LastName + (l.workers.Count>1?("+"+(l.workers.Count-1)):"");
                    Debug.WriteLine("line " + l.LineId + ": " + l.owntip + " " + l.owner);
                }
                var IsOwner = l.workers.Any(w => w.WorkerId == login.WorkerId);
                l.CanEdit = (IsOwner || login.IsAdmin) && (l.scores.Count == 0) && (login.GroupId != 5);
                l.scores.ForEach(s =>
                {
                    s.CanEdit = IsOwner || login.IsAdmin || s.GroupId == login.GroupId;
                });
            });
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
        public Line Scores2Line(Line ln, Score sc, Group gr)
        {
            if (ln == null)
                return current;

            if (current != null && current.LineId == ln.LineId)
            {
                if (gr != null) { sc.GroupId = gr.GroupId; sc.Group = gr._Group; }
                else { sc.GroupId = 0; sc.Group = "All"; }
                sc.LineId = ln.LineId;
                sc.Decimal = ln.DecimalPoint;
                current.scores.Add(sc);

                return null;
            }

            // Save the current author
            var p = current;
            if (p!=null)
                p.sub = (p.scores.Count() > 0)? new Score(p.scores): new Score();

            // Setup the new current author
            current = ln;
            current.scores = new List<Score>();
            current.sub = new Score();
            if (sc != null)
            {
                sc.LineId = ln.LineId;
                sc.Decimal = ln.DecimalPoint;
                if (gr != null) { sc.GroupId = gr.GroupId; sc.Group = gr._Group; }
                else { sc.GroupId = 0; sc.Group = "All"; }
                current.scores.Add(sc);
            }

            // Return the now populated previous author (or null if first time through)
            return p;
        }
    }
}