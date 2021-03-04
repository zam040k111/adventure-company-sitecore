using Adventure.Feature.EventDetailsComments.Enums;
using Adventure.Feature.EventDetailsComments.Models;
using FluentValidation;

namespace Adventure.Feature.EventDetailsComments.Validators
{
    public class AddCommentValidator : AbstractValidator<AddCommentViewModel>
    {
        public AddCommentValidator()
        {
            RuleFor(x => x.AuthorName)
                .NotEmpty().WithMessage(AddCommentValidationMessages.AuthorNameRequiredValidation.ToString())
                .MinimumLength(3).WithMessage(AddCommentValidationMessages.AuthorNameLengthValidation.ToString());

            RuleFor(x => x.Text)
                .NotEmpty().WithMessage(AddCommentValidationMessages.TextRequiredValidation.ToString())
                .MinimumLength(10).WithMessage(AddCommentValidationMessages.TextLengthValidation.ToString());
        }
    }
}
