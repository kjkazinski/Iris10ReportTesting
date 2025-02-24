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
    
    public partial class Locate
    {
        public string Locate_Key { get; set; }
        public string CallerIDNumber { get; set; }
        public string InternalLocateNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string NearestCity { get; set; }
        public string ContactPhone { get; set; }
        public string FaxNumber { get; set; }
        public Nullable<short> NumberLocates { get; set; }
        public Nullable<short> PagesTransmitted { get; set; }
        public Nullable<byte> PowerLinesNear { get; set; }
        public string TypeofWork { get; set; }
        public string WorkDoneFor { get; set; }
        public string County { get; set; }
        public string WorkAddress { get; set; }
        public string IntersectingRoad { get; set; }
        public string DistanceFromIntersectingRoad { get; set; }
        public string DirectionFromIntersectingRoad { get; set; }
        public string LocateofWork { get; set; }
        public string Comments { get; set; }
        public string Township1 { get; set; }
        public string Township2 { get; set; }
        public string Range1 { get; set; }
        public string Range2 { get; set; }
        public string Section1 { get; set; }
        public string Section2 { get; set; }
        public string QuarterSection1 { get; set; }
        public string QuarterSection2 { get; set; }
        public Nullable<System.DateTime> ReceiveDate { get; set; }
        public Nullable<System.DateTime> OUNCDateReceived { get; set; }
        public Nullable<System.DateTime> OUNCTimeReceived { get; set; }
        public Nullable<int> OUNCRequestsReceived { get; set; }
        public Nullable<int> OUNCRequestsProcessed { get; set; }
        public string OUNCProcessedBy { get; set; }
        public string OUNCVerifiedBy { get; set; }
        public Nullable<System.DateTime> Datestamp { get; set; }
        public string SecurityUser_Key { get; set; }
    }
}
