using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using IrisWeb.Code.Data.Attributes;

namespace IrisWeb.Code.Data.Models.Database
{
    [ModelDataBindings(DatabaseName = "UserDatabase", TableName = "[54.244.237.117].IRISServices.dbo.Users", KeyFieldName = "Users_Key")]
    public class UsersModel
    {
        [Key]
        [HiddenInput]
        [ScaffoldColumn(false)]
        public string Users_Key { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(100)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [MaxLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(50)]
        [Display(Name = "Password")]
        public string UserPassword { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(50)]
        [Display(Name = "Domain")]
        public string Domain { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(10)]
        [Display(Name = "County Key")]
        public string County_Key { get; set; }

        //[Required(ErrorMessage = "Your {0} is required.")]
        //[MaxLength(50)]
        //[Display(Name = "County")]
        //public string County { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(50)]
        [Display(Name = "Database Name")]
        public string DatabaseName { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(50)]
        [Display(Name = "Server Name")]
        public string ServerName { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(50)]
        [Display(Name = "Primary IP")]
        public string PrimaryIP { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(50)]
        [Display(Name = "Backup IP")]
        public string BackupIP { get; set; }

        //[Display(Name = "Client Version")]
        //public double ClientVersion { get; set; }

        [ReadOnly(true)]
        [Display(Name = "Last Login Version")]
        public double LastLoginVersion { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(500)]
        [Display(Name = "IRIS Path")]
        public string IrisPath { get; set; }

        [ReadOnly(true)]
        [MaxLength(1024)]
        [Display(Name = "System User Name")]
        public string SystemUserName { get; set; }

        [ReadOnly(true)]
        [MaxLength(2048)]
        [Display(Name = "Encrypted Password")]
        public string EncryptedPassword { get; set; }

        [ReadOnly(true)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Login Date")]
        public DateTime LastLoginDate { get; set; }

        [ReadOnly(true)]
        [Display(Name = "Total Logins")]
        public int LoginTotal { get; set; }

        [Display(Name = "Allow Login")]
        public bool AllowLogin { get; set; }

        [MaxLength(512)]
        [Display(Name = "Display Message")]
        public string DisplayMessage { get; set; }

    }
}
