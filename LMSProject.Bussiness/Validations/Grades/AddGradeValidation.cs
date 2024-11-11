using FluentValidation;
using LMSProject.Bussiness.Dtos.GradeDTOS;

namespace LMSProject.Bussiness.Validations.Grades
{
    public class AddGradeValidation : AbstractValidator<AddGradeRequest>
    {
        #region Constructors
        public AddGradeValidation()
        {
            AppplyValidationRules();
        }
        #endregion

        #region Functions
        public void AppplyValidationRules()
        {
            RuleFor(x => x.GradeValue)
                .NotNull().WithMessage("GradeValue is required.")
                .NotEmpty().WithMessage("GradeValue Can't be Empty");

            RuleFor(x => x.SubmissionId)
                .NotNull().WithMessage("SubmissionId is required.")
                .NotEmpty().WithMessage("SubmissionId Can't be Empty");

        }
        #endregion
    }
}

