using FluentValidation;
using LMSProject.Bussiness.Dtos.CourseDTOS;

namespace LMSProject.Bussiness.Validations.Courses
{
    public class UpdateCourseValidation : AbstractValidator<UpdateCourseRequest>
    {
        #region Constructors
        public UpdateCourseValidation()
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
