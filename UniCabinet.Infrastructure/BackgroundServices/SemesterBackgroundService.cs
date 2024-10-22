// Файл: UniCabinet.Infrastructure/BackgroundServices/SemesterBackgroundService.cs

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using UniCabinet.Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace UniCabinet.Infrastructure.BackgroundServices
{
    public class SemesterBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<SemesterBackgroundService> _logger;
        private readonly TimeSpan _checkInterval = TimeSpan.FromHours(1); // Интервал проверки

        public SemesterBackgroundService(IServiceProvider serviceProvider, ILogger<SemesterBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Фоновый сервис обновления семестров запущен.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var semesterService = scope.ServiceProvider.GetRequiredService<ISemesterService>();
                        await semesterService.UpdateCurrentSemesterAsync();
                    }

                    _logger.LogInformation("Проверка и обновление семестров выполнены успешно.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка при обновлении семестров.");
                }

                await Task.Delay(_checkInterval, stoppingToken);
            }

            _logger.LogInformation("Фоновый сервис обновления семестров остановлен.");
        }
    }
}
