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
    
    public partial class ODOTPermitBridge
    {
        public string ODOTPermitBridge_Key { get; set; }
        public string ODOTPermit_Key { get; set; }
        public string BridgeNumber { get; set; }
        public Nullable<int> MaxMPH { get; set; }
        public byte DriveDownCenter { get; set; }
        public byte AloneOnBridge { get; set; }
        public string OtherConditions { get; set; }
        public string User1 { get; set; }
        public string User2 { get; set; }
        public string User3 { get; set; }
        public string User4 { get; set; }
        public string User6 { get; set; }
        public string User5 { get; set; }
        public string User7 { get; set; }
        public string User8 { get; set; }
        public string User9 { get; set; }
        public string User10 { get; set; }
        public Nullable<System.DateTime> DateStamp { get; set; }
        public string SecurityUser_Key { get; set; }
    
        public virtual ODOTPermit ODOTPermit { get; set; }
    }
}
