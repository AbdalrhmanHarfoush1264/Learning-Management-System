using FluentValidation;
using LMSProject.Bussiness.Dtos.UserDTOS;

namespace LMSProject.Bussiness.Validations.Users
{
    public class UpdateUserValidation : AbstractValidator<UpdateUserRequest>
    {
        #region Constructors
        public UpdateUserValidation()
        {
            AppplyValidationRules();
        }
        #endregion

        #region Functions
        public void AppplyValidationRules()
        {

            RuleFor(x => x.Id)
                .NotNull().WithMessage("Id is required.");

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


        }
        #endregion
    }
}
