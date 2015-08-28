using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using ScoreCard.Models;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;

namespace ScoreCard.Hubs
{
    public class Target : ICommand
    {
        private int _scoreid;
        private int? _value;
        private int? _old;
        private object _update;

        public Target(int scoreid, int? value)
        {
            _scoreid = scoreid;
            _value = value;
            Do();
        }

        public string reflect { get { return "reflectTarget"; } }
        public object[] parameters { get { return new object[] { _scoreid, _update }; } }

        public void Do()
        {
            _update = _value;
            _old = Score.SaveScore(_scoreid, _value);
        }

        public void Undo()
        {
            _update = _old;
            var check = Score.SaveScore(_scoreid, _old);
            // if (check != _value)   interference
        }
    }

    public class Cell : ICommand
    {
        private int _scoreid;
        private int _quarter;
        private int? _value;
        private int? _old;
        private object _update;

        public Cell(int s, int q, int? v)
        {
            _scoreid = s;
            _quarter = q;
            _value = v;
            Do();
        }
        public string reflect { get { return "reflectCell"; } }
        public object[] parameters { get { return new object[] { _scoreid, _quarter, _update }; } }

        public void Do()
        {
            _update = _value;
            _old = Score.SaveScore(_scoreid, _quarter, _value);
        }

        public void Undo()
        {
            _update = _old;
            var check = Score.SaveScore(_scoreid, _quarter, _old);
        }
    }

    public class Comment : ICommand
    {
        private int _scoreid;
        private string _comment;
        private string _old;
        private object _update;

        public Comment(int scoreid, string value)
        {
            _scoreid = scoreid;
            _comment = value;
            Do();
        }

        public string reflect { get { return "reflectComment"; } }
        public object[] parameters { get { return new object[] { _scoreid, _update }; } }

        public void Do()
        {
            _update = _comment;
            _old = Score.SaveScore(_scoreid, _comment);
        }

        public void Undo()
        {
            _update = _old;
            var check = Score.SaveScore(_scoreid, _old);
            // if (check != _value)   interference
        }
    }

    [Authorize]
    public class ScoreHub : Hub
    {
        private static Dictionary<string, URStack> stack = new Dictionary<string, URStack>();

        public override Task OnConnected()
        {
            var name = Thread.CurrentPrincipal.Identity.Name;
            URStack s;
            if (!stack.TryGetValue(name, out s))
            {
                s = stack[name] = new URStack();
            }
            Groups.Add(Context.ConnectionId, name);            // user may be in on multiple sessions
            Clients.Group(name).undoRedo(s.UndoCount, s.RedoCount);
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            var name = Thread.CurrentPrincipal.Identity.Name;
            URStack s;
            if (!stack.TryGetValue(name, out s))
            {
                s = stack[name] = new URStack();
            }
            Clients.Group(name).undoRedo(s.UndoCount, s.RedoCount);
            return base.OnReconnected();
        }

        public void SetYear(int newyear)
        {
            var oldyear = Clients.Caller.year;
            var cid = Context.ConnectionId;
            if (oldyear != null && oldyear != newyear) Groups.Remove(cid, oldyear.ToString());
            Groups.Add(cid, newyear.ToString());
        }

        public void UpdateCell(int scoreid, int quarter, int? value)
        {
            var name = Thread.CurrentPrincipal.Identity.Name;
            var ur = stack[name];

            ur.Do(new Cell(scoreid, quarter, value));
            Clients.OthersInGroup(Clients.Caller.year.ToString()).reflectCell(scoreid, quarter, value);

            Clients.Group(name).undoRedo(ur.UndoCount, ur.RedoCount);
        }

        public void UpdateTarget(int scoreid, int? value)
        {
            var name = Thread.CurrentPrincipal.Identity.Name;
            var ur = stack[name];

            ur.Do(new Target(scoreid, value));
            Clients.OthersInGroup(Clients.Caller.year.ToString()).reflectTarget(scoreid, value);

            Clients.Group(name).undoRedo(ur.UndoCount, ur.RedoCount);
        }

        public void UpdateComment(int scoreid, string comment)
        {
            var name = Thread.CurrentPrincipal.Identity.Name;
            var ur = stack[name];

            ur.Do(new Comment(scoreid, comment));
            Clients.OthersInGroup(Clients.Caller.year.ToString()).reflectComment(scoreid, comment);

            Clients.Group(name).undoRedo(ur.UndoCount, ur.RedoCount);
        }

        public void Undo()
        {
            var name = Thread.CurrentPrincipal.Identity.Name;
            var ur = stack[name];
            ICommand c = ur.Undo();
            if (c == null) return;

            string year = Clients.Caller.year.ToString();
            Clients.Group(year).Invoke(c.reflect, c.parameters);
            Clients.Group(name).undoRedo(ur.UndoCount, ur.RedoCount);
        }

        public void Redo()
        {
            var name = Thread.CurrentPrincipal.Identity.Name;
            var ur = stack[name];
            ICommand c = ur.Redo();
            if (c == null) return;

            string year = Clients.Caller.year.ToString();
            Clients.Group(year).Invoke(c.reflect, c.parameters);
            Clients.Group(name).undoRedo(ur.UndoCount, ur.RedoCount);
        }
    }
}