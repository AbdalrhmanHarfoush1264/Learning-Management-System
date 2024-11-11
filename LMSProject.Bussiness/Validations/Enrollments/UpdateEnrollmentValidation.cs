using FluentValidation;
using LMSProject.Bussiness.Dtos.EnrollmentDTOS;

namespace LMSProject.Bussiness.Validations.Enrollments
{
    public class UpdateEnrollmentValidation : AbstractValidator<UpdateEnrollmentRequest>
    {
        #region Constructors
        public UpdateEnrollmentValidation()
        {
            AppplyValidationRules();
        }
        #endregion

        #region Functions
        public void AppplyValidationRules()
        {
            RuleFor(x => x.Id)
               .NotNull().WithMessage("Id is required.")
               .NotEmpty().WithMessage("Id Can't be Empty");

            RuleFor(x => x.CourseId)
                .NotNull().WithMessage("CourseId is required.")
                .NotEmpty().WithMessage("CourseId Can't be Empty");

            RuleFor(x => x.StudentId)
                .NotNull().WithMessage("StudentId is required.")
                .NotEmpty().WithMessage("StudentId Can't be Empty");

        }
        #endregion
    }
}
