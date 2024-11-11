using FluentValidation;
using LMSProject.Bussiness.Dtos.ModuleDTOS;

namespace LMSProject.Bussiness.Validations.Modules
{
    public class AddModuleValidation : AbstractValidator<AddModuleRequest>
    {
        #region Constructors
        public AddModuleValidation()
        {
            AppplyValidationRules();
        }
        #endregion

        #region Functions
        public void AppplyValidationRules()
        {
            RuleFor(x => x.Title)
                .NotNull().WithMessage("Title is required.")
                .NotEmpty().WithMessage("Title Can't be Empty");

            RuleFor(x => x.CourseId)
                .NotNull().WithMessage("CourseId is required.")
                .NotEmpty().WithMessage("CourseId Can't be Empty");

        }
        #endregion
    }
}

