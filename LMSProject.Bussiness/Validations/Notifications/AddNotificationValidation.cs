using FluentValidation;
using LMSProject.Bussiness.Dtos.NotificationDTOS;

namespace LMSProject.Bussiness.Validations.Notifications
{
    public class AddNotificationValidation : AbstractValidator<AddNotificationRequest>
    {

        #region Constructors
        public AddNotificationValidation()
        {
            AppplyValidationRules();
        }
        #endregion

        #region Functions
        public void AppplyValidationRules()
        {
            RuleFor(x => x.Message)
                .NotNull().WithMessage("Message is required.")
                .NotEmpty().WithMessage("Message Can't be Empty");


            RuleFor(x => x.studentId)
                .NotNull().WithMessage("studentId is required.")
                .NotEmpty().WithMessage("studentId Can't be Empty");
        }
        #endregion


    }
}
