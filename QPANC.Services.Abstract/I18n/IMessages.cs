namespace QPANC.Services.Abstract.I18n
{
    public interface IMessages
    {
        string ErrorMessage_Compare { get; }
        string ErrorMessage_CreditCard { get; }
        string ErrorMessage_CustomValidation { get; }
        string ErrorMessage_Email { get; }
        string ErrorMessage_IncorrectPasswordOrUsername { get; }
        string ErrorMessage_MaxLength { get; }
        string ErrorMessage_MaxLengthArray { get; }
        string ErrorMessage_MinLength { get; }
        string ErrorMessage_MinLengthArray { get; }
        string ErrorMessage_PasswordTooWeak { get; }
        string ErrorMessage_Range { get; }
        string ErrorMessage_Regex { get; }
        string ErrorMessage_Required { get; }
        string ErrorMessage_StringLength { get; }
        string ErrorMessage_StringLengthIncludingMinimum { get; }
        string ErrorMessage_UserNameAlreadyTaken { get; }
        string ErrorMessage_Validation { get; }
        string Field_ConfirmPassword { get; }
        string Field_ConfirmUserName { get; }
        string Field_FirstName { get; }
        string Field_LastName { get; }
        string Field_Password { get; }
        string Field_UserName { get; }
        string Text_ProblemDetails { get; }
    }
}
