using Askmethat.Aspnet.JsonLocalizer.Localizer;
using QPANC.Services.Abstract.I18n;

namespace QPANC.Services.I18n
{
    public class Messages : IMessages
    {
        private readonly IJsonStringLocalizer<IMessages> _localizer;
        public Messages(IJsonStringLocalizer<IMessages> localizer)
        {
            this._localizer = localizer;
        }

        public string ErrorMessage_Compare { get { return this._localizer[nameof(ErrorMessage_Compare)]; } }
        public string ErrorMessage_CreditCard { get { return this._localizer[nameof(ErrorMessage_CreditCard)]; } }
        public string ErrorMessage_CustomValidation { get { return this._localizer[nameof(ErrorMessage_CustomValidation)]; } }
        public string ErrorMessage_Email { get { return this._localizer[nameof(ErrorMessage_Email)]; } }
        public string ErrorMessage_IncorrectPasswordOrUsername { get { return this._localizer[nameof(ErrorMessage_IncorrectPasswordOrUsername)]; } }
        public string ErrorMessage_MaxLength { get { return this._localizer[nameof(ErrorMessage_MaxLength)]; } }
        public string ErrorMessage_MaxLengthArray { get { return this._localizer[nameof(ErrorMessage_MaxLengthArray)]; } }
        public string ErrorMessage_MinLength { get { return this._localizer[nameof(ErrorMessage_MinLength)]; } }
        public string ErrorMessage_MinLengthArray { get { return this._localizer[nameof(ErrorMessage_MinLengthArray)]; } }
        public string ErrorMessage_PasswordTooWeak { get { return this._localizer[nameof(ErrorMessage_PasswordTooWeak)]; } }
        public string ErrorMessage_Range { get { return this._localizer[nameof(ErrorMessage_Range)]; } }
        public string ErrorMessage_Regex { get { return this._localizer[nameof(ErrorMessage_Regex)]; } }
        public string ErrorMessage_Required { get { return this._localizer[nameof(ErrorMessage_Required)]; } }
        public string ErrorMessage_StringLength { get { return this._localizer[nameof(ErrorMessage_StringLength)]; } }
        public string ErrorMessage_StringLengthIncludingMinimum { get { return this._localizer[nameof(ErrorMessage_StringLengthIncludingMinimum)]; } }
        public string ErrorMessage_UserNameAlreadyTaken { get { return this._localizer[nameof(ErrorMessage_UserNameAlreadyTaken)]; } }
        public string ErrorMessage_Validation { get { return this._localizer[nameof(ErrorMessage_Validation)]; } }
        public string Field_ConfirmPassword { get { return this._localizer[nameof(Field_ConfirmPassword)]; } }
        public string Field_ConfirmUserName { get { return this._localizer[nameof(Field_ConfirmUserName)]; } }
        public string Field_FirstName { get { return this._localizer[nameof(Field_FirstName)]; } }
        public string Field_LastName { get { return this._localizer[nameof(Field_LastName)]; } }
        public string Field_Password { get { return this._localizer[nameof(Field_Password)]; } }
        public string Field_UserName { get { return this._localizer[nameof(Field_UserName)]; } }
        public string Text_ProblemDetails { get { return this._localizer[nameof(Text_ProblemDetails)]; } }
    }
}
