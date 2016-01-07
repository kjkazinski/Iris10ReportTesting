using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Reporting;
using IrisWeb;
using IrisWeb.Code.Data.Models.Database;
using IrisWeb.Code.Data.Filters;
using System.Web.Script.Serialization;
using System.Diagnostics;
using System.Xml.Serialization;
using System.IO;
using ReportTestApp.Models;
using Kendo.Mvc.Extensions;
using Newtonsoft.Json;
using Kendo.Mvc.Examples.Models;
using Kendo.Mvc.UI;
using ExtensionMethods;
using Testing.Models;
using ReportTestApp.TreeViewScripts;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using IrisWeb.Reports.Model;

/*
Notes:
Store stuff somewhere until you are ready to generate a report.
create a button for generation mode.
grabs all selected data from the java and then generates the report based off the data
do things in order or the report will fail.
    */



namespace ReportTestApp.Controllers
{
    public class HomeController : Controller
    {
        public static string CountyName = "";
        public static CountyListModel myCountyList = new CountyListModel();
        public static string SortField;
        public static string SQLCommandThingy;
        public static string[] myDataFields;
        public static string StartDate;
        public static string EndDate;
        public static string SortBy;
        public static bool SortBool = false;
        public static List<TestModel> TreeViewData;
        public static List<string> GroupNames = new List<string>();
        public static List<string> GroupFields = new List<string>();
        public static List<string> SortGroups = new List<string>();
        public static List<int> SortGroupHeaders = new List<int>();
        public static List<string> FieldNames = new List<string>();
        public static List<string> FieldKeys = new List<string>();




        public ActionResult About()
        {
            
            return View();
        }

        [HttpGet]
        public void ClearData()
        {
            GroupNames.Clear();
            GroupFields.Clear();
            SortGroups.Clear();
            FieldNames.Clear();
            FieldKeys.Clear();
        }


        public void StorageFunction(string groupName = null, string groupField = null, string sortGroup = null, int sortGroupHeader = -1, string fieldName = null, string fieldKey = null)
        {
            if(groupName != null)
            {
                Debug.WriteLine(groupName);
                GroupNames.Add(groupName);
            }
            if (groupField != null)
            {
                GroupFields.Add(groupField);
            }
            if (sortGroup != null)
            {
                SortGroups.Add(sortGroup);
            }
            if (fieldName != null)
            {
                FieldNames.Add(fieldName);
            }
            if (fieldKey != null)
            {
                FieldKeys.Add(fieldKey);
            }
            if(sortGroupHeader != -1)
            {
                SortGroupHeaders.Add(sortGroupHeader);
            }
            Debug.WriteLine("1: " + groupName + " 2: " + groupField + " 3: " + sortGroup + " 4: " + fieldName + " 5: " + fieldKey);
            Debug.WriteLine("Keys " + GroupNames.ToJSON());
        }


        public ActionResult TestDataSet(string tree)
        {
            ActivityTree currentTree = new ActivityTree();
            if (tree == "Activity")
            {
                TreeViewData = currentTree.ActivityData();
            }else if(tree == "Transact")
            {
                TreeViewData = currentTree.TransactData();
            }

            
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(serializer.Serialize(TreeViewData),JsonRequestBehavior.AllowGet);

        }


