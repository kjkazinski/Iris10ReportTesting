using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportTestApp.Models
{
    public class TreeView
    {
            public string Activity_Key { get; set; }

            
            public string Name { get; set; }

          
            public string Description { get; set; }
        
            public string NameDesc { get; set; }
        
            public string DescName { get; set; }
        
            public decimal Perform_Standard { get; set; }
        
            public string Work_Unit { get; set; }
        
            public string WorkComp_Key { get; set; }
        
            public string UOM_Key { get; set; }
        
            public string Work_Methods { get; set; }
        
            public string Inspection { get; set; }
        
            public string Authorize { get; set; }
        
            public bool Active { get; set; }
        
            public string User1 { get; set; }
        
            public string User2 { get; set; }
    
            public string User3 { get; set; }
        
            public string User4 { get; set; }
        
            public string User5 { get; set; }
        
            public string User6 { get; set; }
        
            public string User7 { get; set; }
        
            public string User8 { get; set; }
        
            public string User9 { get; set; }
        
            public string User10 { get; set; }
        
            public Nullable<DateTime> CreateDate { get; set; }
        
            public Nullable<DateTime> DateStamp { get; set; }
        
            public string SecurityUser_Key { get; set; }
        

    }
}