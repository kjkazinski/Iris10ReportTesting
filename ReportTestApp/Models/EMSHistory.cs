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
    
    public partial class EMSHistory
    {
        public string EMSHistory_Key { get; set; }
        public string Equipment_Key { get; set; }
        public string EMSPartList_Key { get; set; }
        public string EMSPolicy_Key { get; set; }
        public string Vendor_Key { get; set; }
        public string EMSRepairType_Key { get; set; }
        public Nullable<int> LastReadingMiles { get; set; }
        public Nullable<int> LastReadingHours { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string EMSPMMethod_Key { get; set; }
        public Nullable<System.DateTime> MaintenanceDate { get; set; }
        public string Employee_Key { get; set; }
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
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> DateStamp { get; set; }
        public string SecurityUser_Key { get; set; }
        public string EMSWorkOrder_Key { get; set; }
        public string EMSWorkOrderDetail_Key { get; set; }
        public byte PostedFromWorkOrder { get; set; }
        public string WorkOrderDescription { get; set; }
        public string ServiceDescription { get; set; }
    
        public virtual EMSRepairType EMSRepairType { get; set; }
        public virtual EMSPartList EMSPartList { get; set; }
        public virtual EMSPMMethod EMSPMMethod { get; set; }
        public virtual EMSPolicy EMSPolicy { get; set; }
        public virtual EMSWorkOrder EMSWorkOrder { get; set; }
        public virtual EMSWorkOrderDetail EMSWorkOrderDetail { get; set; }
        public virtual Equipment Equipment { get; set; }
    }
}
