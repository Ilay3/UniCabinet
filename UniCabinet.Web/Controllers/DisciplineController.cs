using Microsoft.AspNetCore.Mvc;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Web.Extension.Discipline;
using UniCabinet.Web.Mapping;
using UniCabinet.Web.Mapping.Discipline;
using UniCabinet.Web.ViewModel.Discipline;

namespace UniCabinet.Web.Controllers
{
    public class DisciplineController : Controller
    {
        private readonly IDisciplineRepository _disciplineRepository;

        public DisciplineController(IDisciplineRepository disciplineRepository)
        {
            _disciplineRepository = disciplineRepository;
        }

        [HttpGet]
        public async Task<IActionResult> DisciplinesList()
        {
            var disciplineDTOs = await _disciplineRepository.GetAllDisciplines();
            var disciplineViewModels = disciplineDTOs
                .Select(dto => dto.GetDisciplineViewModel())
                .ToList();

            return View(disciplineViewModels);
        }

        [HttpGet]
        public IActionResult DisciplineAddModal()
        {
            var viewModel = new DisciplineAddViewModel();
            return PartialView("_DisciplineAddModal", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddDiscipline(DisciplineAddViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_DisciplineAddModal", viewModel);
            }

            var disciplineDTO = viewModel.GetDisciplineDTO();
            await _disciplineRepository.AddDisciplineAsync(disciplineDTO);

            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> DisciplineEditModal(int id)
        {
            var disciplineDTO = await _disciplineRepository.GetDisciplineById(id);
            if (disciplineDTO == null)
            {
                return NotFound();
            }

            var viewModel = disciplineDTO.GetDisciplineEditViewModel();
            return PartialView("_DisciplineEditModal", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditDiscipline(DisciplineEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_DisciplineEditModal", viewModel);
            }

            var disciplineDTO = viewModel.GetDisciplineDTO();
            _disciplineRepository.UpdateDiscipline(disciplineDTO);

            return Json(new { success = true });
        }
    }
}
