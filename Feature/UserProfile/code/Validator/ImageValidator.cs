using System.Web;
using Adventure.Feature.UserProfile.Constants;
using FluentValidation;

namespace Adventure.Feature.UserProfile.Validator
{
	public class ImageValidator : AbstractValidator<HttpPostedFileBase>
	{
		public ImageValidator()
		{
			RuleFor(i => i.ContentType)
				.Must(i => i.Contains("image"))
				.WithMessage(DictionaryKeys.Messages.FileIsNotImage);

			RuleFor(i => i.ContentLength)
				.Must(i => i < 5000000)
				.WithMessage(DictionaryKeys.Messages.InvalidImageSize);
		}
	}
}