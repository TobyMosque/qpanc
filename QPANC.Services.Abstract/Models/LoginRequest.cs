using QPANC.Services.Abstract.I18n;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace QPANC.Services.Abstract
{
    [DataContract]
    public class LoginRequest
    {
        [EmailAddress(ErrorMessage = nameof(IMessages.ErrorMessage_Email))]
        [Required(ErrorMessage = nameof(IMessages.ErrorMessage_Required))]
        [Display(Name = nameof(IMessages.Field_UserName))]
        [DataMember]
        public string UserName { get; set; }
        [Required(ErrorMessage = nameof(IMessages.ErrorMessage_Required))]
        [Display(Name = nameof(IMessages.Field_Password))]
        [DataMember]
        public string Password { get; set; }
    }
}
