// UniCabinet.Infrastructure/BackgroundServices/CourseBackgroundService.cs
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UniCabinet.Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace UniCabinet.Infrastructure.BackgroundServices
{
    public class CourseBackgroundService : BackgroundService
    {
        private readonly ILogger<CourseBackgroundService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly List<(int Month, int Day)> _targetDates;

        public CourseBackgroundService(IServiceScopeFactory serviceScopeFactory, ILogger<CourseBackgroundService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;

            // Определение целевых дат (30 и 31 августа)
            _targetDates = new List<(int Month, int Day)>
            {
                (8, 30),
                (8, 31)
            };

            _logger.LogInformation("CourseBackgroundService: Конструктор вызван.");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("CourseBackgroundService: ExecuteAsync начал выполнение.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var today = DateTime.Today;
                    var currentMonth = today.Month;
                    var currentDay = today.Day;

                    _logger.LogInformation($"CourseBackgroundService: Сегодня {today.ToShortDateString()}.");

                    // Проверка, совпадает ли текущая дата с одной из целевых дат
                    bool isRunDay = false;
                    foreach (var (Month, Day) in _targetDates)
                    {
                        // Проверка на существование дня в месяце
                        int daysInMonth = DateTime.DaysInMonth(today.Year, Month);
                        int adjustedDay = Day > daysInMonth ? daysInMonth : Day;

                        if (currentMonth == Month && currentDay == adjustedDay)
                        {
                            isRunDay = true;
                            _logger.LogInformation($"CourseBackgroundService: Сегодня совпадает с целевой датой {Month}.{adjustedDay}.");
                            break;
                        }
                    }

                    if (isRunDay)
                    {
                        _logger.LogInformation("CourseBackgroundService: Выполнение обновления курсов.");

                        using (var scope = _serviceScopeFactory.CreateScope())
                        {
                            var courseService = scope.ServiceProvider.GetRequiredService<ICourseService>();
                            courseService.UpdateCourses();
                            _logger.LogInformation("CourseBackgroundService: Обновление курсов выполнено успешно.");
                        }
                    }
                    else
                    {
                        _logger.LogInformation("CourseBackgroundService: Сегодня не требуется обновление курсов.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "CourseBackgroundService: Ошибка при обновлении курсов.");
                }

                // Ждем до следующего дня в полночь
                var nextRunTime = DateTime.Today.AddDays(1).AddHours(0).AddMinutes(0).AddSeconds(0);
                var delay = nextRunTime - DateTime.Now;

                // Проверка на отрицательную задержку
                if (delay < TimeSpan.Zero)
                {
                    delay = TimeSpan.Zero;
                }

                _logger.LogInformation($"CourseBackgroundService: Ожидание {delay} до следующего запуска.");

                await Task.Delay(delay, stoppingToken);
            }
        }
    }
}
