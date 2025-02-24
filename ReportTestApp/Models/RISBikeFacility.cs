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
    
    public partial class RISBikeFacility
    {
        public string RISBikeFacility_Key { get; set; }
        public string Road_Key { get; set; }
        public decimal BeginMilepost { get; set; }
        public decimal EndMilepost { get; set; }
        public string RISLookupBikeFacilityPlacement_Key { get; set; }
        public string RISLookupBikeFacility_Key { get; set; }
        public string RISLookupBikeFacilitySurface_Key { get; set; }
        public Nullable<double> Width { get; set; }
        public string RISLookupCondition_Key { get; set; }
        public Nullable<System.DateTime> DateInspected { get; set; }
        public string Comments { get; set; }
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
        public string Flag { get; set; }
        public Nullable<byte> Active { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> DateStamp { get; set; }
        public string SecurityUser_Key { get; set; }
    
        public virtual RISLookupBikeFacilityPlacement RISLookupBikeFacilityPlacement { get; set; }
        public virtual RISLookupBikeFacilitySurface RISLookupBikeFacilitySurface { get; set; }
        public virtual RISLookupCondition RISLookupCondition { get; set; }
        public virtual Road Road { get; set; }
    }
}