        public ActionResult Index()
        {
            ////////////////////////
            //MyClass r = new MyClass(4.5, 7.5);
            //r.Display();
            //Type type = typeof(MyClass);

            ////iterating through the attribtues of the Rectangle class
            //foreach (Object attributes in type.GetCustomAttributes(false))
            //{
            //    ReportAttribute dbi = (ReportAttribute)attributes;
            //    if (null != dbi)
            //    {
            //        Debug.WriteLine("Bug no: {0}", dbi.BugNo);
            //        Debug.WriteLine("Developer: "+ dbi.Developer);
            //        Debug.WriteLine("Last Reviewed: "+ dbi.LastReview);
            //        Debug.WriteLine("Remarks: "+ dbi.Message);
            //    }
            //}

            ////iterating through the method attribtues
            //foreach (MethodInfo m in type.GetMethods())
            //{
            //    foreach (Attribute a in m.GetCustomAttributes(true))
            //    {
            //        ReportAttribute dbi = a as ReportAttribute; //This fails?
            //        if (null != dbi)
            //        {
            //            Debug.WriteLine("Bug no: {0}, for Method: {1}", dbi.BugNo, m.Name);
            //            Debug.WriteLine("Developer: "+ dbi.Developer);
            //            Debug.WriteLine("Last Reviewed: "+ dbi.LastReview);
            //            Debug.WriteLine("Remarks: "+ dbi.Message);
            //        }
            //    }
            //}



            ///////////////////////

            //Console.ReadKey();
            //for (var i = 0; i < myCountyList.SelectedCountyList.Count; i++) {
            //    Debug.WriteLine(myCountyList.SelectedCountyList[i]);
            //}
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            //myCountyList.SelectedCountyList = new List<string>();
            //myCountyList.SelectedCountyList.Add("Z_Baker");
            //myCountyList.SelectedCountyList.Add("Z_Benton");
            //myCountyList.SelectedCountyList.Add("Z_Clatsop");
            //myCountyList.SelectedCountyList.Add("Z_Columbia");
            //myCountyList.SelectedCountyList.Add("Z_Coos");
            //myCountyList.SelectedCountyList.Add("Z_Crook");
            //myCountyList.SelectedCountyList.Add("Z_Curry");
            //myCountyList.SelectedCountyList.Add("Z_Douglas");
            //myCountyList.SelectedCountyList.Add("Z_Gilliam");
            //myCountyList.SelectedCountyList.Add("Z_GrantCo");
            //myCountyList.SelectedCountyList.Add("Z_Harney");
            //myCountyList.SelectedCountyList.Add("Z_Hood");
            //myCountyList.SelectedCountyList.Add("Z_Jackson");
            //myCountyList.SelectedCountyList.Add("Z_Jefferson");
            //myCountyList.SelectedCountyList.Add("Z_Josephine");
            //myCountyList.SelectedCountyList.Add("Z_Klamath");
            //myCountyList.SelectedCountyList.Add("Z_Lake");
            //myCountyList.SelectedCountyList.Add("Z_Lincoln");
            //myCountyList.SelectedCountyList.Add("Z_Linn");
            //myCountyList.SelectedCountyList.Add("Z_Marion");
            //myCountyList.SelectedCountyList.Add("Z_Morrow");
            //myCountyList.SelectedCountyList.Add("Z_Polk");
            //myCountyList.SelectedCountyList.Add("Z_Sherman");
            //myCountyList.SelectedCountyList.Add("Z_Tillamook");
            //myCountyList.SelectedCountyList.Add("Z_Umatilla");
            //myCountyList.SelectedCountyList.Add("Z_UnionCo");
            //myCountyList.SelectedCountyList.Add("A_Wallowa9");
            //myCountyList.SelectedCountyList.Add("Z_Wasco");
            //myCountyList.SelectedCountyList.Add("Z_Wheeler");
            //myCountyList.SelectedCountyList.Add("Z_Yamhill");

            //Default Report
           // ReportLibrary2.Report3.GenerateHeaderFooters();
           // ReportLibrary2.Report3.ChangeSqlString("Initial Catalog=A_Wallowa9;Data Source=10.0.0.40;User ID=developer;Password=aociris;");
           // ReportLibrary2.Report3.CountyName = "Wallowa";
           // ReportLibrary2.Report3.GroupBy("Employee Key: ", "= Fields.Employee_Key");
           // ReportLibrary2.Report3.GroupBy("Task Date: ", "= Fields.Task_Date");
           // ReportLibrary2.Report3.AddSortings(2, "= Fields.Transact_Key", SortDirection.Desc);
           // ReportLibrary2.Report3.AddSortings(0, "= Fields.Task_Date", SortDirection.Desc);
           // ReportLibrary2.Report3.GroupBy("Date Stamp: ", "= Fields.DateStamp");
           // // Report2.GroupBy("Test Band: ", "= Fields.Description", GroupCount);
           //ReportLibrary2.Report3.GenerateTextField("Date Stamp", "= Fields.DateStamp");
           //ReportLibrary2.Report3.GenerateTextField("Description", "= Fields.Description");
           //ReportLibrary2.Report3.GenerateTextField("Activity Key", "= Fields.Activity_Key");
           //ReportLibrary2.Report3.GenerateTextField("Transact Key", "= Fields.Transact_Key");
           // ReportLibrary2.Report3.GenerateTextField("Total Resource Cost", "= Fields.TotalResourceCost");
           // //Report2.GenerateTextField("UOM Key", "= Fields.UOM_Key");
           // ReportLibrary2.Report3.SumOrCount("Total Resource Cost", 1, "Fields.TotalResourceCost"); //subtotals
           // ReportLibrary2.Report3.SumOrCount("Transact Key", 0, "Fields.Transact_Key");
           // ReportLibrary2.Report3.ChangeBandColor(0, 2, 128, 128, 255);
           // ReportLibrary2.Report3.ChangeBandColor(0, 0, 10, 128, 10);
           // ReportLibrary2.Report3.GroupCount += 1; //Add group for band titles
           // ReportLibrary2.Report3.AddReportFooterSection(1, "Fields.TotalResourceCost", "Total Resource Cost");
           // SQLCommandThingy = "SELECT TOP 6000 * FROM Activity,Transact";
           // //SQLCommandThingy = "SELECT TOP 1 * FROM Activity,Transact";
           // //Debug.WriteLine("sdfsdf " + SQLCommandThingy);
           // ReportLibrary2.Report3.SQLCommandString = SQLCommandThingy;

            return View();
        }

