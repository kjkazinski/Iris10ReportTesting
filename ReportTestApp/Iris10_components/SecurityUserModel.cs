using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IrisWeb.Code.Data.Models.Database
{
    // TODO: I am not convinced this model is needed. We should take a look at other ways to pass this data
    public class SecurityUserDropDown
    {
        [Key]
        [HiddenInput]
        [ScaffoldColumn(false)]
        public string SecurityUser_Key { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(100)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
    }

    public class SecurityUserModel
    {
        [Key]
        [HiddenInput]
        [ScaffoldColumn(false)]
        public string SecurityUser_Key { get; set; }

        [OrderByFieldAttribute(true)]
        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(100)]
        [Display(Name = "Login Name")]
        public string LoginName { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(100)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        public string Employee_Key { get; set; }
        public string NetPassword { get; set; }

        [EmailAddress]
        [MaxLength(128)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        public bool Active { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Create Date")]
        [ReadOnly(true)]
        public Nullable<DateTime> CreateDate { get; set; }
        [Required(ErrorMessage = "Your {0} is required.")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Modified")]
        public Nullable<DateTime> DateStamp { get; set; }

        [Display(Name = "Entered By")]
        public string Entered_By { get; set; }
    }
}
