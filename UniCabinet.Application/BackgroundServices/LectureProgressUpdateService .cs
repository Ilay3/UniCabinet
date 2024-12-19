using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UniCabinet.Application.BackgroundServices;
using UniCabinet.Application.UseCases.BackgroundServices;

public class LectureProgressUpdateService : BackgroundService
{
    private readonly ILogger<LectureProgressUpdateService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public LectureProgressUpdateService(ILogger<LectureProgressUpdateService> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("Lecture progress update service started.");
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var updateStudentProgressUseCase = scope.ServiceProvider.GetRequiredService<UpdateStudentProgressUseCase>();
                    await updateStudentProgressUseCase.ExecuteAsync();
                }


                _logger.LogInformation("Lecture progress update service completed.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating student progress.");
            }

            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }


}
