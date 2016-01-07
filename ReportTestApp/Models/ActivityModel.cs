using IrisWeb.Code.Data.Attributes;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IrisWeb.Code.Data.Models.Database
{
    // TODO: I am not convinced this model is needed. We should take a look at other ways to pass this data
    public class ActivityDropDown
    {
        [Key]
        [ScaffoldColumn(false)]
        public string Field1 { get; set; }

        [OrderByFieldAttribute(true)]
        [MaxLength(70)]
        [Display(Name = "Name")]
        public string Field2 { get; set; }
    }

    [IrisGrid("~API/Activity/Read", "~API/Activity/Create", "~API/Activity/Update", "~API/Activity/Destroy")]
    public class ActivityModel
    {
        [Key]
        [Required]
        [HiddenInput]
        [Display(Name = "Activity Key")]
        public string Activity_Key { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(10)]
        [Display(Name = "Activity Name")]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }

        [ReadOnly(true)]
        public string NameDesc { get; set; }

        [ReadOnly(true)]
        public string DescName { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [Range(typeof(decimal), "1", "100000")]
        [Display(Name = "Performance Standard")]
        public decimal Perform_Standard { get; set; }

        [MaxLength(50)]
        [Display(Name = "Work Units")]
        public string Work_Unit { get; set; }

        [MaxLength(10)]
        [Display(Name = "Worker's Comp Code")]
        public string WorkComp_Key { get; set; }

        [MaxLength(10)]
        [Display(Name = "UOM")]
        public string UOM_Key { get; set; }

        [MaxLength(2048)]
        [Display(Name = "Work Methods")]
        public string Work_Methods { get; set; }

        [MaxLength(2048)]
        [Display(Name = "Inspection Instructions")]
        public string Inspection { get; set; }

        [MaxLength(30)]
        [Display(Name = "Arthorized By")]
        public string Authorize { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; }

        [MaxLength(100)]
        [Display(Name = "User 1")]
        public string User1 { get; set; }

        [MaxLength(100)]
        [Display(Name = "User 2")]
        public string User2 { get; set; }

        [MaxLength(100)]
        [Display(Name = "User 3")]
        public string User3 { get; set; }

        [MaxLength(100)]
        [Display(Name = "User 4")]
        public string User4 { get; set; }

        [MaxLength(100)]
        [Display(Name = "User 5")]
        public string User5 { get; set; }

        [MaxLength(100)]
        [Display(Name = "User 6")]
        public string User6 { get; set; }

        [MaxLength(100)]
        [Display(Name = "User 7")]
        public string User7 { get; set; }

        [MaxLength(100)]
        [Display(Name = "User 8")]
        public string User8 { get; set; }

        [MaxLength(100)]
        [Display(Name = "User 9")]
        public string User9 { get; set; }

        [MaxLength(100)]
        [Display(Name = "User 10")]
        public string User10 { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Create Date")]
        [ReadOnly(true)]
        public Nullable<DateTime> CreateDate { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Modified")]
        public Nullable<DateTime> DateStamp { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [Display(Name = "IRIS User")]
        public string SecurityUser_Key { get; set; }
    }
}
