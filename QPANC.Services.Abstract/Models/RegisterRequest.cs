using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace QPANC.Services.Abstract
{
    [DataContract]
    public class RegisterRequest
    {
        [EmailAddress(ErrorMessage = nameof(Messages.ErrorMessage_Email))]
        [Required(ErrorMessage = nameof(Messages.ErrorMessage_Required))]
        [Display(Name = nameof(Messages.Field_UserName))]
        [DataMember]
        public string UserName { get; set; }

        [Compare(nameof(RegisterRequest.UserName), ErrorMessage = nameof(Messages.ErrorMessage_Compare))]
        [EmailAddress(ErrorMessage = nameof(Messages.ErrorMessage_Email))]
        [Required(ErrorMessage = nameof(Messages.ErrorMessage_Required))]
        [Display(Name = nameof(Messages.Field_ConfirmUserName))]
        [DataMember]
        public string ConfirmUserName { get; set; }

        [Required(ErrorMessage = nameof(Messages.ErrorMessage_Required))]
        [Display(Name = nameof(Messages.Field_FirstName))]
        [DataMember]
        public string FirstName { get; set; }

        [Required(ErrorMessage = nameof(Messages.ErrorMessage_Required))]
        [Display(Name = nameof(Messages.Field_LastName))]
        [DataMember]
        public string LastName { get; set; }

        [Required(ErrorMessage = nameof(Messages.ErrorMessage_Required))]
        [Display(Name = nameof(Messages.Field_Password))]
        [DataMember]
        public string Password { get; set; }

        [Compare(nameof(RegisterRequest.Password), ErrorMessage = nameof(Messages.ErrorMessage_Compare))]
        [Required(ErrorMessage = nameof(Messages.ErrorMessage_Required))]
        [Display(Name = nameof(Messages.Field_ConfirmPassword))]
        [DataMember]
        public string ConfirmPassword { get; set; }
    }
}
