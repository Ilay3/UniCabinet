using Microsoft.AspNetCore.Mvc;
using System;
using UniCabinet.Application.Interfaces;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Application.Interfaces.Services;
using UniCabinet.Domain.DTO;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Repository;
using UniCabinet.Web.Extension.Lecture;
using UniCabinet.Web.Mapping.Lecture;
using UniCabinet.Web.ViewModel.Lecture;

public class LectureController : Controller
{
    private readonly ILectureRepository _lectureRepository;
    private readonly ILectureService _lectureService;
    private readonly IDisciplineDetailRepository _disciplineDetailRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILectureVisitRepository _lectureVisitRepository;

    public LectureController(ILectureRepository lectureRepository, 
        ILectureService lectureService, 
        IDisciplineDetailRepository disciplineDetailRepository,
        IUserRepository userRepository,
        ILectureVisitRepository lectureVisitRepository)
    {
        _lectureRepository = lectureRepository;
        _lectureService = lectureService;
        _disciplineDetailRepository = disciplineDetailRepository;
        _userRepository = userRepository;
        _lectureVisitRepository = lectureVisitRepository;
    }

    [HttpGet]
    public IActionResult LecturesList(int id)
    {
        var lectureListDTO = _lectureRepository.GetLectureListByDisciplineDetailId(id);
        var disciplineDetail = _disciplineDetailRepository.GetDisciplineDetailById(id);

        var lectureListViewModel = lectureListDTO
            .Select(l => l.GetLectureViewModel())
            .ToList();

        ViewBag.Discipline = _lectureService.GetDisciplineById(id);
        ViewBag.DisciplineDetaildId = id;
        ViewBag.MaxLectures = disciplineDetail.LectureCount;


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
            var existingLecturesCount = _lectureRepository.GetLectureCountByDisciplineDetailId(viewModel.DisciplineDetailId);
            var disciplineDetail = _disciplineDetailRepository.GetDisciplineDetailById(viewModel.DisciplineDetailId);
            int maxLectures = disciplineDetail.LectureCount;

            if (existingLecturesCount >= maxLectures)
            {
                ModelState.AddModelError("", "Достигнуто максимальное количество лекций.");
                return PartialView("_LectureAddModal", viewModel);
            }

            var lectureDTO = viewModel.GetLectureDTO();
            _lectureRepository.AddLecture(lectureDTO);

            return Json(new { success = true, redirectUrl = Url.Action("LecturesList", new { id = viewModel.DisciplineDetailId }) });
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

    [HttpGet]
    public IActionResult LectureAttendance(int lectureId)
    {
        var lecture = _lectureRepository.GetLectureById(lectureId);
        if (lecture == null)
        {
            return NotFound();
        }

        int disciplineDetailId = lecture.DisciplineDetailId;

        var disciplineDetail = _disciplineDetailRepository.GetDisciplineDetailById(disciplineDetailId);
        int groupId = disciplineDetail.GroupId;

        var students = _userRepository.GetStudentsByGroupId(groupId);

        // Получаем уже сохраненные посещения для этой лекции
        var existingVisits = _lectureVisitRepository.GetLectureVisitsByLectureId(lectureId)
            .ToDictionary(lv => lv.StudentId, lv => lv);

        var attendanceViewModel = new LectureAttendanceViewModel
        {
            LectureId = lectureId,
            DisciplineDetailId = disciplineDetailId,
            Students = students.Select(s => new StudentAttendanceViewModel
            {
                StudentId = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Patronymic = s.Patronymic,
                IsPresent = existingVisits.ContainsKey(s.Id) ? existingVisits[s.Id].isVisit : false
            }).ToList()
        };

        return View(attendanceViewModel);
    }


    [HttpPost]
    public IActionResult SaveAttendance(LectureAttendanceViewModel viewModel)
    {
        foreach (var studentAttendance in viewModel.Students)
        {
            var lectureVisitDTO = new LectureVisitDTO
            {
                LectureId = viewModel.LectureId,
                StudentId = studentAttendance.StudentId,
                isVisit = studentAttendance.IsPresent,
                PointsCount = studentAttendance.IsPresent ? CalculatePointsForLecture(viewModel.LectureId) : 0
            };

            _lectureVisitRepository.AddOrUpdateLectureVisit(lectureVisitDTO);
        }

        return RedirectToAction("LecturesList", new { id = viewModel.DisciplineDetailId });
    }


    private decimal CalculatePointsForLecture(int lectureId)
    {
        // Реализуйте логику расчета баллов
        var lecture = _lectureRepository.GetLectureById(lectureId);
        return lecture.PointsCount;
    }


}
