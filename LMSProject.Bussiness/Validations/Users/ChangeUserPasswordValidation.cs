using FluentValidation;
using LMSProject.Bussiness.Dtos.UserDTOS;

namespace LMSProject.Bussiness.Validations.Users
{
    public class ChangeUserPasswordValidation : AbstractValidator<ChangeUserPasswordRequest>
    {
        #region Constructors
        public ChangeUserPasswordValidation()
        {
            AppplyValidationRules();
        }
        #endregion

        #region Functions
        public void AppplyValidationRules()
        {

            RuleFor(x => x.Id)
                .NotNull().WithMessage("Id is required.");

            RuleFor(x => x.CurrentPassword)
                .NotNull().WithMessage("CurrentPassword is required.")
                .NotEmpty().WithMessage("CurrentPassword Can't be Empty");

            RuleFor(x => x.NewPassword)
                .NotNull().WithMessage("NewPassword is required.")
                .NotEmpty().WithMessage("NewPassword Can't be Empty");

            RuleFor(x => x.ConfirmPassword)
                .NotNull().WithMessage("ConfirmPassword is required.")
                .NotEmpty().WithMessage("ConfirmPassword Can't be Empty")
                .Equal(x => x.NewPassword).WithMessage("New password and confirm password not Equals.");


        }
        #endregion
    }
}
