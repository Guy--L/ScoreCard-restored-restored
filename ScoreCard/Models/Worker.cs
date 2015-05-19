using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using NPoco;

namespace ScoreCard.Models
{
    public class Workers
    {
        public bool IsAdmin { get; set; }
        public List<Worker> list { get; set; }
    }

    public partial class Worker
    {
        private static string beginswith = @" where ionname like @0";
        private static string who  = @" where ionname = @0";
        private static string _permit = @" where workerid = @0";

        public static string _workers = @" 
                SELECT 
                               w.[WorkerId]
                              ,w.[GroupId]
                              ,w.[FirstName]
                              ,w.[LastName]
                              ,w.[IsActive]
                              ,w.[IonName]
                              ,w.[IsAdmin]
                              ,w.[SiteId]
                              ,g.[Group]
                              ,s.[Site]
                              ,(select count(responsibilityid) from responsibility where workerid = w.[WorkerId]) as Pages
                              ,(select count(permitid) from [permit] where workerid = w.[WorkerId]) as Sites
                  FROM [dbo].[Worker] w
                    join [Group] g on g.groupid = w.groupid
                    join Site s on s.siteid = w.siteid
        ";

        [ResultColumn] public int LineId { get; set; }
        public List<Permit> permits { get; set; }
        [ResultColumn] public string Group { get; set; }
        [ResultColumn] public string Site { get; set; }
        [ResultColumn] public int Pages { get; set; }
        [ResultColumn] public int Sites { get; set; }

        public Worker()
        { }

        public static List<Worker> Candidates(string start)
        {
            List<Worker> match = null;
            using (scoreDB s = new scoreDB())
            {
                match = s.Fetch<Worker>(beginswith, start+'%');
            }
            return match;
        }

        public Worker(string ion)
        {
            Worker w;
            using (scoreDB s = new scoreDB())
            {
                w = s.FirstOrDefault<Worker>(who, ion);
                if (w==null)
                    return;
                FirstName = w.FirstName;
                LastName = w.LastName;
                IonName = w.IonName;
                IsActive = w.IsActive;
                IsAdmin = w.IsAdmin;
                IsPartTime = w.IsPartTime;
                OnDisability = w.OnDisability;
                GroupId = w.GroupId;
                WorkerId = w.WorkerId;
                ManagerId = w.ManagerId;
                IsManager = w.IsManager;
                EmployeeNumber = w.EmployeeNumber;
                FacilityId = w.FacilityId;
                SiteId = w.SiteId;               
                    
                permits = s.Fetch<Permit>(_permit, WorkerId);
            }
        }
    }

    public class WorkerView 
    {
        private static string _responsibility = @"
            merge responsibility with (holdlock) as t
            using line as s
            on t.workerid = {0} and s.lineid = t.lineid and s.lineid in ({1})
            when not matched by target and s.lineid in ({1}) then
                insert (lineid, workerid) values (s.lineid, {0})
            when not matched by source and t.workerid = {0} then
                delete;
        ";

        private static string _permitted = @"
            merge permit with (holdlock) as t
            using (values {0}) s (groupid, siteid)
            on t.groupid = s.groupid and t.siteid = s.siteid and t.workerid = {1}
            when not matched by target then
                insert (groupid, siteid, workerid) values (s.groupid, s.siteid, {1})
            when not matched by source and t.workerid = {1} then
                delete;
        ";

        public Worker w { get; set; }
        public List<Group> groups;
        public List<Site> sites;
        public List<Permit> permits;
        public List<Line> lines;
        public List<Responsibility> owned;

        public bool Cancel { get; set; }
        public int[] permitids { get; set; }
        public int[] lineids { get; set; }
        public ILookup<int, int> rights { get; set; }

        public Dictionary<int, string> groupList;
        public IEnumerable<SelectListItem> siteList;
        public IEnumerable<SelectListItem> lineList;

        public string groupids { get; set; }
        public int[] siteids;

        public SelectList managers;
        public SelectList permitList;

        public string jRights { get; set; }
        public string lineRights { get; set; }

        private string badger(int gid)
        {
            return permits.Exists(p => p.GroupId == gid) ? 
                (rights[gid].Contains(0)?"All":rights[gid].Count().ToString()) : "0";
        }

        public WorkerView()
        { }

        public WorkerView(int id)
        {
            using (scoreDB db = new scoreDB())
            {
                if (id > 0)
                    w = db.SingleOrDefault<Worker>("where WorkerId = @0", id);
                else
                {
                    w = new Worker()
                    {
                        GroupId = 0,
                        SiteId = 0
                    };
                }
                sites = db.Fetch<Site>("");
                siteList = sites.Select(x => new SelectListItem
                {
                    Value = x.SiteId.ToString(),
                    Text = x._Site
                });

                groups = db.Fetch<Group>("");
                groupids = new JavaScriptSerializer().Serialize(groups.Select(g => g.GroupId).ToArray());

                permits = db.Fetch<Permit>(" where workerid = @0", id);
                rights = permits.ToLookup(p => p.GroupId, p => p.SiteId);
                groupList = groups.ToDictionary(g => g.GroupId, g => badger(g.GroupId));

                lines = db.Fetch<Line>(Line._itemlist);
                lineList = lines.Select(y => new SelectListItem
                {
                    Value = y.LineId.ToString(),
                    Text = y.ItemDesc
                });

                owned = db.Fetch<Responsibility>(" where workerid = @0", id);

                lineRights = "[" + string.Join(",", owned.Select(t => t.LineId).ToArray()) + "]";
                jRights = "[" + string.Join(",", groups.Select(r => "[" + string.Join(",", rights[r.GroupId].ToArray()) + "]").ToArray()) + "]";
            }
            Cancel = false;
        }

        public void Save()
        {
            var admin = w.IsAdmin;
            w = new Worker(w.IonName);

            if (admin != w.IsAdmin)
            {
                w.IsAdmin = admin;
                w.Save();
            }

            var rights = new JavaScriptSerializer().Deserialize<int[][]>(jRights);
            var groups = new JavaScriptSerializer().Deserialize<int[]>(groupids);
            var localrights = string.Join(",", groups.Select((g, i) => rights[i].Length > 0 ? string.Join(",", rights[i].Select(s => "(" + g.ToString() + "," + s.ToString() + ")").ToArray()) : null).Where(t=>t != null).ToArray());
            using (scoreDB s = new scoreDB())
            {
                var merge = string.Format(_responsibility, w.WorkerId, string.Join(",", lineids)) + string.Format(_permitted, localrights, w.WorkerId);
                s.Execute(merge);
            }
        }

        private SelectList AddNone(SelectList list)
        {
            List<SelectListItem> _list = list.ToList();
            _list.Insert(0, new SelectListItem() { Value = "0", Text = "All" });
            return new SelectList((IEnumerable<SelectListItem>)_list, "Value", "Text");
        }
    }

}