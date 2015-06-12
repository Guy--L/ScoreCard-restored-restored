
// This file was automatically generated by the PetaPoco T4 Template
// Do not make changes directly to this file - edit the template instead
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: `score`
//     Provider:               `System.Data.SqlClient`
//     Connection String:      `Data Source=GUYLISTER3546;Initial Catalog=ScoreCard;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False`
//     Schema:                 `dbo`
//     Include Views:          `True`

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPoco;

namespace ScoreCard.Models
{
	public partial class scoreDB : Database
	{
		public scoreDB() 
			: base("score")
		{
			CommonConstruct();
		}

		public scoreDB(string connectionStringName) 
			: base(connectionStringName)
		{
			CommonConstruct();
		}
		
		partial void CommonConstruct();
		
		public interface IFactory
		{
			scoreDB GetInstance();
		}
		
		public static IFactory Factory { get; set; }
        public static scoreDB GetInstance()
        {
			if (_instance!=null)
				return _instance;
				
			if (Factory!=null)
				return Factory.GetInstance();
			else
				return new scoreDB();
        }

		[ThreadStatic] static scoreDB _instance;
		
        protected override void OnBeginTransaction()
        {
                if (_instance==null)
                        _instance=this;
        }
                
        protected override void OnCompleteTransaction()
        {
                if (_instance==this)
                        _instance=null;
        }
				        
		public class Record<T> where T:new()
		{
			public static scoreDB repo { get { return scoreDB.GetInstance(); } }
			public bool IsNew() { return repo.IsNew<T>(this); }
			public object Insert() { return repo.Insert(this); }
			public void Save() { repo.Save<T>(this); }
			public int Update() { return repo.Update(this); }
			public int Update(IEnumerable<string> columns) { return repo.Update(this, columns); }
			public static int Update(string sql, params object[] args) { return repo.Update<T>(sql, args); }
			public static int Update(Sql sql) { return repo.Update<T>(sql); }
			public int Delete() { return repo.Delete(this); }
			public static int Delete(string sql, params object[] args) { return repo.Delete<T>(sql, args); }
			public static int Delete(Sql sql) { return repo.Delete<T>(sql); }
			public static int Delete(object primaryKey) { return repo.Delete<T>(primaryKey); }
			public static bool Exists(object primaryKey) { return repo.Exists<T>(primaryKey); }
			public static T SingleOrDefault(object primaryKey) { return repo.SingleOrDefaultById<T>(primaryKey); }
			public static T SingleOrDefault(string sql, params object[] args) { return repo.SingleOrDefault<T>(sql, args); }
			public static T SingleOrDefault(Sql sql) { return repo.SingleOrDefault<T>(sql); }
			public static T FirstOrDefault(string sql, params object[] args) { return repo.FirstOrDefault<T>(sql, args); }
			public static T FirstOrDefault(Sql sql) { return repo.FirstOrDefault<T>(sql); }
			public static T Single(object primaryKey) { return repo.SingleById<T>(primaryKey); }
			public static T Single(string sql, params object[] args) { return repo.Single<T>(sql, args); }
			public static T Single(Sql sql) { return repo.Single<T>(sql); }
			public static T First(string sql, params object[] args) { return repo.First<T>(sql, args); }
			public static T First(Sql sql) { return repo.First<T>(sql); }
			public static List<T> Fetch(string sql, params object[] args) { return repo.Fetch<T>(sql, args); }
			public static List<T> Fetch(Sql sql) { return repo.Fetch<T>(sql); }
			public static List<T> Fetch(long page, long itemsPerPage, string sql, params object[] args) { return repo.Fetch<T>(page, itemsPerPage, sql, args); }
			public static List<T> Fetch(long page, long itemsPerPage, Sql sql) { return repo.Fetch<T>(page, itemsPerPage, sql); }
			public static List<T> SkipTake(long skip, long take, string sql, params object[] args) { return repo.SkipTake<T>(skip, take, sql, args); }
			public static List<T> SkipTake(long skip, long take, Sql sql) { return repo.SkipTake<T>(skip, take, sql); }
			public static Page<T> Page(long page, long itemsPerPage, string sql, params object[] args) { return repo.Page<T>(page, itemsPerPage, sql, args); }
			public static Page<T> Page(long page, long itemsPerPage, Sql sql) { return repo.Page<T>(page, itemsPerPage, sql); }
			public static IEnumerable<T> Query(string sql, params object[] args) { return repo.Query<T>(sql, args); }
			public static IEnumerable<T> Query(Sql sql) { return repo.Query<T>(sql); }
		}
	}
	

	[TableName("Site")]
	[PrimaryKey("SiteId")]
	[ExplicitColumns]
    public partial class Site : scoreDB.Record<Site>  
    {		
		[Column] public int SiteId { get; set; } 		
		[Column("Site")] public string _Site { get; set; }
		
