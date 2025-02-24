//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReportTestApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RBF
    {
        public RBF()
        {
            this.APSPayables = new HashSet<APSPayable>();
            this.APSTransacts = new HashSet<APSTransact>();
            this.CrewSheets = new HashSet<CrewSheet>();
            this.TimeCards = new HashSet<TimeCard>();
            this.Transacts = new HashSet<Transact>();
            this.Transact_History = new HashSet<Transact_History>();
        }
    
        public string RBF_Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string NameDesc { get; set; }
        public string DescName { get; set; }
        public string RBF_Type_Key { get; set; }
        public Nullable<double> Budget { get; set; }
        public Nullable<System.DateTime> DateStamp { get; set; }
        public string SecurityUser_Key { get; set; }
    
        public virtual ICollection<APSPayable> APSPayables { get; set; }
        public virtual ICollection<APSTransact> APSTransacts { get; set; }
        public virtual ICollection<CrewSheet> CrewSheets { get; set; }
        public virtual RBF_Type RBF_Type { get; set; }
        public virtual ICollection<TimeCard> TimeCards { get; set; }
        public virtual ICollection<Transact> Transacts { get; set; }
        public virtual ICollection<Transact_History> Transact_History { get; set; }
    }
}
