using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportFormat.Model
{
    
    public class ReportModel
    {
        public ReportModel(string json)
        {
            JObject jObject = JObject.Parse(json);
            JToken jUser = jObject["myRoot"];
            ReportName = (string)jUser["ReportName"];
            ConnectionString = (string)jUser["ConnectionString"];
            SelectCommand = (string)jUser["SelectCommand"];
            GroupBy = jUser["GroupBy"].ToArray();
            GenerateTitleField = jUser["GenerateTitleField"].ToArray();
            GenerateDataField = jUser["GenerateDataField"].ToArray();
        }



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
        public Array GroupBy { get; set; }

        public List<string> AddSortings { get; set; }

        public Array GenerateTitleField { get; set; }

        public Array GenerateDataField { get; set; }

        public List<string> SumOrCount { get; set; }

        public List<int> ChangeBandColor { get; set; }

        public List<string> Filters { get; set; }

        public string AddReportFooterSection { get; set; }
        public string AddPageNumbers { get; set; }

    }
}





