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
    
    public partial class VMSVegetationClass
    {
        public VMSVegetationClass()
        {
            this.VMSVegetations = new HashSet<VMSVegetation>();
            this.VMSVegetationCodes = new HashSet<VMSVegetationCode>();
        }
    
        public string VMSVegetationClass_Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte Active { get; set; }
        public Nullable<System.DateTime> DateStamp { get; set; }
        public string SecurityUser_Key { get; set; }
    
        public virtual ICollection<VMSVegetation> VMSVegetations { get; set; }
        public virtual ICollection<VMSVegetationCode> VMSVegetationCodes { get; set; }
    }
}
