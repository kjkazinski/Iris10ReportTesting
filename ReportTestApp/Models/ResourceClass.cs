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
    
    public partial class ResourceClass
    {
        public ResourceClass()
        {
            this.Employees = new HashSet<Employee>();
            this.Equipments = new HashSet<Equipment>();
            this.Inventories = new HashSet<Inventory>();
            this.ProjectResourceItemBillRates = new HashSet<ProjectResourceItemBillRate>();
            this.ResourceClassRates = new HashSet<ResourceClassRate>();
            this.Transacts = new HashSet<Transact>();
            this.Transact_History = new HashSet<Transact_History>();
            this.TimeCards = new HashSet<TimeCard>();
        }
    
        public string ResourceClass_Key { get; set; }
        public string Resource_Type_Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string NameDesc { get; set; }
        public string DescName { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> Percent_Avail { get; set; }
        public string UOM_Key { get; set; }
        public byte Active { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> DateStamp { get; set; }
        public string SecurityUser_Key { get; set; }
    
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Equipment> Equipments { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<ProjectResourceItemBillRate> ProjectResourceItemBillRates { get; set; }
        public virtual Resource_Type Resource_Type { get; set; }
        public virtual ICollection<ResourceClassRate> ResourceClassRates { get; set; }
        public virtual UOM UOM { get; set; }
        public virtual ICollection<Transact> Transacts { get; set; }
        public virtual ICollection<Transact_History> Transact_History { get; set; }
        public virtual ICollection<TimeCard> TimeCards { get; set; }
    }
}
