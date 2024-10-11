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
            var disciplineDTO = _disciplineRepository.GetAllDisciplines();
            var disciplineViewModel = disciplineDTO
                .Select(dto => dto.GetDisciplineViewModel())
                .ToList();

            return View(disciplineViewModel);
        }

        public async Task<IActionResult> AddDiscipline(DisciplineAddViewModel viewModel) 
        {
            if (!ModelState.IsValid) return PartialView("_DisciplineAddModal", viewModel);

            var disciplineDTO = viewModel.GetDisciplineDTO();

            await _disciplineRepository.AddDisciplineAsync(disciplineDTO);

            return RedirectToAction("DisciplinesList");
        }

        public IActionResult EditDiscipline(DisciplineEditViewModel viewModel, int id)
        {
            if (viewModel.Id != id) return NotFound();

            if (!ModelState.IsValid) return PartialView("_DisciplineEditModal", viewModel);

            var disciplineDTO = viewModel.GetDisciplineDTO();

            _disciplineRepository.UpdateDiscipline(disciplineDTO);

            return RedirectToAction("DisciplinesList");
        }

        public IActionResult DisciplineAddModal()
        {
            return PartialView("_DisciplineAddModal");
        }
        
        public IActionResult DisciplineEditModal(int id)
        {
            var disciplineDTO = _disciplineRepository.GetDisciplineById(id);

            var disciplineViewModel = disciplineDTO.GetDisciplineEditViewModel();


            return PartialView("_DisciplineEditModal", disciplineViewModel);
        }
    }
}
