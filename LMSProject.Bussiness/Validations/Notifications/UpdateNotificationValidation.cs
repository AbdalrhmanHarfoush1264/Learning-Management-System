using FluentValidation;
using LMSProject.Bussiness.Dtos.NotificationDTOS;

namespace LMSProject.Bussiness.Validations.Notifications
{
    public class UpdateNotificationValidation : AbstractValidator<UpdateNotificationRequest>
    {
        #region Constructors
        public UpdateNotificationValidation()
        {
            AppplyValidationRules();
        }
        #endregion

        #region Functions
        public void AppplyValidationRules()
        {

            RuleFor(x => x.Id)
                .NotNull().WithMessage("Id is required.")
                .GreaterThan(0).WithMessage("The ID must be a positive number greater than zero.");

            RuleFor(x => x.Message)
                .NotNull().WithMessage("Message is required.")
                .NotEmpty().WithMessage("Message Can't be Empty");
        }
        #endregion
    }
}
