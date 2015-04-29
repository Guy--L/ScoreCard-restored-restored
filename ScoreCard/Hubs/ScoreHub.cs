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
        public void SetYear(int oldyear, int newyear)
        {
            if (oldyear != newyear) Groups.Remove(Context.ConnectionId, oldyear.ToString());
            Groups.Add(Context.ConnectionId, newyear.ToString());
        }

        public void UpdateCell(int year, int scoreid, int quarter, int value)
        {
            var id = Score.SaveQuarter(year, scoreid, quarter, value);
            Clients.OthersInGroup(year.ToString()).reflectCell(scoreid, quarter, value);
        }
        
        public void UpdateComment(int year, int scoreid, string comment)
        {
            var id = Score.SaveComment(year, scoreid, comment);
            Clients.OthersInGroup(year.ToString()).reflectComment(scoreid, comment);
        }
    }
}