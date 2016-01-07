using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Testing.Models;

namespace ReportTestApp.TreeViewScripts
{
    public class ActivityTree
    {
        public List<TestModel> ActivityData()
        {
            List<TestModel> people = new List<TestModel>{
                   new TestModel{id = "*", text = "Activity", items = new List<TestModel> {
                       new TestModel { id =  "Activity_Key", text= "Activity Key", table="Activity" },
                       new TestModel { id = "Name", text= "Activity Name", table="Activity" },
                       new TestModel { id = "Description", text= "Description", table="Activity" },
                       new TestModel { id = "NameDesc", text= "Name Description", table="Activity" },
                       new TestModel { id = "DescName", text= "Description Name", table="Activity" },
                       new TestModel { id = "Perform_Standard", text= "Performance Standard", table="Activity" },
                       new TestModel { id = "Work_Unit", text= "Work Unit", table="Activity" },
                       new TestModel { id = "WorkComp_Key", text= "WorkComp Key", table="Activity" },
                       new TestModel { id = "UOM_Key", text= "UOM", table="Activity" },
                       new TestModel { id = "Work_Methods", text= "Method", table="Activity" },
                       new TestModel { id = "Inspection", text= "Inspection", table="Activity" },
                       new TestModel { id = "Authorize", text= "Authorized By", table="Activity" },
                       new TestModel { id = "Active", text= "Active?", table="Activity" },
                       new TestModel { id = "User1", text= "User 1", table="Activity" },
                       new TestModel { id = "User2", text= "User 2", table="Activity" },
                       new TestModel { id = "User3", text= "User 3", table="Activity" },
                       new TestModel { id = "User4", text= "User 4", table="Activity" },
                       new TestModel { id = "User5", text= "User 5", table="Activity" },
                       new TestModel { id = "User6", text= "User 6", table="Activity" },
                       new TestModel { id = "User7", text= "User 7", table="Activity" },
                       new TestModel { id = "User8", text= "User 8", table="Activity" },
                       new TestModel { id = "User9", text= "User 9", table="Activity" },
                       new TestModel { id = "User10", text= "User 10", table="Activity" },
                       new TestModel { id = "CreateDate", text= "Date Created", table="Activity" },
                       new TestModel { id = "DateStamp", text= "Last Accessed", table="Activity" },
                       new TestModel { id = "SecurityUser_Key", text= "User Security Key", table="Activity" }
                   }}
                   };
            return people;

        }

