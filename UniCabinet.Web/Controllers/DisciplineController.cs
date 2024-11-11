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
        public IActionResult DisciplinesList([FromServices] GetDisciplinesListUseCase getDisciplinesListUseCase)
        {
            var disciplineVMs = getDisciplinesListUseCase.Execute();
            return View(disciplineVMs);
        }

        [HttpGet]
        public IActionResult DisciplineAddModal()
        {
            var viewModel = new DisciplineAddVM();
            return PartialView("_DisciplineAddModal", viewModel);
        }

        [HttpPost]
        public IActionResult AddDiscipline(
            DisciplineAddVM viewModel,
            [FromServices] AddDisciplineUseCase addDisciplineUseCase)
        {
            var success = addDisciplineUseCase.Execute(viewModel, ModelState);

            if (success)
            {
                return Json(new { success = true });
            }

            return PartialView("_DisciplineAddModal", viewModel);
        }

        [HttpGet]
        public IActionResult DisciplineEditModal(
            int id,
            [FromServices] GetDisciplineForEditUseCase getDisciplineForEditUseCase)
        {
            var disciplineVM = getDisciplineForEditUseCase.Execute(id);
            if (disciplineVM == null)
            {
                return NotFound();
            }

            return PartialView("_DisciplineEditModal", disciplineVM);
        }

        [HttpPost]
        public IActionResult EditDiscipline(
            DisciplineEditVM viewModel,
            [FromServices] UpdateDisciplineUseCase updateDisciplineUseCase)
        {
            var success = updateDisciplineUseCase.Execute(viewModel, ModelState);

            if (success)
            {
                return Json(new { success = true });
            }

            return PartialView("_DisciplineEditModal", viewModel);
        }
    }
}
