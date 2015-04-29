using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPoco;

namespace ScoreCard.Models
{
    public partial class Worker
    {
        private static string who  = @" where ionname = @0";
        private static string _permit = @" where workerid = @0";
        [ResultColumn] public int LineId { get; set; }
        public List<Permit> permits { get; set; }

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
}