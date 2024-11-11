using FluentValidation;
using LMSProject.Bussiness.Dtos.CertificateDTOS;

namespace LMSProject.Bussiness.Validations.Certifications
{
    public class AddCertificateValidation : AbstractValidator<AddCertificateRequest>
    {
        #region Constructors
        public AddCertificateValidation()
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

            RuleFor(x => x.StudentId)
                .NotNull().WithMessage("StudentId is required.")
                .NotEmpty().WithMessage("StudentId Can't be Empty");

        }
        #endregion
    }
}
