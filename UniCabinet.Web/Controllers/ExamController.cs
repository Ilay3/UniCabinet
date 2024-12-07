using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UniCabinet.Application.UseCases.ExamUseCase;
using UniCabinet.Core.Models.ViewModel.Exam;
using UniCabinet.Core.DTOs.ExamManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniCabinet.Controllers
{
    public class ExamController : Controller
    {
        private readonly IMapper _mapper;

        public ExamController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ExamList(int disciplineDetailId, [FromServices] GetExamListByDisciplineUseCase getExamListUseCase)
        {
            var examDTOList = await getExamListUseCase.ExecuteAsync(disciplineDetailId);
            var examListVM = _mapper.Map<List<ExamEditVM>>(examDTOList);

            ViewBag.DisciplineDetailId = disciplineDetailId;
            return View(examListVM);
        }

        [HttpGet]
        public IActionResult ExamAddModal(int id)
        {
            var viewModel = new ExamAddVM
            {
                DisciplineDetailId = id
            };
            return PartialView("_ExamAddModal", viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> AddExam(ExamAddVM viewModel, [FromServices] AddExamUseCase addExamUseCase)
        {
            if (ModelState.IsValid)
            {
                var examDTO = _mapper.Map<ExamDTO>(viewModel);
                var success = await addExamUseCase.ExecuteAsync(examDTO, ModelState);
                if (success)
                {
                    return RedirectToAction("ExamList", new { disciplineDetailId = viewModel.DisciplineDetailId });
                }
            }

            return PartialView("_ExamAddModal", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ExamEditModal(int id, [FromServices] GetExamForEditUseCase getExamForEditUseCase)
        {
            var examDTO = await getExamForEditUseCase.ExecuteAsync(id);
            if (examDTO == null)
            {
                return NotFound("Экзамен не найден.");
            }
            var examVM = _mapper.Map<ExamEditVM>(examDTO);
            return PartialView("_ExamEditModal", examVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditExam(ExamEditVM viewModel, [FromServices] UpdateExamUseCase updateExamUseCase)
        {
            if (ModelState.IsValid)
            {
                var examDTO = _mapper.Map<ExamDTO>(viewModel);
                var success = await updateExamUseCase.ExecuteAsync(examDTO, ModelState);
                if (success)
                {
                    return RedirectToAction("ExamList", new { disciplineDetailId = viewModel.DisciplineDetailId });
                }
            }

            return PartialView("_ExamEditModal", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ExamResults(int examId, int disciplineDetailId, [FromServices] GetExamResultsUseCase getExamResultsUseCase)
        {
            var resultsDTO = await getExamResultsUseCase.ExecuteAsync(examId);
            var resultVM = new ExamResultsVM
            {
                ExamId = examId,
                DisciplineDetailId = disciplineDetailId,
                Students = _mapper.Map<List<ExamResultItemVM>>(resultsDTO)
            };

            // Ищет /Views/Exam/ExamResults.cshtml
            return View(resultVM);
        }

        [HttpPost]
        public async Task<IActionResult> SaveExamResults(ExamResultsVM viewModel, [FromServices] SaveExamResultsUseCase saveExamResultsUseCase)
        {
            if (!ModelState.IsValid)
            {
                return View("ExamResults", viewModel);
            }

            var dtoList = _mapper.Map<List<ExamResultDTO>>(viewModel.Students);
            foreach (var dto in dtoList)
            {
                dto.ExamId = viewModel.ExamId;
            }

            var success = await saveExamResultsUseCase.ExecuteAsync(dtoList, ModelState);
            if (!success)
            {
                return View("ExamResults", viewModel);
            }

            return RedirectToAction("ExamList", new { disciplineDetailId = viewModel.DisciplineDetailId });
        }
    }
}
