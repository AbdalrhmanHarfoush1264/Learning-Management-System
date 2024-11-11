﻿using FluentValidation;
using LMSProject.Bussiness.Dtos.ModuleDTOS;

namespace LMSProject.Bussiness.Validations.Modules
{
    public class UpdateModuleValidation : AbstractValidator<UpdateModuleRequest>
    {
        #region Constructors
        public UpdateModuleValidation()
        {
            AppplyValidationRules();
        }
        #endregion

        #region Functions
        public void AppplyValidationRules()
        {
            RuleFor(x => x.Id)
               .NotNull().WithMessage("Id is required.")
               .NotEmpty().WithMessage("Id Can't be Empty");

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