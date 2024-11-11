using FluentValidation;
using LMSProject.Bussiness.Dtos.AuthonticationDTOS;

namespace LMSProject.Bussiness.Validations.Authontications
{
    public class RefreshTokenValidation : AbstractValidator<RefreshTokenRequest>
    {
        #region Constructors
        public RefreshTokenValidation()
        {
            AppplyValidationRules();
        }
        #endregion

        #region Functions
        public void AppplyValidationRules()
        {
            RuleFor(x => x.AccessToken)
                .NotNull().WithMessage("AccessToken is required.")
                .NotEmpty().WithMessage("AccessToken Can't be Empty");

            RuleFor(x => x.RefreshToken)
                .NotNull().WithMessage("RefreshToken is required.")
                .NotEmpty().WithMessage("RefreshToken Can't be Empty");
        }
        #endregion
    }
}

