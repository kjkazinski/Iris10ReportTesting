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
    
    public partial class Vendor
    {
        public Vendor()
        {
            this.Contracts = new HashSet<Contract>();
            this.EMSParts = new HashSet<EMSPart>();
            this.Inv_Receive = new HashSet<Inv_Receive>();
            this.Inv_Transact = new HashSet<Inv_Transact>();
            this.Inventories = new HashSet<Inventory>();
            this.Transacts = new HashSet<Transact>();
            this.VMSSprayReports = new HashSet<VMSSprayReport>();
        }
    
        public string Vendor_Key { get; set; }
        public string APSBudgetDetail_Key { get; set; }
        public string Vendor_No { get; set; }
        public string Alternate_Vendor_No { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string NameDesc { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string ZipCode { get; set; }
        public string CityStateZip { get; set; }
        public string ShipAddress1 { get; set; }
        public string ShipAddress2 { get; set; }
        public string ShipCity { get; set; }
        public string ShipState { get; set; }
        public string ShipZipCode { get; set; }
        public string ShipCityStateZip { get; set; }
        public string CountyAccount { get; set; }
        public string TaxID { get; set; }
        public Nullable<byte> Send1099 { get; set; }
        public string BoxNumber1099 { get; set; }
        public string LineNumber { get; set; }
        public string Fund { get; set; }
        public string Department { get; set; }
        public string Terms { get; set; }
        public string Comments { get; set; }
        public byte Active { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string Email { get; set; }
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
        public Nullable<System.DateTime> Datestamp { get; set; }
        public string SecurityUser_Key { get; set; }
    
        public virtual APSBudgetDetail APSBudgetDetail { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<EMSPart> EMSParts { get; set; }
        public virtual ICollection<Inv_Receive> Inv_Receive { get; set; }
        public virtual ICollection<Inv_Transact> Inv_Transact { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<Transact> Transacts { get; set; }
        public virtual ICollection<VMSSprayReport> VMSSprayReports { get; set; }
    }
}
