using Adventure.Feature.Mailing.Models;
using FluentValidation;

namespace Adventure.Feature.Mailing.Validators
{
	public class SubscriptionValidator : AbstractValidator<SubscribeViewModel>
	{
		public void SetMessageToEmailValidator(string message)
		{
			RuleFor(i => i.Email)
				.EmailAddress().WithMessage(message)
				.NotEmpty().WithMessage(message);
		}
	}
}