using FluentValidation;
using LMSProject.Bussiness.Dtos.AuthonticationDTOS;

namespace LMSProject.Bussiness.Validations.Authontications
{
    public class SignInValidation : AbstractValidator<SignInRequest>
    {
        #region Constructors
        public SignInValidation()
        {
            AppplyValidationRules();
        }
        #endregion

        #region Functions
        public void AppplyValidationRules()
        {
            RuleFor(x => x.UserName)
                .NotNull().WithMessage("UserName is required.")
                .NotEmpty().WithMessage("UserName Can't be Empty");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("Password is required.")
                .NotEmpty().WithMessage("Password Can't be Empty");
        }
        #endregion
    }
}
