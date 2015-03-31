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
            var id = Score.SaveQuarter(scoreid, quarter, value);
            Clients.Others.reflectCell(scoreid, quarter, value);
        }

        public void UpdateComment(int scoreid, string comment)
        {
            var id = Score.SaveComment(scoreid, comment);
            Clients.Others.reflectComment(scoreid, comment);
        }
    }
}