using FluentValidation;
using LMSProject.Bussiness.Dtos.AssignmentDTOS;

namespace LMSProject.Bussiness.Validations.Assignments
{
    public class AddAssignmentValidation : AbstractValidator<AddAssignmentRequest>
    {
        #region Constructors
        public AddAssignmentValidation()
        {
            AppplyValidationRules();
        }
        #endregion

        #region Functions
        public void AppplyValidationRules()
        {
            RuleFor(x => x.CourseId)
                .NotNull().WithMessage("CourseId is required.")
                .NotEmpty().WithMessage("CourseId Can't be Empty");

            RuleFor(x => x.Title)
                .NotNull().WithMessage("Title is required.")
                .NotEmpty().WithMessage("Title Can't be Empty");

        }
        #endregion
    }
}
