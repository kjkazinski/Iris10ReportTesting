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
    
    public partial class CRWSaveReport
    {
        public CRWSaveReport()
        {
            this.CRWSaveReportUsers = new HashSet<CRWSaveReportUser>();
        }
    
        public string CRWSaveReport_Key { get; set; }
        public string Module_Key { get; set; }
        public string BaseTable { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public string Fields { get; set; }
        public string ColumnWidths { get; set; }
        public string FieldsAlias { get; set; }
        public string SortedColumns { get; set; }
        public string GroupByColumns { get; set; }
        public string PageBreakColumns { get; set; }
        public string HiddenColumns { get; set; }
        public string SumColumns { get; set; }
        public string AvgColumns { get; set; }
        public string MinColumns { get; set; }
        public string MaxColumns { get; set; }
        public string CountColumns { get; set; }
        public Nullable<int> TotalRan { get; set; }
        public Nullable<System.DateTime> LastRanDate { get; set; }
        public string Filter { get; set; }
        public string FilterDescription { get; set; }
        public string SearchField { get; set; }
        public string Author_Key { get; set; }
        public byte ShowSelectionCriteria { get; set; }
        public byte ShowGridLines { get; set; }
        public byte ShowPageNumber { get; set; }
        public byte ShowFileName { get; set; }
        public byte HideDetails { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> DateStamp { get; set; }
        public string SecurityUser_Key { get; set; }
    
        public virtual SecurityUser SecurityUser { get; set; }
        public virtual SecurityUser SecurityUser1 { get; set; }
        public virtual ICollection<CRWSaveReportUser> CRWSaveReportUsers { get; set; }
    }
}
