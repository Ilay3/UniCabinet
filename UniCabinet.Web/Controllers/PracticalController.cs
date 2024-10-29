using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Application.Interfaces.Services;
using UniCabinet.Domain.DTO;
using UniCabinet.Web.ViewModel.Practical;

namespace UniCabinet.Web.Controllers
{
    [Authorize(Roles = "Преподаватель")]
    public class PracticalController : Controller
    {
        private readonly IPracticalService _practicalService;
        private readonly IDisciplineDetailRepository _disciplineDetailRepository;
        private readonly ILogger<PracticalController> _logger;

        public PracticalController(
            IPracticalService practicalService,
            IDisciplineDetailRepository disciplineDetailRepository,
            ILogger<PracticalController> logger)
        {
            _practicalService = practicalService;
            _disciplineDetailRepository = disciplineDetailRepository;
            _logger = logger;
        }

        // Отображение списка практических работ для DisciplineDetail
        public async Task<IActionResult> Index(int disciplineDetailId)
        {
            var practicals = await _practicalService.GetPracticalsByDisciplineDetailIdAsync(disciplineDetailId);
            var disciplineDetail = await _disciplineDetailRepository.GetDisciplineDetailByIdAsync(disciplineDetailId);

            if (disciplineDetail == null)
            {
                return NotFound();
            }

            var viewModel = new PracticalListViewModel
            {
                DisciplineDetailId = disciplineDetailId,
                CurrentPracticalCount = practicals.Count,
                MaxPracticalCount = disciplineDetail.PracticalCount,
                Practicals = practicals
            };

            return View(viewModel);
        }

        // GET: Создание новой практической работы
        [HttpGet]
        public IActionResult Create(int disciplineDetailId)
        {
            var viewModel = new PracticalDTO
            {
                DisciplineDetailId = disciplineDetailId
            };
            return PartialView("_PracticalCreateModal", viewModel);
        }

        // POST: Создание новой практической работы
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PracticalDTO practicalDto)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_PracticalCreateModal", practicalDto);
            }

            try
            {
                await _practicalService.AddPracticalAsync(practicalDto);
                return Json(new { success = true });
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return PartialView("_PracticalCreateModal", practicalDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при добавлении практической работы.");
                ModelState.AddModelError(string.Empty, "Произошла ошибка при добавлении практической работы.");
                return PartialView("_PracticalCreateModal", practicalDto);
            }
        }

        // GET: Редактирование практической работы
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var practical = await _practicalService.GetPracticalByIdAsync(id);
            if (practical == null)
            {
                return NotFound();
            }

            return PartialView("_PracticalEditModal", practical);
        }

        // POST: Редактирование практической работы
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PracticalDTO practicalDto)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_PracticalEditModal", practicalDto);
            }

            try
            {
                await _practicalService.UpdatePracticalAsync(practicalDto);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении практической работы.");
                ModelState.AddModelError(string.Empty, "Произошла ошибка при обновлении практической работы.");
                return PartialView("_PracticalEditModal", practicalDto);
            }
        }

        // POST: Удаление практической работы
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int disciplineDetailId)
        {
            try
            {
                await _practicalService.DeletePracticalAsync(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при удалении практической работы.");
                return Json(new { success = false, message = "Произошла ошибка при удалении практической работы." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPracticals(int disciplineDetailId)
        {
            var practicals = await _practicalService.GetPracticalsByDisciplineDetailIdAsync(disciplineDetailId);
            ViewBag.DisciplineDetailId = disciplineDetailId;
            return PartialView("_PracticalList", practicals);
        }

    }
}
