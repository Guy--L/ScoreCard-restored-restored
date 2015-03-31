using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPoco;

namespace ScoreCard.Models
{
    public partial class Worker
    {
        [ResultColumn] public int LineId { get; set; }
        [ResultColumn] public string Group { get; set; }
    }
}