using System;
using System.Collections.Generic;
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
        public string owners { get; set; }

        [ResultColumn] public string item { get; set; }
        [ResultColumn] public string symbol { get; set; }
        [ResultColumn] public int DecimalPoint { get; set; }
        [ResultColumn] public int Total { get; set; }

        public static List<Line> Card() 
        {
            List<Line> lines = null;
            using (scoreDB s = new scoreDB()) {
                lines = s.Fetch<Line, Score, Group, Line>(new LineOwners().MapIt, string.Format(_outline, 2014));
            }
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
        public Line MapIt(Line ln, Score sc, Group gr)
        {
            if (ln == null)
                return current;

            if (current != null && current.LineId == ln.LineId)
            {
                if (gr != null) { sc.GroupId = gr.GroupId; sc.Group = gr._Group; }
                sc.LineId = ln.LineId;
                sc.Decimal = ln.DecimalPoint;
                current.scores.Add(sc);

                return null;
            }

            // This is a different author to the current one, or this is the 
            // first time through and we don't have an author yet


            // Save the current author
            var p = current;
            if (p!=null)
                p.sub = (p.scores.Count() > 0)? new Score(p.scores): new Score();

            // Setup the new current author
            current = ln;
            current.scores = new List<Score>();
            current.sub = new Score();
            if (gr != null) { sc.GroupId = gr.GroupId; sc.Group = gr._Group; }
            if (sc != null)
            {
                sc.LineId = ln.LineId;
                sc.Decimal = ln.DecimalPoint;
                current.scores.Add(sc);
            }

            // Return the now populated previous author (or null if first time through)
            return p;
        }
    }
}