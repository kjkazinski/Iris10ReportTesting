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
    
    public partial class GridConfigDetail
    {
        public string GridConfigDetail_Key { get; set; }
        public string GridConfig_Key { get; set; }
        public string FieldName { get; set; }
        public short ColumnOrder { get; set; }
        public byte Fixed { get; set; }
        public double ColumnWidth { get; set; }
        public short SortOrder { get; set; }
        public string SortIndicator { get; set; }
        public byte CopyDown { get; set; }
        public byte UserConfigRequired { get; set; }
        public byte BandIndex { get; set; }
        public string FieldProp_Key { get; set; }
        public Nullable<System.DateTime> Datestamp { get; set; }
        public string SecurityUser_Key { get; set; }
    
        public virtual GridConfig GridConfig { get; set; }
    }
}
