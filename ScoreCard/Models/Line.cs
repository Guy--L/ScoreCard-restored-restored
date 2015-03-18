using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPoco;

namespace ScoreCard.Models
{
    public partial class Line
    {
        private static string _outline = @"
            with linelist as (
	            select CONVERT(varchar(10), n.[order]) as item, 
	            1 as linelevel, n.[description], n.[order], n.LineId, n.MeasureId
	            from line n
	            where n.LineId = n.ParentLineId
	            union all
	            select (convert(varchar(4), p.[order]) + '.' + CONVERT(varchar(5), n.[order])) as item, 
	            p.linelevel+1 as linelevel, n.[description], n.[order], n.LineId, n.MeasureId
	            from line n
	            inner join linelist p on p.LineId = n.ParentLineId
	            where n.LineId != n.ParentLineId
            )
            select q.LineId, q.item, q.[description], m.symbol, s.[Target], s.Q1, s.Q2, s.Q3, s.Q4, s.Comment,
             w.OwnerId, w.FirstName, w.LastName
            from linelist q
            left join Measure m on m.MeasureId = q.MeasureId
            left join Score s on s.LineId = q.LineId
            left join Responsibility r on r.LineId = q.LineId
            left join [Owner] w on r.OwnerId = w.OwnerId
            order by item";

        private static string _linebyid = @"
            select * from line where lineid = {0}
            select * from owner w
            left join Responsibility r on r.OwnerId = w.OwnerId
            where r.LineId = {0}
        ";

        public Score year { get; set; }
        public List<Owner> owners { get; set; }
        public string ownerlist
        {
            get
            {
                return string.Join(" / ", owners.Select(o => o.FirstName + ". " + o.LastName));
            }
        }
        [ResultColumn] public string item { get; set; }
        [ResultColumn] public string symbol { get; set; }

        public static List<Line> Card() 
        {
            List<Line> lines = null;
            using (scoreDB s = new scoreDB()) {
                lines = s.Fetch<Line, Score, Owner, Line>(new LineOwners().MapIt, _outline);
            }
            return lines;
        }

        public Line() { }

        public Line(int id)
        {
            Line ln = null;
            using (scoreDB s = new scoreDB())
            {
                ln = s.Fetch<Line, Owner, Line>(_linebyid, id).SingleOrDefault();
            }
            
        }
    }

    class LineOwners
    {
        public Line current;
        public Line MapIt(Line ln, Score sc, Owner on)
        {
            // Terminating call.  Since we can return null from this function
            // we need to be ready for PetaPoco to callback later with null
            // parameters
            if (ln == null)
                return current;

            // Is this the same author as the current one we're processing
            if (current != null && current.LineId == ln.LineId)
            {
                // Yes, just add this post to the current author's collection of posts
                current.owners.Add(on);

                // Return null to indicate we're not done with this author yet
                return null;
            }

            // This is a different author to the current one, or this is the 
            // first time through and we don't have an author yet

            // Save the current author
            var prev = current;

            // Setup the new current author
            current = ln;
            current.owners = new List<Owner>();
            current.owners.Add(on);
            current.year = sc ?? new Score();

            // Return the now populated previous author (or null if first time through)
            return prev;
        }
    }
}