using FluentValidation;
using LMSProject.Bussiness.Dtos.ForumPostDTOS;

namespace LMSProject.Bussiness.Validations.ForumPosts
{
    public class AddForumPostValidation : AbstractValidator<AddForumPostRequest>
    {
        #region Constructors
        public AddForumPostValidation()
        {
            AppplyValidationRules();
        }
        #endregion

        #region Functions
        public void AppplyValidationRules()
        {
            RuleFor(x => x.Content)
                .NotNull().WithMessage("Content is required.")
                .NotEmpty().WithMessage("Content Can't be Empty");

            RuleFor(x => x.ForumId)
                .NotNull().WithMessage("ForumId is required.")
                .NotEmpty().WithMessage("ForumId Can't be Empty");

            RuleFor(x => x.StudentId)
                .NotNull().WithMessage("StudentId is required.")
                .NotEmpty().WithMessage("StudentId Can't be Empty");
        }
        #endregion
    }
}
