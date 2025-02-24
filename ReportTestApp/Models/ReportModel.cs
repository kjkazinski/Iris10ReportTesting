﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IrisWeb.Reports.Model
{
    
    public class ReportModel
    {
        public string ReportName { get; set; }
        public string AddReportHeader { get; set; }

        //call only
        public string GenerateHeaderFooters { get; set; }

        //recieve connection string, no annotations
        public string ConnectionString { get; set; }

        //recieve selection string
        public string SelectCommand { get; set; }

        //recieve county
        public string CountyName { get; set; }

        //recieve data field, annotations define function
        public List<string> GroupBy { get; set; }

        public List<string> GroupName { get; set; }

        public List<string> AddSortings { get; set; }

        public List<string> GenerateTitleField { get; set; }

        public List<string> GenerateDataField { get; set; }

        public List<string> SumOrCount { get; set; }
        
       public List<string> AggregateType { get; set; }

        public List<int> ChangeBandColor { get; set; }

        public List<string> Filters { get; set; }

        public bool AddReportFooterSection { get; set; }

        public string AddPageNumbers { get; set; }

        public bool StoreReportData { get; set; }

        public List<string> SavedReportData { get; set; }

    }
}





