﻿using System;
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
        public Worker w { get; set; }
        public List<Group> groups;
        public List<Site> sites;
        public List<Permit> permits;
        public List<Line> lines;

        public bool Cancel { get; set; }
        public int[] permitids { get; set; }
        public int[] lineids { get; set; }
        public ILookup<int, int> rights { get; set; }

        public Dictionary<int, int> groupList;
        public IEnumerable<SelectListItem> siteList;

        public int[] groupids;
        public int[] siteids;

        public SelectList managers;
        public SelectList permitList;
        public SelectList lineList;

        public string jRights { get; set; }

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

                permits = db.Fetch<Permit>(" where workerid = @0", id);
                rights = permits.ToLookup(p => p.GroupId, p => p.SiteId);
                groupList = groups.ToDictionary(g => g.GroupId, g => permits.Exists(p=>p.GroupId==g.GroupId)?rights[g.GroupId].Count():0);

                lines = db.Fetch<Line>(Line._itemlist);

                jRights = "[" + string.Join(",", groups.Select(r => "[" + string.Join(",", rights[r.GroupId].ToArray()) + "]").ToArray()) + "]";
            }
            Cancel = false;
        }

        private SelectList AddNone(SelectList list)
        {
            List<SelectListItem> _list = list.ToList();
            _list.Insert(0, new SelectListItem() { Value = "0", Text = "All" });
            return new SelectList((IEnumerable<SelectListItem>)_list, "Value", "Text");
        }
    }

}