using FluentValidation;
using LMSProject.Bussiness.Dtos.ForumDTOS;

namespace LMSProject.Bussiness.Validations.Forums
{
    public class AddForumValidation : AbstractValidator<AddForumRequest>
    {
        #region Constructors
        public AddForumValidation()
        {
            AppplyValidationRules();
        }
        #endregion

        #region Functions
        public void AppplyValidationRules()
        {
            RuleFor(x => x.Title)
                .NotNull().WithMessage("Title is required.")
                .NotEmpty().WithMessage("Title Can't be Empty");

            RuleFor(x => x.CourseId)
                .NotNull().WithMessage("CourseId is required.")
                .NotEmpty().WithMessage("CourseId Can't be Empty");
        }
        #endregion
    }
}

