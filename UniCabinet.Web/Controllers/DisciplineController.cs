﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UniCabinet.Application.UseCases.DisciplineUseCase;
using UniCabinet.Core.DTOs.Entites;
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
            var result = await getDisciplinesListUseCase.ExecuteAsync();
            var disciplineVMs = _mapper.Map<List<DisciplineListVM>>(result);

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
            var disciplineDTO = _mapper.Map<DisciplineDTO>(viewModel);

            var success = await addDisciplineUseCase.ExecuteAsync(disciplineDTO, ModelState);

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
            var disciplineDTO = await getDisciplineForEditUseCase.ExecuteAsync(id);
            if (disciplineDTO == null)
            {
                return NotFound();
            }
            var disciplineVM = _mapper.Map<DisciplineEditVM>(disciplineDTO);

            return PartialView("_DisciplineEditModal", disciplineVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditDisciplineAsync(
            DisciplineEditVM viewModel,
            [FromServices] UpdateDisciplineUseCase updateDisciplineUseCase)
        {
            var disciplineDTO = _mapper.Map<DisciplineDTO>(viewModel);

            var success = await updateDisciplineUseCase.ExecuteAsync(disciplineDTO, ModelState);

            if (success)
            {
                return Json(new { success = true });
            }

            return PartialView("_DisciplineEditModal", viewModel);
        }
    }
}
