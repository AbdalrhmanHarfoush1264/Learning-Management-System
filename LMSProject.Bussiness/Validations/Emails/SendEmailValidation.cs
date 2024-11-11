using FluentValidation;
using LMSProject.Bussiness.Dtos.EmailDTOS;

namespace LMSProject.Bussiness.Validations.Emails
{
    public class SendEmailValidation : AbstractValidator<SendEmailRequest>
    {

        #region Constructors
        public SendEmailValidation()
        {
            AppplyValidationRules();
        }
        #endregion

        #region Functions
        public void AppplyValidationRules()
        {
            RuleFor(x => x.Email)
                .NotNull().WithMessage("Email is required.")
                .NotEmpty().WithMessage("Email Can't be Empty")
                .EmailAddress().WithMessage("A vaild Email is Required.");


            RuleFor(x => x.Message)
                .NotNull().WithMessage("Message is required.")
                .NotEmpty().WithMessage("Message Can't be Empty");
        }
        #endregion


    }
}
