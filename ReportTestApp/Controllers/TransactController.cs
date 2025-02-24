﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IrisWeb;
using IrisWeb.Code.Data.Models.Database;
using IrisWeb.Code.Data.Filters;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using ExtensionMethods;
using ReportTestApp.Models;
using Kendo.Mvc.UI;
using IrisWeb.Reports.Model;



namespace ReportTestApp.Controllers
{
       public class TransactController : Controller
       {
              public static ReportModel ReportObject = new ReportModel();
              // GET: Transact
              public ActionResult Transact()
              {
                     //var filter = new SqlGenerator(SqlGenerator.SqlTypes.Select, "Transact");
                     // filter.SelectStatementLimit = 200;
                     //filter.AddWhereParameter(SqlWhereAndorOptions.SqlWhereAndor.OR, "TotalResourceCost", 51.00, 100.00, SqlWhereComparison.SqlComparer.Between);
                     //filter.AddWhereParameter(SqlWhereAndorOptions.SqlWhereAndor.And, "Task_Date", "05/13/2015", "10/05/2015", SqlWhereComparison.SqlComparer.Between);
                     //filter.AddWhereParameter(SqlWhereAndorOptions.SqlWhereAndor.And, "Task_Date", "01/20/2014", "02/01/2014", SqlWhereComparison.SqlComparer.Between);
                     // var baseModel = ModelBase.LoadModel<TransactModel>();

                     var filter = new SqlGenerator(SqlGenerator.SqlTypes.Select, "Transact");
                     filter.SelectStatementLimit = 40;
                     var baseModel = ModelBase.LoadModel<TransactModel>(filter);
                     return View(baseModel.ToList());

                     //var mysql = GetSqlString();

                     //return View(mysql);



                     ////return View(baseModel.ToList<TransactModel>());
                     //return View();

              }


              [HttpGet]
              public List<TransactModel> GetSqlString()
              {
                     //var sqlGen = new SqlGenerator(SqlGenerator.SqlTypes.Select, "Activity");
                     //sqlGen.SelectStatementLimit = 1;
                     //var data = ModelBase.LoadModel<ActivityModel>(sqlGen);
                     // return Json(data, JsonRequestBehavior.AllowGet);
                     //Adocls.AddCountySpecificConnectionString("Z_Linn");

                     // var sqlGen = new SqlGenerator(SqlGenerator.SqlTypes.Select, "Activity");
                     // sqlGen.SelectStatementLimit = 10;
                     //Adocls.ExecuteSql(sqlGen);
                     // var db = Adocls.FetchDataSet(sqlGen);
                     var gen2 = ModelBase.LoadModel<TransactModel>();
                     // ReportLibrary2.Report2.ChangeSqlString("Data Source=devServer;Initial Catalog=Z_Yamhill;User ID=sa;Password=data22");
                     //Debug.WriteLine("Using this mode? " + data.ToJSON());
                     return gen2.ToList();
              }

              [HttpGet]
              public ActionResult Employee_Read()
              {

                     Debug.WriteLine("Called 1");
                     var result = ModelBase.LoadModel<TransactModel>();
                     Debug.WriteLine(result.ToJSON());
                     return Json(result, JsonRequestBehavior.AllowGet);
              }

              [HttpPost]
              public List<TransactModel> Employee_Read2()
              {
                     Debug.WriteLine("Called 2");
                     var result = ModelBase.LoadModel<TransactModel>();
                     Debug.WriteLine(result.ToJSON());
                     return result.ToList();
              }

              [HttpGet]
              public string GetGridData(string columnData, string columnName, string groupData, string filterData, string countFields, string aggregateType)
              {
                     Debug.WriteLine("YO: " + columnData + " | " + groupData);
                     Debug.WriteLine("Filter: " + filterData);
                     var columns = StringHelper(columnData);
                     var columnNames = StringHelper(columnName);
                     var groups = StringHelper(groupData);
                     var groupNames = StringHelper(groupData);
                     var filters = FilterHelper(filterData);
                     var count = FieldCount(countFields);
                     var average = FieldCount(aggregateType);

                     for (int i = 0; i < groups.Length; i++)
                     {
                            for (int k = 0; k < columns.Length; k++)
                            {
                                   if (groups[i] == columns[k])
                                   {
                                          groupNames[i] = columnNames[k];
                                   }
                            }
                     }

                     ReportObject.ReportName = "Test Report";
                     ReportObject.ConnectionString = "Initial Catalog=A_Wallowa9;Data Source=10.0.0.40;User ID=developer;Password=aociris;";
                     /*"Initial Catalog=Jefferson;Data Source=localhost;User ID=developer;Password=aociris;";*/
                     ReportObject.SelectCommand = "SELECT TOP 200 * FROM Transact";
                     ReportObject.GenerateDataField = columns.ToList();
                     ReportObject.GenerateTitleField = columnNames.ToList();                    
                     ReportObject.GroupBy = groups.ToList();
                     ReportObject.GroupName = groupNames.ToList();
                     ReportObject.Filters = filters.ToList();
                     ReportObject.SumOrCount = count.ToList();
                     ReportObject.AggregateType = average.ToList(); //Figure out a new name instead of an actual name dumb ass!!!!!
                     ReportObject.AddReportFooterSection = true;
                     dynamic collectionWrapper = new
                     {

                            myRoot = ReportObject

                     };
                     var RawData = JsonConvert.SerializeObject(collectionWrapper); //Convert Object to something usable by the report
                                                                                   // Debug.WriteLine("YO2: " + ReportObject.GenerateTitleField[0] + " | " + ReportObject.GroupBy[0]);
                                                                                   // Debug.WriteLine("Raw Data: " + RawData);
                     ReportLibrary2.Report3.GetJSON(RawData);
                     //JavaScriptSerializer serializer = new JavaScriptSerializer();
                     //Debug.WriteLine("serial: "+serializer.Serialize(columnData));
                     return "Done";
              }

              private string[] StringHelper(string obj)
              {
                     var arrayNull = new string[0];
                     if (obj.Length > 2)
                     {
                            obj = obj.Substring(1, obj.Length - 2);
                            // Debug.WriteLine("First Pass: " + obj);
                            var array = obj.Split(',');
                            for (int k = 0; k < array.Length; k++)
                            {
                                   array[k] = array[k].Substring(1, array[k].Length - 2);
                            }
                            return array;
                     }

                     return arrayNull;
              }

              private string[] FilterHelper(string obj)
              {
                     var arrayNull = new string[0];
                     if (obj.Length > 2)
                     {
                            obj = obj.Substring(1, obj.Length - 2);
                            var array = obj.Split(',');
                            var fullFilter = new string[array.Length / 3];
                            var count = 0;
                            for (int i = 0; i < array.Length; i += 3)
                            {
                                   fullFilter[count] = array[i] + "|" + array[i + 1] + "|" + array[i + 2];
                                   count++;
                            }
                            Debug.WriteLine(fullFilter[0]);
                            return fullFilter;
                     }
                     return arrayNull;
              }

              private string[] FieldCount(string obj)
              {
                     var arrayNull = new string[0];
                     if (obj.Length > 2)
                     {
                            obj = obj.Substring(1, obj.Length - 2);
                            var array = obj.Split(',');

                            Debug.WriteLine(array[0]);
                            return array;
                     }
                     return arrayNull;
              }

       }
}
