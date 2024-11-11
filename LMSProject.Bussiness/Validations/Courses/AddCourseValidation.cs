using FluentValidation;
using LMSProject.Bussiness.Dtos.CourseDTOS;

namespace LMSProject.Bussiness.Validations.Courses
{
    public class AddCourseValidation : AbstractValidator<AddCourseRequest>
    {
        #region Constructors
        public AddCourseValidation()
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

            RuleFor(x => x.TeacherId)
                .NotNull().WithMessage("TeacherId is required.")
                .NotEmpty().WithMessage("TeacherId Can't be Empty");
        }
        #endregion
    }
}
