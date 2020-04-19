using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace QPANC.Services.Abstract
{
    [DataContract]
    public class LoginRequest
    {
        [EmailAddress(ErrorMessage = nameof(Messages.ErrorMessage_Email))]
        [Required(ErrorMessage = nameof(Messages.ErrorMessage_Required))]
        [Display(Name = nameof(Messages.Field_UserName))]
        [DataMember]
        public string UserName { get; set; }
        [Required(ErrorMessage = nameof(Messages.ErrorMessage_Required))]
        [Display(Name = nameof(Messages.Field_Password))]
        [DataMember]
        public string Password { get; set; }
    }
}
