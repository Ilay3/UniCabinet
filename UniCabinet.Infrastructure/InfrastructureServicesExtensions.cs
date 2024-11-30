using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using UniCabinet.Application.Interfaces;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Infrastructure.Implementations.Repository;
using UniCabinet.Infrastructure.Implementations.Services;

namespace UniCabinet.Infrastructure
{
    public class InfrastructureServicesExtensions
    {
        public static void AddInfrastructureLayer(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<ICourseRepository, CourseRepositoryImpl>();
            services.AddScoped<ISemesterRepository, SemesterRepository>();
            services.AddScoped<IDisciplineRepository, DisciplineRepositoryImpl>();
            services.AddScoped<ILectureRepository, LectureRepositoryImpl>();
            services.AddScoped<ILectureVisitRepository, LectureVisitRepositoryImpl>();
            services.AddScoped<IDisciplineDetailRepository, DisciplineDetailRepositoryImpl>();
            services.AddTransient<IDepartmentRepository, DepartmentRepositoryImpl>();
            services.AddTransient<ISpecializationRepository, SpecializationRepositoryImpl>();

        }
    }
}
