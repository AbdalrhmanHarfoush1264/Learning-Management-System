using FluentValidation;
using FluentValidation.AspNetCore;
using LMSProject.Bussiness.Implmentions;
using LMSProject.Bussiness.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LMSProject.Bussiness
{
    public static class ModuleLMSBussinessDependencies
    {
        public static IServiceCollection AddLMSBussinessDependencies(this IServiceCollection services)
        {

            // Service for Fluent Validation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

            // Service for Confirm Email 
            services.AddTransient<IActionContextAccessor, ActionContextAccessor>();
            services.AddTransient<IUrlHelper>(x =>
            {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext!);
            });

            //Service for Custom Services in project
            services.AddTransient<INotificationServicecs, NotificationServicecs>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICusAuthonticationService, CusAuthonticationService>();
            services.AddTransient<ICusAuthorizationService, CusAuthorizationService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<ICertificateService, CertificateService>();
            services.AddTransient<IModuleService, ModuleService>();
            services.AddTransient<IEnrollmentService, EnrollmentService>();
            services.AddTransient<IAssignmentService, AssignmentService>();
            services.AddTransient<ISubmissionService, SubmissionService>();
            services.AddTransient<IGradeService, GradeService>();
            services.AddTransient<IForumService, ForumService>();
            services.AddTransient<IForumPostService, ForumPostService>();
            services.AddTransient<IVideoService, VideoService>();
            services.AddTransient<ILessonService, LessonService>();

            return services;
        }
    }
}
