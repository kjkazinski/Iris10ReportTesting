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
    
    public partial class SWSInspectionEntry
    {
        public SWSInspectionEntry()
        {
            this.SWSInspectionEntryDetails = new HashSet<SWSInspectionEntryDetail>();
        }
    
        public string SWSInspectionEntry_Key { get; set; }
        public string SWSManagementSection_Key { get; set; }
        public Nullable<System.DateTime> InspectionDate { get; set; }
        public Nullable<decimal> BeginMilepost { get; set; }
        public Nullable<decimal> EndMilepost { get; set; }
        public Nullable<int> InspectionLengthFeet { get; set; }
        public string Flag { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> DateStamp { get; set; }
        public string SecurityUser_Key { get; set; }
    
        public virtual SWSManagementSection SWSManagementSection { get; set; }
        public virtual ICollection<SWSInspectionEntryDetail> SWSInspectionEntryDetails { get; set; }
    }
}
