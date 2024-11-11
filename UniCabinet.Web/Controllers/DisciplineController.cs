using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UniCabinet.Application.UseCases.DisciplineUseCase;
using UniCabinet.Core.Models.ViewModel.Discipline;

namespace UniCabinet.Web.Controllers
{
    public class DisciplineController : Controller
    {
        private readonly IMapper _mapper;

        public DisciplineController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> DisciplinesListAsync([FromServices] GetDisciplinesListUseCase getDisciplinesListUseCase)
        {
            var disciplineVMs = await getDisciplinesListUseCase.ExecuteAsync();
            return View(disciplineVMs);
        }

        [HttpGet]
        public IActionResult DisciplineAddModal()
        {
            var viewModel = new DisciplineAddVM();
            return PartialView("_DisciplineAddModal", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddDisciplineAsync(
            DisciplineAddVM viewModel,
            [FromServices] AddDisciplineUseCase addDisciplineUseCase)
        {
            var success = await addDisciplineUseCase.ExecuteAsync(viewModel, ModelState);

            if (success)
            {
                return Json(new { success = true });
            }

            return PartialView("_DisciplineAddModal", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DisciplineEditModalAsync(
            int id,
            [FromServices] GetDisciplineForEditUseCase getDisciplineForEditUseCase)
        {
            var disciplineVM = await getDisciplineForEditUseCase.ExecuteAsync(id);
            if (disciplineVM == null)
            {
                return NotFound();
            }

            return PartialView("_DisciplineEditModal", disciplineVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditDisciplineAsync(
            DisciplineEditVM viewModel,
            [FromServices] UpdateDisciplineUseCase updateDisciplineUseCase)
        {
            var success = await updateDisciplineUseCase.ExecuteAsync(viewModel, ModelState);

            if (success)
            {
                return Json(new { success = true });
            }

            return PartialView("_DisciplineEditModal", viewModel);
        }
    }
}