        [HttpPost]
        public ActionResult SetStyles(int R, int G, int B, int header)
        {
            Debug.WriteLine("header " + header, " red " + R + " green " + G + " blue " + B);
            //ReportLibrary2.Report3.SetCaptionColors(R, G, B);
            ReportLibrary2.Report3.ChangeBandColor(0, header, R, G, B);
            return Content("Success");
        }

        [HttpPost]
        public ActionResult SetSort(string sortOption)
        {
            SortBool = true;
            SortBy = sortOption;
           // Debug.WriteLine(sortOption);
            StorageFunction(sortOption, sortOption);
            //ReportLibrary2.Report3.GroupBy(SortBy+": ", "= Fields."+ SortBy, 0);
            // ReportLibrary2.Report3.SortOptions("=Fields." + SortBy, SortDirection.Asc, SortBool);
            //ReportLibrary2.Report3.AddSortings(0, "=Fields." + SortBy, SortDirection.Asc);
            return Content("success");
        }

        [HttpPost]
        public ActionResult SetRealSort(int header, string sortOption)
        {
            SortBool = true;
            SortBy = sortOption;
            Debug.WriteLine("sdfsdfsd f : "+sortOption);
            StorageFunction(null, null, sortOption,header);
            //ReportLibrary2.Report3.GroupBy(SortBy+": ", "= Fields."+ SortBy, 0);
            // ReportLibrary2.Report3.SortOptions("=Fields." + SortBy, SortDirection.Asc, SortBool);
            //ReportLibrary2.Report3.AddSortings(0, "=Fields." + SortBy, SortDirection.Asc);
            return Content("success");
        }

        [HttpPost]
        public ActionResult DateFilter(string myStartDate, string myEndDate)
        {
            StartDate = myStartDate;
            EndDate = myEndDate;
           // ReportLibrary2.Report3.AddDateFilter(StartDate, EndDate);
            return Content("Success");
        }

