using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.Entites;
using UniCabinet.Core.Models.ViewModel.Discipline;

namespace UniCabinet.Web.Controllers
{
    public class DisciplineController : Controller
    {
        private readonly IDisciplineRepository _disciplineRepository;
        private readonly IMapper _mapper;

        public DisciplineController(IDisciplineRepository disciplineRepository, IMapper mapper)
        {
            _disciplineRepository = disciplineRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult DisciplinesList()
        {
            var disciplineDTOs = _disciplineRepository.GetAllDisciplines();
            var disciplineVMs = _mapper.Map<List<DisciplineListVM>>(disciplineDTOs);

            return View(disciplineVMs);
        }

        [HttpGet]
        public IActionResult DisciplineAddModal()
        {
            var viewModal = new DisciplineAddVM();
            return PartialView("_DisciplineAddModal", viewModal);
        }

        [HttpPost]
        public IActionResult AddDiscipline(DisciplineAddVM viewModal)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_DisciplineAddModal", viewModal);
            }

            var disciplineDTO = _mapper.Map<DisciplineDTO>(viewModal);
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

            var viewModal = _mapper.Map<DisciplineEditVM>(disciplineDTO);
            return PartialView("_DisciplineEditModal", viewModal);
        }

        [HttpPost]
        public IActionResult EditDiscipline(DisciplineEditVM viewModal)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_DisciplineEditModal", viewModal);
            }

            var disciplineDTO = _mapper.Map<DisciplineDTO>(viewModal);
            _disciplineRepository.UpdateDiscipline(disciplineDTO);

            return Json(new { success = true });
        }
    }
}
