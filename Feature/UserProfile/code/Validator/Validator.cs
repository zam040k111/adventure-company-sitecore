using System.Linq;
using Adventure.Feature.UserProfile.Constants;
using Adventure.Feature.UserProfile.Models;
using FluentValidation;

namespace Adventure.Feature.UserProfile.Validator
{
	public class Validator : AbstractValidator<UserProfileModel>
	{
		public Validator()
		{
			RuleFor(x => x.Password)
				.Must(HasNumber).WithMessage(DictionaryKeys.Messages.PasswordNotContainsNumber)
				.Must(HasUpperCase).WithMessage(DictionaryKeys.Messages.PasswordNotContainsUpperLetter)
				.Must(HasLowerCase).WithMessage(DictionaryKeys.Messages.PasswordNotContainsLowerLetter)
				.Must(HasSpecial).WithMessage(DictionaryKeys.Messages.PasswordNotContainsNonNumeric)
				.MinimumLength(8).WithMessage(DictionaryKeys.Messages.PasswordNotEnoughCharacters);

			RuleFor(x => x.ConfirmPassword)
				.Equal(x => x.Password)
				.WithMessage(DictionaryKeys.Messages.ConfirmPasswordFiled);

			RuleFor(x => x.Email)
				.EmailAddress().WithMessage(DictionaryKeys.Messages.EmailNotCorrect);
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