		[Column] public string SiteName { get; set; } 	
	}

	[TableName("Settings")]
	[PrimaryKey("SettingsId", AutoIncrement=false)]
	[ExplicitColumns]
    public partial class Setting : scoreDB.Record<Setting>  
    {		
		[Column] public int SettingsId { get; set; } 		
		[Column] public bool AllowIonCorrection { get; set; } 	
	}

	[TableName("Group")]
	[PrimaryKey("GroupId")]
	[ExplicitColumns]
    public partial class Group : scoreDB.Record<Group>  
    {		
		[Column] public int GroupId { get; set; } 		
		[Column("Group")] public string _Group { get; set; }
		
		[Column] public string GroupName { get; set; } 		
		[Column] public string ADName { get; set; } 	
	}

	[TableName("Permit")]
	[PrimaryKey("PermitId")]
	[ExplicitColumns]
    public partial class Permit : scoreDB.Record<Permit>  
    {		
		[Column] public int PermitId { get; set; } 		
		[Column] public int WorkerId { get; set; } 		
		[Column] public int GroupId { get; set; } 		
		[Column] public int SiteId { get; set; } 	
	}

	[TableName("Measure")]
	[PrimaryKey("MeasureId")]
	[ExplicitColumns]
    public partial class Measure : scoreDB.Record<Measure>  
    {		
		[Column] public int MeasureId { get; set; } 		
		[Column] public string Symbol { get; set; } 		
		[Column] public string Mask { get; set; } 		
		[Column] public string Description { get; set; } 		
		[Column] public int DecimalPoint { get; set; } 	
	}

	[TableName("Line")]
	[PrimaryKey("LineId")]
	[ExplicitColumns]
    public partial class Line : scoreDB.Record<Line>  
    {		
		[Column] public int LineId { get; set; } 		
		[Column] public int ParentLineId { get; set; } 		
		[Column] public string Order { get; set; } 		
		[Column] public int MeasureId { get; set; } 		
		[Column] public string Description { get; set; } 		
		[Column] public bool ShowTotal { get; set; } 	
	}

	[TableName("Presence")]
	[PrimaryKey("PresenceId")]
	[ExplicitColumns]
    public partial class Presence : scoreDB.Record<Presence>  
    {		
		[Column] public int PresenceId { get; set; } 		
		[Column] public int GroupId { get; set; } 		
		[Column] public int SiteId { get; set; } 	
	}

	[TableName("Worker")]
	[PrimaryKey("WorkerId")]
	[ExplicitColumns]
    public partial class Worker : scoreDB.Record<Worker>  
    {		
		[Column] public int WorkerId { get; set; } 		
		[Column] public string EmployeeNumber { get; set; } 		
		[Column] public int? LevelId { get; set; } 		
		[Column] public int? WorkDeptId { get; set; } 		
		[Column] public int? FacilityId { get; set; } 		
		[Column] public int? RoleId { get; set; } 		
		[Column] public int GroupId { get; set; } 		
		[Column] public string FirstName { get; set; } 		
		[Column] public string LastName { get; set; } 		
		[Column] public bool IsManager { get; set; } 		
		[Column] public bool IsActive { get; set; } 		
		[Column] public bool IsPartTime { get; set; } 		
		[Column] public bool OnDisability { get; set; } 		
		[Column] public string IonName { get; set; } 		
		[Column] public bool IsAdmin { get; set; } 		
		[Column] public int? ManagerId { get; set; } 		
		[Column] public int? SiteId { get; set; } 	
	}

	[TableName("Score")]
	[PrimaryKey("ScoreId")]
	[ExplicitColumns]
    public partial class Score : scoreDB.Record<Score>  
    {		
		[Column] public int ScoreId { get; set; } 		
		[Column] public int YearEnding { get; set; } 		
		[Column] public int LineId { get; set; } 		
		[Column] public int? Target { get; set; } 		
		[Column] public int? Q1 { get; set; } 		
		[Column] public int? Q2 { get; set; } 		
		[Column] public int? Q3 { get; set; } 		
		[Column] public int? Q4 { get; set; } 		
		[Column] public int? Total { get; set; } 		
		[Column] public string Comment { get; set; } 		
		[Column] public int? GroupId { get; set; } 		
		[Column] public int? SiteId { get; set; } 	
	}

	[TableName("Responsibility")]
	[PrimaryKey("ResponsibilityId")]
	[ExplicitColumns]
    public partial class Responsibility : scoreDB.Record<Responsibility>  
    {		
		[Column] public int ResponsibilityId { get; set; } 		
		[Column] public int WorkerId { get; set; } 		
		[Column] public int LineId { get; set; } 	
	}

}



