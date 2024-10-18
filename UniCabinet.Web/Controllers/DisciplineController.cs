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
        public IActionResult DisciplinesList()
        {
            var disciplineDTOs = _disciplineRepository.GetAllDisciplines();
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
        public IActionResult AddDiscipline(DisciplineAddViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_DisciplineAddModal", viewModel);
            }

            var disciplineDTO = viewModel.GetDisciplineDTO();
            _disciplineRepository.AddDiscipline(disciplineDTO);

            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult DisciplineEditModal(int id)
        {
            var disciplineDTO = _disciplineRepository.GetDisciplineById(id);
            if (disciplineDTO == null)
            {
                return NotFound();
            }

            var viewModel = disciplineDTO.GetDisciplineEditViewModel();
            return PartialView("_DisciplineEditModal", viewModel);
        }

        [HttpPost]
        public IActionResult EditDiscipline(DisciplineEditViewModel viewModel)
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
