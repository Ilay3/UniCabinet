using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UniCabinet.Application.UseCases.AdminUseCase;
using UniCabinet.Application.UseCases.SpecializationUseCase;
using UniCabinet.Core.DTOs.SpecializationManagement;
using UniCabinet.Core.Models.ViewModel.Specialization;

namespace UniCabinet.Web.Controllers
{
    public class SpecializationController : Controller
    {
        private readonly IMapper _mapper;

        public SpecializationController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IActionResult> GetDataSpecialization([FromServices] GetTeacherSpecializationUseCase getDataSpecializationUseCase)
        {
            var result = await getDataSpecializationUseCase.ExecuteAsync();
            var viewModel = _mapper.Map<List<SpecializationListVM>>(result);

            return View("SpecializationList", viewModel);
        }
        [HttpGet]
        public IActionResult AddSpecialization()
        {
            return PartialView("_AddSpecializationModal");
        }

        [HttpPost]
        public async Task<IActionResult> AddSpecialization(
            SpecializationAddVM model,
            [FromServices] AddSpecializationUseCase addSpecializationUseCase)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_AddSpecializationModal", model);
            }

            try
            {
                var specializationDTO = _mapper.Map<SpecializationAddDTO>(model);
                await addSpecializationUseCase.ExecuteAsync(specializationDTO);

                return RedirectToAction("GetDataSpecialization");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ошибка при добавлении специальности: " + ex.Message);
                return PartialView("_AddSpecializationModal", model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditSpecialization(
            int id,
            [FromServices] GetSpecializationByIdUseCase getSpecializationByIdUseCase)
        {
            var specialization = await getSpecializationByIdUseCase.ExecuteAsync(id);
            if (specialization == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<SpecializationEditVM>(specialization);
            return PartialView("_EditSpecializationModal", model);
        }

        [HttpPost]
        public async Task<IActionResult> EditSpecialization(
            SpecializationEditVM model,
            [FromServices] UpdateSpecializationUseCase updateSpecializationUseCase)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_EditSpecializationModal", model);
            }

            try
            {
                var specializationDTO = _mapper.Map<SpecializationEditDTO>(model);
                await updateSpecializationUseCase.ExecuteAsync(specializationDTO);

                return RedirectToAction("GetDataSpecialization");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ошибка при редактировании специальности: " + ex.Message);
                return PartialView("_EditSpecializationModal", model);
            }
        }
    }
}
