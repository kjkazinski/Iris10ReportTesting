using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWeb.Code.Data.Models.API
{
    public sealed class AuthStartRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public sealed class AuthStartResponseModel : ApiResponseModelBase
    {
        public string SessionToken { get; set; }
    }

    public sealed class AuthEndRequestModel
    {
        public string SessionToken { get; set; }
    }

    public sealed class AuthUserInformationModel
    {
        public string UserKey { get; set; }
        public string OldUserKey { get; set; }
        public string Username { get; set; }
        public string FullName = "";
        public Dictionary<string, int> RoleLookup { get; set; }
    }

    public sealed class LostPasswordModel
    {
        [Required(ErrorMessage = "We need your email to send you a reset link!")]
        [Display(Name = "Your account email")]
        [EmailAddress(ErrorMessage = "Not a valid email--what are you trying to do here?")]
        public string Email { get; set; }
    }
}
