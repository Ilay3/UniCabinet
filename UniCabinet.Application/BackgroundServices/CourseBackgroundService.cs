// UniCabinet.Application/BackgroundServices/CourseBackgroundService.cs
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UniCabinet.Application.UseCases;

namespace UniCabinet.Application.BackgroundServices
{
    public class CourseBackgroundService : BackgroundService
    {
        private readonly ILogger<CourseBackgroundService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public CourseBackgroundService(IServiceScopeFactory serviceScopeFactory, ILogger<CourseBackgroundService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("CourseBackgroundService: ExecuteAsync начал выполнение.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var updateCoursesUseCase = scope.ServiceProvider.GetRequiredService<UpdateCoursesUseCase>();
                        await updateCoursesUseCase.Execute();
                    }

                    _logger.LogInformation("CourseBackgroundService: Обновление курсов выполнено успешно.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка при обновлении курсов.");
                }

                // Ждём до следующего дня
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }
    }

}