        [HttpGet]
        public ActionResult GetStuff(string myTable)
        {
            // var data = ModelBase.LoadModel<ActivityModel>();
            Debug.WriteLine("HELLO WORLD~! " + myTable);
            if (myTable == "Activity")
            {
                var sqlGen = new SqlGenerator(SqlGenerator.SqlTypes.Select, "Activity");
                //sqlGen.SelectStatementLimit = 1;
                var data = ModelBase.LoadModel<ActivityModel>(sqlGen);
                Debug.WriteLine(data.ToJSON());
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else if (myTable == "Transact")
            {
                var sqlGen = new SqlGenerator(SqlGenerator.SqlTypes.Select, "Transact");
                sqlGen.SelectStatementLimit = 1;
                var data = ModelBase.LoadModel<TransactModel>(sqlGen);
                return Json(data, JsonRequestBehavior.AllowGet);
            }

            return null;

        }

        [HttpPost]
        public ActionResult SelectedCountyInList(int id, string county, bool sort, string datasort, string[] myData, string myStartDate, string myEndDate)
        {
            //set up County List

            myCountyList.SelectedCountyText = county;
            myCountyList.SortCounty = sort;
            CountyName = myCountyList.SelectedCountyList[id];
            SortField = datasort;
            myDataFields = new string[myData.Count()];
            myDataFields = myData;
            string myString = "";
            StartDate = myStartDate;
            EndDate = myEndDate;
            for (int i = 0; i < myData.Count(); i++)
            {
                Debug.WriteLine("sdfsdf " + myData[i]);
                myString += myData[i];

                if ((i + 1) < myData.Count())
                {
                    myString += ", ";
                }
            }

            //"SELECT        Activity.*\r\nFROM            Activity"
            SQLCommandThingy = "SELECT " + myString + " FROM Activity";
            Debug.WriteLine("SQL: " + SQLCommandThingy);



            return Content(null);
        }

       

        [HttpGet]
        public ActionResult SetupReport(string table, string dataFields, string dataFieldNames, int id, string county)
        {
            string myString = "";
            string[] myData = dataFields.Split(',');
            string[] myNames = dataFieldNames.Split(',');
            
            myCountyList.SelectedCountyText = county;
            CountyName = myCountyList.SelectedCountyList[id];
            // ReportLibrary2.Report3.AddGrouping("DateStamp");
           // ReportLibrary2.Report3.GroupBy("Date Stamp: ", "=Fields.DateStamp", 0);
            if (myData.Count() == 1)
            {
                if (myData[0] == "[]")
                {
                    ReportLibrary2.Report3.ResetDefaults();
                    //ReportLibrary2.Report3.ChangeSqlString("Initial Catalog=" + CountyName + ";Data Source=192.168.104.202;User ID=developer;Password=aociris;");
                   // ReportLibrary2.Report3.ChangeSqlString("Initial Catalog=" + CountyName + ";Data Source=10.0.0.40;User ID=developer;Password=aociris;");
                    //ReportLibrary2.Report3.CountyName = myCountyList.SelectedCountyText;
                   // SQLCommandThingy = "SELECT TOP 10 * FROM " + table;
                    //SQLCommandThingy = "SELECT * FROM " + table;
                    Debug.WriteLine("sdfsdf " + SQLCommandThingy);
                    //ReportLibrary2.Report3.SQLCommandString = SQLCommandThingy;
                    return View(null, null, null);
                }
                myData[0] = myData[0].Substring(2, myData[0].Length - 4);
                myNames[0] = myNames[0].Substring(2, myNames[0].Length - 4);
            }
            else if (myData.Count() > 1)
            {
                myData[0] = myData[0].Substring(2, myData[0].Length - 3);
                myNames[0] = myNames[0].Substring(2, myNames[0].Length - 3);

                for (int i = 1; i < myData.Count() - 1; i++)
                {
                    myData[i] = myData[i].Substring(1, myData[i].Length - 2);
                    myNames[i] = myNames[i].Substring(1, myNames[i].Length - 2);
                }
                myData[myData.Count() - 1] = myData[myData.Count() - 1].Substring(1, myData[myData.Count() - 1].Length - 3);
                myNames[myNames.Count() - 1] = myNames[myNames.Count() - 1].Substring(1, myNames[myNames.Count() - 1].Length - 3);
            }
            //if (myData.Count() > 1)
            //{
            //    for (int i = 1; i < myData.Count() - 1; i++)
            //    {
            //        myData[i] = myData[i].Substring(1, myData[i].Length - 2);
            //        myNames[i] = myNames[i].Substring(1, myNames[i].Length - 2);
            //    }
            //    myData[myData.Count() - 1] = myData[myData.Count() - 1].Substring(1, myData[myData.Count() - 1].Length - 3);
            //    myNames[myNames.Count() - 1] = myNames[myNames.Count() - 1].Substring(1, myNames[myNames.Count() - 1].Length - 3);
            //}
            //ReportLibrary2.Report3.ResetDefaults();
            //ReportLibrary2.Report3.GenerateHeaderFooters();
            //if (ReportLibrary2.Report3.GroupCount == -1)
            //{
            //   // ReportLibrary2.Report3.GroupBy("Date Stamp: ", "= Fields.DateStamp", 0);
            //    ReportLibrary2.Report3.GroupBy("Employee Key: ", "= Fields.Employee_Key", 0);
            //    ReportLibrary2.Report3.GroupBy("Task Date: ", "= Fields.Task_Date", 0);
            //}
            //if(GroupNames.Count >= 0)
            //{
            //Debug.WriteLine(GroupNames.Count + " groupCount " + GroupNames.ToJSON());
            //for (int i = 0; i < GroupNames.Count; i++)
            //{
            //    Debug.WriteLine("groupCount2 " + GroupNames[i]);
            //    ReportLibrary2.Report3.GroupBy(GroupNames[i] + ": ", "= Fields." + GroupNames[i], 0);
            //    ReportLibrary2.Report3.AddSortings(0, "= Fields." + GroupNames[i], SortDirection.Asc);
            //}
            //}
            //ReportLibrary2.Report3.GroupCount += 1;
            //ReportLibrary2.Report3.ChangeSqlString("Initial Catalog=" + CountyName + ";Data Source=10.0.0.40;User ID=developer;Password=aociris;");
            //ReportLibrary2.Report3.CountyName = myCountyList.SelectedCountyText;
            Debug.WriteLine("HERE IS MY TEAPOT: " + myData.Count());
            //SQLCommandThingy = "SELECT TOP 10 * FROM " + table;
            //SQLCommandThingy = "SELECT * FROM Transact LEFT JOIN Activity ON Transact.Activity_Key = Activity.Activity_Key";// + table;
            Debug.WriteLine("sdfsdf " + SQLCommandThingy);
            // ReportLibrary2.Report3.SQLCommandString = SQLCommandThingy;
            FieldNames.Clear();
            FieldKeys.Clear();
            for (int i = 0; i < myData.Count(); i++)
            {
                if (myData[i] == "*")
                {
                    //Skip
                }
                else
                {
                    Debug.WriteLine("Name " + myNames[i]+" Data "+myData[i]);
                    StorageFunction(null, null, null,-1, myNames[i], "=Fields." + myData[i]);
                    //ReportLibrary2.Report3.GenerateTextField(myNames[i], "=Fields." + myData[i]);
                   // ReportLibrary2.Report3.GenerateTextField(myNames[i]);
                   // ReportLibrary2.Report3.GenerateDataField("=Fields." + myData[i]);
                }
            }
            GetReport();
            return View(null, null, null);
        }


        [HttpPost]
        public ActionResult GetReport()
        {
            ReportLibrary2.Report3.ResetDefaults();
            ReportLibrary2.Report3.GenerateHeaderFooters();
            Debug.WriteLine("headers: " + GroupNames.ToJSON());
            for (int i = 0; i < GroupNames.Count; i++)
            {
                Debug.WriteLine("groupCount2 " + GroupNames[i]);
                ReportLibrary2.Report3.GroupBy(GroupNames[i] + ": ", "= Fields." + GroupNames[i]);
            }
            for(int i = 0; i < SortGroups.Count; i++)
            {
                ReportLibrary2.Report3.AddSortings(2, "= Fields." + SortGroups[i], SortDirection.Asc);
            }
            ReportLibrary2.Report3.GroupCount += 1;
            for (int i = 0; i < FieldNames.Count(); i++)
            {
                if (FieldNames[i] == "*")
                {
                    //Skip
                }
                else
                {
                    Debug.WriteLine("Name " + FieldNames[i] + " Data " + FieldKeys[i]);
                    ReportLibrary2.Report3.GenerateTextField(FieldNames[i], FieldKeys[i]);
                    // ReportLibrary2.Report3.GenerateTextField(myNames[i]);
                    // ReportLibrary2.Report3.GenerateDataField("=Fields." + myData[i]);
                }
            }
            var myReportModel = new ReportModel();
            myReportModel.GenerateTitleField = FieldNames;
            myReportModel.GenerateDataField = FieldKeys;
            myReportModel.GroupBy = GroupNames;
            myReportModel.AddSortings = SortGroups;

            Debug.WriteLine(myReportModel.ToString());

            //ReportLibrary2.Report3.TestFunction(myReportModel.GenerateTitleField.ToList());
            //Debug.WriteLine("This is sparta! "+myReportModel.GenerateTitleField[0] + " " + myReportModel.GenerateDataField[0]);

            return Content("Success");
        }

        public ActionResult ReportViewerView1()
        {
            ViewBag.Message = "Your contact page.";
            // Debug.WriteLine("my county is: " + CountyName);
            //ReportLibrary2.Report3.ResetDefaults();
            //ReportLibrary2.Report3.ChangeSqlString("Initial Catalog=" + CountyName + ";Data Source=192.168.104.202;User ID=developer;Password=aociris;");
            //ReportLibrary2.Report3.CountyName = myCountyList.SelectedCountyText;
            //ReportLibrary2.Report3.SortOptions("=Fields." + SortField, SortDirection.Asc, myCountyList.SortCounty);
            //ReportLibrary2.Report3.SQLCommandString = SQLCommandThingy;
            //Debug.WriteLine("garbnage " + StartDate + " " + EndDate);
            //ReportLibrary2.Report3.AddDateFilter(StartDate, EndDate);
            //for (int i = 0; i < myDataFields.Count(); i++)
            //{
            //    ReportLibrary2.Report3.GenerateTextField(myDataFields[i]);
            //    ReportLibrary2.Report3.GenerateDataField("=Fields." + myDataFields[i]);
            //}
            //var sqlGen = new SqlGenerator(SqlGenerator.SqlTypes.Select, "Activity");
            ////  var uriReportSource = new UriReportSource();
            //// sqlGen.SelectStatementLimit = 100;
            ////Adocls.ExecuteSql(sqlGen);
            ////var db = Adocls.FetchDataSet(sqlGen);
            //var gen2 = ModelBase.LoadModel<ActivityModel>(sqlGen);
            ////  Debug.WriteLine("Last Key " + Adocls.FetchDataSet(sqlGen).DataSetName );
            //// return gen2.ToList<ActivityModel>();
            //var json = new JavaScriptSerializer().Serialize(gen2);
            //// ReportLibrary2.Report3.GetJSON(json);
            //Debug.WriteLine("JSON " + json.ToString());
            //var res = new XmlResult(json);
            // SerializeXml(gen2.ToList<ActivityModel>());
            // Debug.WriteLine("XML OBJECT: "+res.ObjectToSerialize.ToString());
            //return View(null, null, json);
            return View();
        }

    }
}
