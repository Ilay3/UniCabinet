using Microsoft.AspNetCore.Mvc;
using System;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Application.Interfaces.Services;
using UniCabinet.Web.Extension.Lecture;
using UniCabinet.Web.Mapping.Lecture;
using UniCabinet.Web.ViewModel.Lecture;

public class LectureController : Controller
{
    private readonly ILectureRepository _lectureRepository;
    private readonly ILectureService _lectureService;
    public LectureController(ILectureRepository lectureRepository, ILectureService lectureService)
    {
        _lectureRepository = lectureRepository;
        _lectureService = lectureService;
    }

    [HttpGet]
    public IActionResult LecturesList(int id)
    {
        var lectureListDTO = _lectureRepository.GetLectureListByDisciplineDetailId(id);

        var lectureListViewModel = lectureListDTO
            .Select(l => l.GetLectureViewModel())
            .ToList();

        ViewBag.Discipline = _lectureService.GetDisciplineById(id);
        ViewBag.DisciplineDetaildId = id;

        return View(lectureListViewModel);
    }

    [HttpGet]
    public IActionResult LectureAddModal(int id)
    {
        var viewModel = new LectureAddViewModel
        {
            DisciplineDetailId = id
        };
        return PartialView("_LectureAddModal", viewModel);
    }

    [HttpPost]
    public IActionResult AddLecture(LectureAddViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var lectureDTO = viewModel.GetLectureDTO();
            _lectureRepository.AddLecture(lectureDTO);

            var disciplineDId = viewModel.DisciplineDetailId;

            return Json(new { success = true, redirectUrl = Url.Action("LecturesList", new { id = disciplineDId }) });
        }

        return PartialView("_LectureAddModal", viewModel);
    }

    [HttpGet]
    public IActionResult LectureEditModal(int id)
    {
        var lectureDTO = _lectureRepository.GetLectureById(id);
        var lectureViewModel = lectureDTO.GetLectureEditViewModel();

        return PartialView("_LectureEditModal", lectureViewModel);
    }

    [HttpPost]
    public IActionResult EditLecture(LectureEditViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var lectureDTO = viewModel.GetLectureDTO();
            _lectureRepository.UpdateLecture(lectureDTO);

            var disciplineDId = viewModel.DisciplineDetailId;

            return Json(new { success = true, redirectUrl = Url.Action("LecturesList", new { id = disciplineDId }) });
        }

        return PartialView("_LectureEditModal", viewModel);
    }
}
