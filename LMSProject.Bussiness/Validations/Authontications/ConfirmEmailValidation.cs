using FluentValidation;
using LMSProject.Bussiness.Dtos.AuthonticationDTOS;

namespace LMSProject.Bussiness.Validations.Authontications
{
    public class ConfirmEmailValidation : AbstractValidator<ConfirmEmailRequest>
    {
        #region Constructors
        public ConfirmEmailValidation()
        {
            AppplyValidationRules();
        }
        #endregion

        #region Functions
        public void AppplyValidationRules()
        {
            RuleFor(x => x.userId)
                .NotNull().WithMessage("UserName is required.")
                .NotEmpty().WithMessage("UserName Can't be Empty");

            RuleFor(x => x.code)
                .NotNull().WithMessage("Password is required.")
                .NotEmpty().WithMessage("Password Can't be Empty");
        }
        #endregion
    }
}
