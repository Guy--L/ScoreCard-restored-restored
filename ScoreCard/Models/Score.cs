﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoreCard.Models
{
    public partial class Score
    {
        public int Total { get { return Q1.Value + Q2.Value + Q3.Value + Q4.Value; } }

        public Score()
        {
            Q1 = Q2 = Q3 = Q4 = 0;
            Target = 0;
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