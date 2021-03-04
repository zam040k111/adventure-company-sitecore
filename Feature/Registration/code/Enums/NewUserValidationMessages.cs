namespace Adventure.Feature.Registration.Enums
{
    public enum NewUserValidationMessages
    { 
        NoNumberInPasswordMessage, 
        NoUpperCaseInPasswordMessage,
        NoLowerCaseInPasswordMessage, 
        NoSpecialCharacterInPasswordMessage,
        PasswordLengthMessage,
        PasswordRequiredMessage,
        EmailInvalidFormatMessage,
        EmailRequiredMessage,
        EmailNotUniqueMessage,
        UserNameLengthMessage,
        UserNameRequiredMessage,
        UserNameNotUnique,
        ConfirmPassword
    }
}
