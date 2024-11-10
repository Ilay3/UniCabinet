// Файл: UniCabinet.Application/BackgroundServices/SemesterBackgroundService.cs

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UniCabinet.Application.UseCases;

namespace UniCabinet.Application.BackgroundServices
{
    public class SemesterBackgroundService : BackgroundService
    {
        private readonly ILogger<SemesterBackgroundService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public SemesterBackgroundService(ILogger<SemesterBackgroundService> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Фоновый сервис обновления семестров запущен.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var updateCurrentSemesterAsyncUseCase = scope.ServiceProvider.GetRequiredService<UpdateCurrentSemesterAsyncUseCase>();
                        await updateCurrentSemesterAsyncUseCase.ExecuteAsync();
                    }

                    _logger.LogInformation("Проверка и обновление семестров выполнены успешно.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка при обновлении семестров.");
                }

                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }
    }

}
