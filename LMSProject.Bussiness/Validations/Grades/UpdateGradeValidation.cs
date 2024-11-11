using FluentValidation;
using LMSProject.Bussiness.Dtos.GradeDTOS;

namespace LMSProject.Bussiness.Validations.Grades
{
    public class UpdateGradeValidation : AbstractValidator<UpdateGradeRequest>
    {
        #region Constructors
        public UpdateGradeValidation()
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