        public List<TestModel> TransactData()
        {
            List<TestModel> people = new List<TestModel>{
                   new TestModel{id = "*", text = "Transact", items = new List<TestModel> {
                       new TestModel { id = "Transact_Key", text= "Transact Key", table="Transact" },
                       new TestModel { id = "Batch_Num", text= "Batch Number", table="Transact" },
                       new TestModel { id = "Task_Date", text= "Task Date", table="Transact" },
                       new TestModel { id = "Crew_Num", text= "Crew Number", table="Transact" },
                       new TestModel { id = "Fiscal_Key", text= "Fiscal Name", table="Transact" },
                       new TestModel { id = "*", text= "Activity", items = new List<TestModel> {
                           new TestModel { id = "Activity_Key", text= "Activity Key", table="Activity" },
                           new TestModel { id = "Name", text= "Activity Name", table="Activity" },
                           new TestModel { id = "Description", text= "Description", table="Activity" },
                           new TestModel { id = "NameDesc", text= "Name Description", table="Activity" },
                           new TestModel { id = "DescName", text= "Description Name", table="Activity" },
                           new TestModel { id = "Perform_Standard", text= "Performance Standard", table="Activity" },
                           new TestModel { id = "Work_Unit", text= "Work Unit", table="Activity" },
                           new TestModel { id = "WorkComp_Key", text= "WorkComp Key", table="Activity" },
                           new TestModel { id = "UOM_Key", text= "UOM", table="Activity" },
                           new TestModel { id = "Work_Methods", text= "Method", table="Activity" },
                           new TestModel { id = "Inspection", text= "Inspection", table="Activity" },
                           new TestModel { id = "Authorize", text= "Authorized By", table="Activity" },
                           new TestModel { id = "Active", text= "Active?", table="Activity" },
                           new TestModel { id = "User1", text= "User 1", table="Activity" },
                           new TestModel { id = "User2", text= "User 2", table="Activity" },
                           new TestModel { id = "User3", text= "User 3", table="Activity" },
                           new TestModel { id = "User4", text= "User 4", table="Activity" },
                           new TestModel { id = "User5", text= "User 5", table="Activity" },
                           new TestModel { id = "User6", text= "User 6", table="Activity" },
                           new TestModel { id = "User7", text= "User 7", table="Activity" },
                           new TestModel { id = "User8", text= "User 8", table="Activity" },
                           new TestModel { id = "User9", text= "User 9", table="Activity" },
                           new TestModel { id = "User10", text= "User 10", table="Activity" },
                           new TestModel { id = "CreateDate", text= "Date Created", table="Activity" },
                           new TestModel { id = "DateStamp", text= "Last Accessed", table="Activity" },
                           new TestModel { id = "SecurityUser_Key", text= "User Security Key", table="Activity" }
                       }
                       },
                       new TestModel { id = "Production", text= "Production", table="Transact" },
                       new TestModel { id = "Employee_Key", text= "Employee Name", table="Transact" },
                       new TestModel { id = "Equipment_Key", text= "Equipment Name", table="Transact" },
                       new TestModel { id = "ResourceClass_Key", text= "Resource Class Name", table="Transact" },
                       new TestModel { id = "Resource_Type_Key", text= "Resource Type Name", table="Transact" },
                       new TestModel { id = "Quantity", text= "Quantity", table="Transact" },
                       new TestModel { id = "UOM_Key", text= "UOM Name", table="Transact" },
                       new TestModel { id = "Pay_Type_Key", text= "Pay Type", table="Transact" },
                       new TestModel { id = "Premium_Key", text= "Premium", table="Transact" },
                       new TestModel { id = "Class_Rate", text= "Class Rate", table="Transact" },
                       new TestModel { id = "Bill_Rate", text= "Bill Rate", table="Transact" },
                       new TestModel { id = "Resource_Rate", text= "Resource Rate", table="Transact" },
                       new TestModel { id = "Overridden_Labor_Rate", text= "Override Labor", table="Transact" },
                       new TestModel { id = "Employee_Rate", text= "Employee Rate", table="Transact" },
                       new TestModel { id = "Fringe_Rate", text= "Fringe Rate", table="Transact" },
                       new TestModel { id = "OH_Rate", text= "OH Rate", table="Transact" },
                       new TestModel { id = "Other_Rate", text= "Other Rate", table="Transact" },
                       new TestModel { id = "Pay_Factor", text= "Pay Factor", table="Transact" },
                       new TestModel { id = "Premium_Amount", text= "Premium Amount", table="Transact" },
                       new TestModel { id = "Premium_Percentage", text= "Premium Percentage", table="Transact" },
                       new TestModel { id = "Description", text= "Description", table="Transact" },
                       new TestModel { id = "Inventory_Location_Key", text= "Inventory Location", table="Transact" },
                       new TestModel { id = "Mgt_Unit_Key", text= "Management Unit", table="Transact" },
                       new TestModel { id = "Program_Key", text= "Program", table="Transact" },
                       new TestModel { id = "Zone_Key", text= "Zone", table="Transact" },
                       new TestModel { id = "Project_Key", text= "Project", table="Transact" },
                       new TestModel { id = "RBF_Key", text= "RBF", table="Transact" },
                       new TestModel { id = "Road_Key", text= "Road", table="Transact" },
                       new TestModel { id = "RoadName_Key", text= "RoadName", table="Transact" },
                       new TestModel { id = "Beg_Point", text= "Begin Point", table="Transact" },
                       new TestModel { id = "End_Point", text= "End Point", table="Transact" },
                       new TestModel { id = "FromLocation", text= "From Location", table="Transact" },
                       new TestModel { id = "ToLocation", text= "To Location", table="Transact" },
                       new TestModel { id = "Reason_Key", text= "Reason", table="Transact" },
                       new TestModel { id = "SRSRequestNumber", text= "SRS Request Number", table="Transact" },
                       new TestModel { id = "APSTransact_Key", text= "APS Transact Key", table="Transact" },
                       new TestModel { id = "PostedFromAPS", text= "Posted From APS", table="Transact" },
                       new TestModel { id = "Comments", text= "Comments", table="Transact" },
                       new TestModel { id = "ExportMMS", text= "Export MMS", table="Transact" },
                       new TestModel { id = "ExportARS", text= "Export ARS", table="Transact" },
                       new TestModel { id = "ExportARSDeleted", text= "Export ARS Deleted", table="Transact" },
                       new TestModel { id = "FuelImport", text= "Fuel Import", table="Transact" },
                       new TestModel { id = "User1", text= "User 1", table="Transact" },
                       new TestModel { id = "User2", text= "User 2", table="Transact" },
                       new TestModel { id = "User3", text= "User 3", table="Transact" },
                       new TestModel { id = "User4", text= "User 4", table="Transact" },
                       new TestModel { id = "User5", text= "User 5", table="Transact" },
                       new TestModel { id = "User6", text= "User 6", table="Transact" },
                       new TestModel { id = "User7", text= "User 7", table="Transact" },
                       new TestModel { id = "User8", text= "User 8", table="Transact" },
                       new TestModel { id = "User9", text= "User 9", table="Transact" },
                       new TestModel { id = "User10", text= "User 10", table="Transact" },
                       new TestModel { id = "OHCost", text= "OH Cost", table="Transact" },
                       new TestModel { id = "OH_Calc_Method", text= "OH Calc Method", table="Transact" },
                       new TestModel { id = "FixedEmployeeRate", text= "Fixed Employee Rate", table="Transact" },
                       new TestModel { id = "EmployeeHourlyRateWithPremium", text= "Employee Rate w/ Premium", table="Transact" },
                       new TestModel { id = "EmployeeHourlyRateWithPremiumPayFactor", text= "Pay Factor For Employee Rate w/ Premium", table="Transact" },
                       new TestModel { id = "PemiumHourlyRate", text= "Premium Hourly Rate", table="Transact" },
                       new TestModel { id = "TotalClassCost", text= "Total Class Cost", table="Transact" },
                       new TestModel { id = "TotalResourceCost", text= "Total Resource Cost", table="Transact" },
                       new TestModel { id = "TotalBillCost", text= "Total Bill Cost", table="Transact" },
                       new TestModel { id = "TotalLaborWithFringeOH", text= "Total Labor w/ Fringe OH", table="Transact" },
                       new TestModel { id = "TotalLaborWithoutFringeOH", text= "Total Labor w/o Fringe OH", table="Transact" },
                       new TestModel { id = "TotalLaborWithoutFringeOHOther", text= "Total Labor w/o Fringe OH Other", table="Transact" },
                       new TestModel { id = "TotalLaborWithoutOH", text= "Total Labor Without OH", table="Transact" },
                       new TestModel { id = "TotalOHWithFringe", text= "Total OH w/ Fringe", table="Transact" },
                       new TestModel { id = "TotalOHWithoutFringe", text= "Total OH w/o Fringe", table="Transact" },
                       new TestModel { id = "RecordID", text= "Record ID", table="Transact" },
                       new TestModel { id = "TimecardDateStamp", text= "Timecard Date", table="Transact" },
                       new TestModel { id = "CreateDate", text= "Create Date", table="Transact" },
                       new TestModel { id = "DateStamp", text= "Date Stamp", table="Transact" },
                       new TestModel { id = "SecurityUser_Key", text= "Security User", table="Transact" },
                       new TestModel { id = "TotalFringe", text= "Total Fringe", table="Transact" },
                       new TestModel { id = "ProjectSub_Key", text= "Project SubKey", table="Transact" },
                       new TestModel { id = "CreatedBySecurityUser_Key", text= "Created By Security", table="Transact" },
                       new TestModel { id = "PostedBySecurityUser_Key", text= "Posted By Security", table="Transact" },
                       new TestModel { id = "PostedDate", text= "Posted Date", table="Transact" },
                       new TestModel { id = "TotalLaborWithoutOHOther", text= "Total Labor w/o OH Other", table="Transact" },
                       new TestModel { id = "Vendor_Key", text= "Vender", table="Transact" },
                       new TestModel { id = "Timecard_Key", text= "Timecard", table="Transact" }
                   }}
                   };
            return people;
        }

    }
}