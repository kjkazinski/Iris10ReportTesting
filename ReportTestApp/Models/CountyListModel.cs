using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportTestApp.Models
{
    public class CountyListModel
    {
        public List<string> SelectedCountyList;
        public string SelectedCounty { get; set; }
        public string SelectedCountyText { get; set; }
        public int SelectedCountyValue { get; set; }
        public bool SortCounty { get; set; }
    }


    
}