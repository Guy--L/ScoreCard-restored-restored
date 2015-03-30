using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using ScoreCard.Models;

namespace ScoreCard.Hubs
{
    public class ScoreHub : Hub
    {
        public void UpdateCell(int scoreid, int quarter, int value)
        {
            Score.UpdateQuarter(scoreid, quarter, value);
            Clients.All.reflectCell(scoreid, quarter, value);
        }

        public void UpdateComment(int scoreid, string comment)
        {
            Score.UpdateComment(scoreid, comment);
            Clients.All.reflectComment(scoreid, comment);
        }
    }
}