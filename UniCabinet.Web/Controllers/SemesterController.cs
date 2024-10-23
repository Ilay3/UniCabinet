using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniCabinet.Application.Interfaces.Services;

namespace UniCabinet.Controllers
{
    [Authorize(Roles = "Administrator, Manager")]
    [ApiController]
    [Route("api/[controller]")]
    public class SemesterController : ControllerBase
    {
        private readonly ISemesterService _semesterService;
        private readonly ILogger<SemesterController> _logger;

        public SemesterController(ISemesterService semesterService, ILogger<SemesterController> logger)
        {
            _semesterService = semesterService;
            _logger = logger;
        }

        /// <summary>
        /// Ручной триггер для обновления текущего семестра.
        /// Можно передать тестовую дату для обновления семестра.
        /// </summary>
        /// <param name="testDate">Тестовая дата (опционально)</param>
        /// <returns>Результат обновления семестра.</returns>
        [HttpPost("Update")]
        public IActionResult UpdateSemester([FromQuery] DateTime? testDate = null)
        {
            try
            {
                _semesterService.UpdateCurrentSemesterAsync(testDate);
                return Ok("Обновление семестра выполнено.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при ручном обновлении семестра.");
                return StatusCode(500, "Ошибка при обновлении семестра.");
            }
        }
    }
}
