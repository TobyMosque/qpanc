using QPANC.Services.Abstract.I18n;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace QPANC.Services.Abstract
{
    [DataContract]
    public class RegisterRequest
    {
        [EmailAddress(ErrorMessage = nameof(IMessages.ErrorMessage_Email))]
        [Required(ErrorMessage = nameof(IMessages.ErrorMessage_Required))]
        [Display(Name = nameof(IMessages.Field_UserName))]
        [DataMember]
        public string UserName { get; set; }

        [Compare(nameof(RegisterRequest.UserName), ErrorMessage = nameof(IMessages.ErrorMessage_Compare))]
        [EmailAddress(ErrorMessage = nameof(IMessages.ErrorMessage_Email))]
        [Required(ErrorMessage = nameof(IMessages.ErrorMessage_Required))]
        [Display(Name = nameof(IMessages.Field_ConfirmUserName))]
        [DataMember]
        public string ConfirmUserName { get; set; }

        [Required(ErrorMessage = nameof(IMessages.ErrorMessage_Required))]
        [Display(Name = nameof(IMessages.Field_FirstName))]
        [DataMember]
        public string FirstName { get; set; }

        [Required(ErrorMessage = nameof(IMessages.ErrorMessage_Required))]
        [Display(Name = nameof(IMessages.Field_LastName))]
        [DataMember]
        public string LastName { get; set; }

        [Required(ErrorMessage = nameof(IMessages.ErrorMessage_Required))]
        [Display(Name = nameof(IMessages.Field_Password))]
        [DataMember]
        public string Password { get; set; }

        [Compare(nameof(RegisterRequest.Password), ErrorMessage = nameof(IMessages.ErrorMessage_Compare))]
        [Required(ErrorMessage = nameof(IMessages.ErrorMessage_Required))]
        [Display(Name = nameof(IMessages.Field_ConfirmPassword))]
        [DataMember]
        public string ConfirmPassword { get; set; }
    }
}
