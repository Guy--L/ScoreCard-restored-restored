using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using ScoreCard.Models;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ScoreCard.Hubs
{
    public class DoScore : ICommand<string[]>
    {
        private string[] _queries;

        public DoScore()
        {
            _queries = null;
        }

        public DoScore(int scoreid, int quarter, int? value)
        {
            _queries = Score.SaveScore(scoreid, quarter, value);
        }

        public DoScore(int scoreid, string comment)
        {
            _queries = Score.SaveScore(scoreid, comment);
        }

        public DoScore(int scoreid, int? target)
        {
            _queries = Score.SaveScore(scoreid, target);
        }

        public string[] Do(string[] input)
        {
            if (_queries == null)
            {
                Score.Step(input[1]);
                return input;
            }
            Score.Step(_queries[1]);
            return _queries;
        }

        public string[] Undo(string[] input)
        {
            if (_queries == null)
            {
                Score.Step(input[0]);
                return input;
            }
            Score.Step(_queries[0]);
            return _queries;
        }
    }

    [Authorize]
    public class ScoreHub : Hub
    {
        private static Dictionary<string, URStack<string[]>> stack = new Dictionary<string, URStack<string[]>>();

        public override Task OnConnected()
        {
            var name = Thread.CurrentPrincipal.Identity.Name;
            URStack<string[]> s;
            if (!stack.TryGetValue(name, out s))
            {
                s = stack[name] = new URStack<string[]>();
                Debug.WriteLine("added " + name);
            }
            Groups.Add(Context.ConnectionId, name);            // user may be in on multiple sessions
            Clients.Group(name).undoRedo(s.UndoCount, s.RedoCount);
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            var name = Thread.CurrentPrincipal.Identity.Name;
            URStack<string[]> s;
            if (!stack.TryGetValue(name, out s))
            {
                s = stack[name] = new URStack<string[]>();
                Debug.WriteLine("added " + name);
            }
            Clients.Group(name).undoRedo(s.UndoCount, s.RedoCount);
            return base.OnReconnected();
        }

        public void SetYear(int oldyear, int newyear)
        {
            var cid = Context.ConnectionId;
            if (oldyear != newyear) Groups.Remove(cid, oldyear.ToString());
            Groups.Add(cid, newyear.ToString());
        }

        public void UpdateCell(int year, int scoreid, int quarter, int? value)
        {
            var name = Thread.CurrentPrincipal.Identity.Name;
            var ur = stack[name];
            ur.Do(new DoScore(scoreid, quarter, value), null);
            Clients.OthersInGroup(year.ToString()).reflectCell(scoreid, quarter, value);
            Clients.Group(name).undoRedo(ur.UndoCount, ur.RedoCount);
        }

        public void UpdateTarget(int year, int scoreid, int? value)
        {
            var name = Thread.CurrentPrincipal.Identity.Name;
            var ur = stack[name];
            ur.Do(new DoScore(year, scoreid, value), null);
            Clients.OthersInGroup(year.ToString()).reflectTarget(scoreid, value);
            Clients.Group(name).undoRedo(ur.UndoCount, ur.RedoCount);
        }

        public void UpdateComment(int year, int scoreid, string comment)
        {
            var name = Thread.CurrentPrincipal.Identity.Name;
            var ur = stack[name];
            ur.Do(new DoScore(scoreid, comment), null);
            Clients.OthersInGroup(year.ToString()).reflectComment(scoreid, comment);
            Clients.Group(name).undoRedo(ur.UndoCount, ur.RedoCount);
        }

        public void Undo()
        {
            var name = Thread.CurrentPrincipal.Identity.Name;
            var ur = stack[name];
            ur.Undo(null);
            Clients.Group(name).undoRedo(ur.UndoCount, ur.RedoCount);
        }

        public void Redo()
        {
            var name = Thread.CurrentPrincipal.Identity.Name;
            var ur = stack[name];
            ur.Redo(null);
            Clients.Group(name).undoRedo(ur.UndoCount, ur.RedoCount);
        }
    }
}