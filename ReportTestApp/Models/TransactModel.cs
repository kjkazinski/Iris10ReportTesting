using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IrisWeb.Code.Data.Models.Database
{
    public class TransactModel
    {
        [Key]
        [ScaffoldColumn(false)]
        public string Transact_Key { get; set; }

        [ReadOnly(true)]
        [MaxLength(10)]
        [Display(Name = "Batch #")]
        public string Batch_Num { get; set; }

        [ReadOnly(true)]
        [Required(ErrorMessage = "Your {0} is required.")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Task Date")]
        public DateTime Task_Date { get; set; }

        [ReadOnly(true)]
        [MaxLength(8)]
        [Display(Name = "Crew #")]
        public string Crew_Num { get; set; }

        [ReadOnly(true)]
        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(10)]
        [Display(Name = "Fiscal Year")]
        public string FiscalName { get; set; }

        [ReadOnly(true)]
        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(10)]
        [Display(Name = "Activity")]
        public string ActivityName { get; set; }

        [ReadOnly(true)]
        [DataType("Decimal")]
        [Range(0, 5)]
        public Nullable<decimal> Production { get; set; }

        [ReadOnly(true)]
        [MaxLength(10)]
        [Display(Name = "Employee")]
        public string EmployeeName { get; set; }

        [ReadOnly(true)]
        [MaxLength(10)]
        [Display(Name = "Equipment")]
        public string Equipment_Key { get; set; }

        [ReadOnly(true)]
        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(10)]
        [Display(Name = "Resource Class")]
        public string ResourceClassName { get; set; }

        [ReadOnly(true)]
        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(10)]
        [Display(Name = "Resource Type")]
        public string ResourceTypeName { get; set; }

        [ReadOnly(true)]
        [DataType("Decimal")]
        public Nullable<decimal> Quantity { get; set; }

        [ReadOnly(true)]
        [MaxLength(10)]
        [Display(Name = "UOM")]
        public string UOMName { get; set; }

        [ReadOnly(true)]
        [MaxLength(10)]
        [Display(Name = "Pay Type")]
        public string PayTypeName { get; set; }

        [ReadOnly(true)]
        [MaxLength(10)]
        [Display(Name = "Premium")]
        public string PremiumName { get; set; }

        [ReadOnly(true)]
        [DataType("Currency")]
        [UIHint("Currency")]
        [Display(Name = "Class Rate")]
        public Nullable<decimal> Class_Rate { get; set; }

        [ReadOnly(true)]
        [DataType("Currency")]
        [UIHint("Currency")]
        [Display(Name = "Bill Rate")]
        public Nullable<decimal> Bill_Rate { get; set; }

        [ReadOnly(true)]
        [DataType("Currency")]
        [UIHint("Currency")]
        [Display(Name = "Resource Rate")]
        public Nullable<decimal> Resource_Rate { get; set; }

        [ReadOnly(true)]
        [Display(Name = "Overriden Labor Rate")]
        [DataType("Boolean")]
        public bool Overridden_Labor_Rate { get; set; }

        [ReadOnly(true)]
        [DataType("Currency")]
        [UIHint("Currency")]
        [Display(Name = "Employee Rate")]
        public Nullable<decimal> Employee_Rate { get; set; }

        [ReadOnly(true)]
        [DataType("Currency")]
        [UIHint("Currency")]
        [Display(Name = "Fringe Rate")]
        public Nullable<decimal> Fringe_Rate { get; set; }

        [ReadOnly(true)]
        [DataType("Currency")]
        [UIHint("Currency")]
        [Display(Name = "OH Rate")]
        public Nullable<decimal> OH_Rate { get; set; }

        [ReadOnly(true)]
        [DataType("Currency")]
        [UIHint("Currency")]
        [Display(Name = "Other Rate")]
        public Nullable<decimal> Other_Rate { get; set; }

        [ReadOnly(true)]
        [DataType("Decimal")]
        [Display(Name = "Pay Factor")]
        public Nullable<decimal> Pay_Factor { get; set; }


        [ReadOnly(true)]
        [DataType("Currency")]
        [UIHint("Currency")]
        [Display(Name = "Premium Amount")]
        public Nullable<decimal> Premium_Amount { get; set; }

        [ReadOnly(true)]
        [DataType("Decimal")]
        [Display(Name = "Premium %")]
        public Nullable<decimal> Premium_Percentage { get; set; }

        [ReadOnly(true)]
        [MaxLength(256)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [ReadOnly(true)]
        [MaxLength(10)]
        [Display(Name = "Inventory")]
        public string InventoryName { get; set; }

        [ReadOnly(true)]
        [MaxLength(10)]
        [Display(Name = "Mgt. Unit")]
        public string MgtUnitName { get; set; }

        [ReadOnly(true)]
        [MaxLength(10)]
        [Display(Name = "Program")]
        public string ProgramName { get; set; }

        [ReadOnly(true)]
        [MaxLength(10)]
        [Display(Name = "Zone")]
        public string ZoneName { get; set; }

        [ReadOnly(true)]
        [MaxLength(10)]
        [Display(Name = "Project")]
        public string ProjectName { get; set; }

        [ReadOnly(true)]
        [MaxLength(10)]
        [Display(Name = "Bridge/Facility")]
        public string RBFName { get; set; }

        [ReadOnly(true)]
        [MaxLength(10)]
        [Display(Name = "Road")]
        public string RoadName { get; set; }

        [ReadOnly(true)]
        [MaxLength(10)]
        [Display(Name = "Road Name")]
        public string RoadNameName { get; set; }

        [ReadOnly(true)]
        [DataType("Decimal")]
        [Display(Name = "Begin Point")]
        public Nullable<decimal> Beg_Point { get; set; }

        [ReadOnly(true)]
        [DataType("Decimal")]
        [Display(Name = "End Point")]
        public Nullable<decimal> End_Point { get; set; }

        [ReadOnly(true)]
        [MaxLength(10)]
        [Display(Name = "From Location")]
        public string FromLocation { get; set; }


        [ReadOnly(true)]
        [MaxLength(10)]
        [Display(Name = "To Location")]
        public string ToLocation { get; set; }

        [ReadOnly(true)]
        [MaxLength(10)]
        [Display(Name = "Reason")]
        public string ReasonName { get; set; }

        [ReadOnly(true)]
        [MaxLength(10)]
        [Display(Name = "Service Request")]
        public string SRSRequestNumber { get; set; }

        [ReadOnly(true)]
        [MaxLength(10)]
        public string APSTransact_Key { get; set; }

        [ReadOnly(true)]
        [DataType("Boolean")]
        [Display(Name = "Post From APS")]
        public bool PostedFromAPS { get; set; }

        [ReadOnly(true)]
        [MaxLength(256)]
        [Display(Name = "Comments")]
        public string Comments { get; set; }

        [ReadOnly(true)]
        [DataType("Boolean")]
        [Display(Name = "Export To MMS")]
        public bool ExportMMS { get; set; }

        [ReadOnly(true)]
        [DataType("Boolean")]
        [Display(Name = "Export To ARS")]
        public bool ExportARS { get; set; }

        [ReadOnly(true)]
        [DataType("Boolean")]
        [Display(Name = "Export To ARS Deleted")]
        public bool ExportARSDeleted { get; set; }

        [ReadOnly(true)]
        [DataType("Boolean")]
        [Display(Name = "Fuel Import")]
        public bool FuelImport { get; set; }

        [ReadOnly(true)]
        [Display(Name = "User 1")]
        public string User1 { get; set; }

        [ReadOnly(true)]
        [Display(Name = "User 2")]
        public string User2 { get; set; }

        [ReadOnly(true)]
        [Display(Name = "User 3")]
        public string User3 { get; set; }

        [ReadOnly(true)]
        [Display(Name = "User 4")]
        public string User4 { get; set; }

        [ReadOnly(true)]
        [Display(Name = "User 5")]
        public string User5 { get; set; }

        [ReadOnly(true)]
        [Display(Name = "User 6")]
        public string User6 { get; set; }

        [ReadOnly(true)]
        [Display(Name = "User 7")]
        public string User7 { get; set; }

        [ReadOnly(true)]
        [Display(Name = "User 8")]
        public string User8 { get; set; }

        [ReadOnly(true)]
        [Display(Name = "User 9")]
        public string User9 { get; set; }

        [ReadOnly(true)]
        [Display(Name = "User 10")]
        public string User10 { get; set; }

        [ReadOnly(true)]
        [DataType("Currency")]
        [UIHint("Currency")]
        [Display(Name = "End Point")]
        public decimal OHCost { get; set; }

        [ReadOnly(true)]
        [Display(Name = "OH Calc Method")]
        public Byte OH_Calc_Method { get; set; }

        [ReadOnly(true)]
        [DataType("Boolean")]
        [Display(Name = "Fixed Employee Rate")]
        public bool FixedEmployeeRate { get; set; }

        [ReadOnly(true)]
        [DataType("Currency")]
        [UIHint("Currency")]
        [Display(Name = "Employee Rate w/Premium")]
        public Nullable<decimal> EmployeeHourlyRateWithPremium { get; set; }

        [ReadOnly(true)]
        [DataType("Currency")]
        [UIHint("Currency")]
        [Display(Name = "Employee Rate w/Premium & Pay Factor")]
        public Nullable<decimal> EmployeeHourlyRateWithPremiumPayFactor { get; set; }

        [ReadOnly(true)]
        [DataType("Currency")]
        [UIHint("Currency")]
        [Display(Name = "Premium Hourly Rate")]
        public Nullable<decimal> PemiumHourlyRate { get; set; }

        [ReadOnly(true)]
        [DataType("Currency")]
        [UIHint("Currency")]
        [Display(Name = "Total Class Cost")]
        public Nullable<decimal> TotalClassCost { get; set; }

        [ReadOnly(true)]
        [DataType("Currency")]
        [UIHint("Currency")]
        [Display(Name = "Total Resource Cost")]
        public Nullable<decimal> TotalResourceCost { get; set; }

        [ReadOnly(true)]
        [DataType("Currency")]
        [UIHint("Currency")]
        [Display(Name = "Total Bill Cost")]
        public Nullable<decimal> TotalBillCost { get; set; }

        [ReadOnly(true)]
        [DataType("Currency")]
        [UIHint("Currency")]
        [Display(Name = "Total Labor w/Fringe & OH")]
        public Nullable<decimal> TotalLaborWithFringeOH { get; set; }

        [ReadOnly(true)]
        [DataType("Currency")]
        [UIHint("Currency")]
        [Display(Name = "Total Labor wo/Fringe & OH")]
        public Nullable<decimal> TotalLaborWithoutFringeOH { get; set; }

        [ReadOnly(true)]
        [DataType("Currency")]
        [UIHint("Currency")]
        [Display(Name = "Total Labor wo/Fringe, OH and Other")]
        public Nullable<decimal> TotalLaborWithoutFringeOHOther { get; set; }

        [ReadOnly(true)]
        [DataType("Currency")]
        [UIHint("Currency")]
        [Display(Name = "Total Labor wo/OH")]
        public Nullable<decimal> TotalLaborWithoutOH { get; set; }

        [ReadOnly(true)]
        [DataType("Currency")]
        [UIHint("Currency")]
        [Display(Name = "Total OH w/Fringe")]
        public Nullable<decimal> TotalOHWithFringe { get; set; }

        [ReadOnly(true)]
        [DataType("Currency")]
        [UIHint("Currency")]
        [Display(Name = "Total OH wo/Fringe")]
        public Nullable<decimal> TotalOHWithoutFringe { get; set; }

        [ReadOnly(true)]
        [Display(Name = "Record ID")]
        public string RecordID { get; set; }

        [ReadOnly(true)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Timecard Date")]
        public Nullable<DateTime> TimecardDateStamp { get; set; }

        [ReadOnly(true)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Create Date")]
        public Nullable<DateTime> CreateDate { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Modified")]
        public Nullable<DateTime> DateStamp { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [Display(Name = "IRIS User")]
        public string SecurityUserName { get; set; }

        [ReadOnly(true)]
        [DataType("Currency")]
        [UIHint("Currency")]
        [Display(Name = "Total Fringe")]
        public Nullable<decimal> TotalFringe { get; set; }

        [ReadOnly(true)]
        [MaxLength(10)]
        [Display(Name = "Sub Project")]
        public string ProjectSubName { get; set; }

        [Display(Name = "Created User")]
        [ReadOnly(true)]
        public string CreateSecurityUserName { get; set; }

        [Display(Name = "Posted User")]
        [ReadOnly(true)]
        public string PostedBySecurityUserName { get; set; }

        [ReadOnly(true)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Posted")]
        public Nullable<DateTime> PostedDate { get; set; }

        [ReadOnly(true)]
        [DataType("Currency")]
        [UIHint("Currency")]
        [Display(Name = "Total Labor wo/OH & Other")]
        public Nullable<decimal> TotalLaborWithoutOHOther { get; set; }

        [ReadOnly(true)]
        [MaxLength(10)]
        [Display(Name = "Vendor")]
        public string VendorName { get; set; }

    }
}