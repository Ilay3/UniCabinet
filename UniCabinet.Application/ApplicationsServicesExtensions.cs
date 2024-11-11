using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UniCabinet.Application.BackgroundServices;
using UniCabinet.Application.UseCases;
using UniCabinet.Application.UseCases.GroupUseCase;
using UniCabinet.Application.UseCases.LectureUseCase;

namespace UniCabinet.Infrastructure
{
    public class ApplicationsServicesExtensions
    {
        public static void AddApplicationLayer(IServiceCollection services)
        {
            services.AddHostedService<SemesterBackgroundService>();
            services.AddHostedService<CourseBackgroundService>();

            services.AddTransient<GetLecturesListDataUseCase>();
            services.AddTransient<GetLecturesListDataUseCase>();
            services.AddTransient<AddLectureUseCase>();
            services.AddTransient<GetLectureForEditUseCase>();
            services.AddTransient<UpdateLectureUseCase>();
            services.AddTransient<GetLectureAttendanceUseCase>();
            services.AddTransient<SaveLectureAttendanceUseCase>();

            services.AddTransient<UpdateCoursesUseCase>();
            services.AddTransient<UpdateCurrentSemesterAsyncUseCase>();
            services.AddTransient<UserVerificationUseCase>();

            //Группы
            services.AddTransient<GetGroupsListUseCase>();
            services.AddTransient<GetGroupAddModalUseCase>();
            services.AddTransient<GetGroupEditModalUseCase>();
            services.AddTransient<AddGroupUseCase>();
            services.AddTransient<EditGroupUseCase>();


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
