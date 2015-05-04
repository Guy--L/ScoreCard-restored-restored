using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
                  FROM [dbo].[Worker] w
                    join [Group] g on g.groupid = w.groupid
                    join Site s on s.siteid = w.siteid
        ";

        [ResultColumn] public int LineId { get; set; }
        public List<Permit> permits { get; set; }
        [ResultColumn] public string Group { get; set; }
        [ResultColumn] public string Site { get; set; }

        public Worker()
        { }

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
        public SelectList groups;
        public SelectList sites;
        public SelectList managers;

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

                sites = AddNone(new SelectList(db.Fetch<Site>(""), "SiteId", "_Site", w.FacilityId));
                groups = AddNone(new SelectList(db.Fetch<Group>(""), "GroupId", "_Group", w.GroupId));
                managers = AddNone(new SelectList(db.Fetch<Worker>("where IsManager = 1"), "WorkerId", "LastName", w.ManagerId));
            }
        }

        private SelectList AddNone(SelectList list)
        {
            List<SelectListItem> _list = list.ToList();
            _list.Insert(0, new SelectListItem() { Value = "0", Text = "" });
            return new SelectList((IEnumerable<SelectListItem>)_list, "Value", "Text");
        }
    }

}