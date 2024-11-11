using FluentValidation;
using LMSProject.Bussiness.Dtos.UserDTOS;

namespace LMSProject.Bussiness.Validations.Users
{
    public class AddUserValidation : AbstractValidator<AddUserRequest>
    {
        #region Constructors
        public AddUserValidation()
        {
            AppplyValidationRules();
        }
        #endregion

        #region Functions
        public void AppplyValidationRules()
        {
            RuleFor(x => x.FullName)
                .NotNull().WithMessage("FullName is required.")
                .NotEmpty().WithMessage("FullName Can't be Empty");

            RuleFor(x => x.UserName)
                .NotNull().WithMessage("UserName is required.")
                .NotEmpty().WithMessage("UserName Can't be Empty");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invaild Email.")
                .NotNull().WithMessage("Email is required.")
                .NotEmpty().WithMessage("Email Can't be Empty");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("Password is required.")
                .NotEmpty().WithMessage("Password Can't be Empty");

            RuleFor(x => x.ConfirmPassword)
                .NotNull().WithMessage("ConfirmPassword is required.")
                .NotEmpty().WithMessage("ConfirmPassword Can't be Empty")
                .Equal(x => x.Password).WithMessage("Password and ConfirmPassword not Equal.");

        }
        #endregion
    }
}
