// UniCabinet.Infrastructure/BackgroundServices/CourseBackgroundService.cs
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UniCabinet.Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using EFCore.BulkExtensions;

namespace UniCabinet.Infrastructure.BackgroundServices
{
    public class CourseBackgroundService : BackgroundService
    {
        private readonly ILogger<CourseBackgroundService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly List<(int Month, int Day)> _targetDates;
        private readonly TimeZoneInfo _timeZone;

        public CourseBackgroundService(IServiceScopeFactory serviceScopeFactory, ILogger<CourseBackgroundService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;

            // Определение целевых дат (30 и 31 августа)
            _targetDates = new List<(int Month, int Day)>
            {
                (8, 30),
                (8, 31),
                (10, 29)
            };

            // Установите нужный часовой пояс (по умолчанию - локальный)
            _timeZone = TimeZoneInfo.Local;

            _logger.LogInformation("CourseBackgroundService: Конструктор вызван. Часовой пояс: {TimeZone}", _timeZone.DisplayName);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("CourseBackgroundService: ExecuteAsync начал выполнение.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Получаем текущее время в заданном часовом поясе
                    var now = TimeZoneInfo.ConvertTime(DateTime.UtcNow, _timeZone);
                    var today = now.Date;
                    var currentMonth = now.Month;
                    var currentDay = now.Day;

                    _logger.LogInformation("CourseBackgroundService: Сегодня {Date}.", today.ToShortDateString());

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
                            _logger.LogInformation("CourseBackgroundService: Сегодня совпадает с целевой датой {Month}.{Day}.", Month, adjustedDay);
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

                // Ждём до следующего дня в полночь
                var nowUtc = DateTime.UtcNow;
                var nowInTimeZone = TimeZoneInfo.ConvertTime(nowUtc, _timeZone);
                var nextRunTime = nowInTimeZone.Date.AddDays(1).AddHours(0).AddMinutes(0).AddSeconds(0);
                var delay = nextRunTime - TimeZoneInfo.ConvertTime(DateTime.UtcNow, _timeZone);

                // Проверка на отрицательную задержку
                if (delay < TimeSpan.Zero)
                {
                    delay = TimeSpan.Zero;
                }

                _logger.LogInformation("CourseBackgroundService: Ожидание {Delay} до следующего запуска ({NextRunTime}).", delay, nextRunTime);

                await Task.Delay(delay, stoppingToken);
            }
        }
    }
}
