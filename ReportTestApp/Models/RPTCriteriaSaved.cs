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
    
    public partial class RPTCriteriaSaved
    {
        public RPTCriteriaSaved()
        {
            this.RPTCriteriaSavedDetails = new HashSet<RPTCriteriaSavedDetail>();
            this.RPTSortSaveds = new HashSet<RPTSortSaved>();
        }
    
        public string RPTCriteriaSaved_Key { get; set; }
        public string SecurityObject_Key { get; set; }
        public string SaveName { get; set; }
        public string SecurityUser_Key { get; set; }
        public Nullable<System.DateTime> DateStamp { get; set; }
    
        public virtual SecurityObject SecurityObject { get; set; }
        public virtual ICollection<RPTCriteriaSavedDetail> RPTCriteriaSavedDetails { get; set; }
        public virtual ICollection<RPTSortSaved> RPTSortSaveds { get; set; }
    }
}
