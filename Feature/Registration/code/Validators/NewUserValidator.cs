using System;
using System.Linq;
using Adventure.Feature.Registration.Enums;
using Adventure.Feature.Registration.Models;
using FluentValidation;

namespace Adventure.Feature.Registration.Validators
{
    public class NewUserValidator : AbstractValidator<NewUserViewModel>
    {
        public NewUserValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(NewUserValidationMessages.PasswordRequiredMessage.ToString())
                .Must(HasNumber).WithMessage(NewUserValidationMessages.NoNumberInPasswordMessage.ToString())
                .Must(HasUpperCase).WithMessage(NewUserValidationMessages.NoUpperCaseInPasswordMessage.ToString())
                .Must(HasLowerCase).WithMessage(NewUserValidationMessages.NoLowerCaseInPasswordMessage.ToString())
                .Must(HasSpecial).WithMessage(NewUserValidationMessages.NoSpecialCharacterInPasswordMessage.ToString())
                .MinimumLength(8).WithMessage(NewUserValidationMessages.PasswordLengthMessage.ToString());

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage(NewUserValidationMessages.ConfirmPassword.ToString());

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(NewUserValidationMessages.EmailRequiredMessage.ToString())
                .EmailAddress().WithMessage(NewUserValidationMessages.EmailInvalidFormatMessage.ToString());

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage(NewUserValidationMessages.UserNameRequiredMessage.ToString())
                .Length(3, 30).WithMessage(NewUserValidationMessages.UserNameLengthMessage.ToString());
        }

        public void SetUserNameUniqueValidation(Func<string, bool> validator)
        {
            RuleFor(x => x.UserName)
                .Must(x => x is null || validator(x))
                .WithMessage(NewUserValidationMessages.UserNameNotUnique.ToString());
        }

        public void SetEmailUniqueValidation(Func<string, bool> validator)
        {
            RuleFor(x => x.Email)
                .Must(x => x is null || validator(x))
                .WithMessage(NewUserValidationMessages.EmailNotUniqueMessage.ToString());
        }
        
        private bool HasNumber(string password)
        {
            return password is null || password.Any(char.IsDigit);
        }

        private bool HasUpperCase(string password)
        {
            return password is null || password.Any(char.IsUpper);
        }

        private bool HasLowerCase(string password)
        {
            return password is null || password.Any(char.IsLower);
        }

        private bool HasSpecial(string password)
        {
            return password is null || password.Any(char.IsLetterOrDigit);
        }
    }
}