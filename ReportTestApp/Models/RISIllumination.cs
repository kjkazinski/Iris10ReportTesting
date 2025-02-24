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
    
    public partial class RISIllumination
    {
        public string RISIllumination_Key { get; set; }
        public string Road_Key { get; set; }
        public decimal BeginMilepost { get; set; }
        public decimal EndMilepost { get; set; }
        public Nullable<short> NumberOfLights { get; set; }
        public string RISLookupIlluminationPole_Key { get; set; }
        public string RISLookupIlluminationMast_Key { get; set; }
        public string RISLookupIlluminationBase_Key { get; set; }
        public string RISLookupIlluminationLightSource_Key { get; set; }
        public Nullable<short> LightWattage { get; set; }
        public string RISLookupIlluminationPlacement_Key { get; set; }
        public string PostNumber { get; set; }
        public string AssetNumber { get; set; }
        public string Owner { get; set; }
        public string MaintJurisdiction { get; set; }
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
        public byte Active { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> DateStamp { get; set; }
        public string SecurityUser_Key { get; set; }
    
        public virtual RISLookupIlluminationBase RISLookupIlluminationBase { get; set; }
        public virtual RISLookupIlluminationLightSource RISLookupIlluminationLightSource { get; set; }
        public virtual RISLookupIlluminationMast RISLookupIlluminationMast { get; set; }
        public virtual RISLookupIlluminationPlacement RISLookupIlluminationPlacement { get; set; }
        public virtual RISLookupIlluminationPole RISLookupIlluminationPole { get; set; }
        public virtual Road Road { get; set; }
    }
}
