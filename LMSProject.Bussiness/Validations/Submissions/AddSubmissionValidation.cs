using FluentValidation;
using LMSProject.Bussiness.Dtos.SubmissionDTOS;

namespace LMSProject.Bussiness.Validations.Submissions
{
    public class AddSubmissionValidation : AbstractValidator<AddSubmissionRequest>
    {
        #region Constructors
        public AddSubmissionValidation()
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

            RuleFor(x => x.AssignmentId)
                .NotNull().WithMessage("AssignmentId is required.")
                .NotEmpty().WithMessage("AssignmentId Can't be Empty");

            RuleFor(x => x.StudentId)
                .NotNull().WithMessage("StudentId is required.")
                .NotEmpty().WithMessage("StudentId Can't be Empty");
        }
        #endregion
    }
}
