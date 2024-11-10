using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UniCabinet.Application.BackgroundServices;
using UniCabinet.Application.UseCases;

namespace UniCabinet.Infrastructure
{
    public class ApplicationsServicesExtensions
    {
        public static void AddApplicationLayer(IServiceCollection services)
        {
            services.AddHostedService<SemesterBackgroundService>();
            services.AddHostedService<CourseBackgroundService>();

            services.AddTransient<LecturesListUseCase>();
            services.AddTransient<UpdateCoursesUseCase>();
            services.AddTransient<UpdateCurrentSemesterAsyncUseCase>();
            services.AddTransient<UserVerificationUseCase>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            try
            {
                var mapper = services.BuildServiceProvider().GetRequiredService<IMapper>();
                mapper.ConfigurationProvider.AssertConfigurationIsValid();
            }
            catch (AutoMapperConfigurationException ex)
            {
                Console.WriteLine(ex.Message); // Посмотреть подробности ошибки
                throw;
            }

        }
    }
}
