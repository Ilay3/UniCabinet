using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UniCabinet.Application.Interfaces;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Application.UseCases;
using UniCabinet.Core.DTOs;
using UniCabinet.Core.DTOs.Entites;
using UniCabinet.Core.Models.ViewModel;
using UniCabinet.Core.Models.ViewModel.Lecture;

public class LectureController : Controller
{
    private readonly ILectureRepository _lectureRepository;
    private readonly IDisciplineDetailRepository _disciplineDetailRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILectureVisitRepository _lectureVisitRepository;
    private readonly IDisciplineRepository _disciplineRepository;
    private readonly IMapper _mapper;

    public LectureController(ILectureRepository lectureRepository,
        IDisciplineDetailRepository disciplineDetailRepository,
        IUserRepository userRepository,
        ILectureVisitRepository lectureVisitRepository,
        IDisciplineRepository disciplineRepository,
        IMapper mapper)
    {
        _lectureRepository = lectureRepository;
        _disciplineDetailRepository = disciplineDetailRepository;
        _userRepository = userRepository;
        _lectureVisitRepository = lectureVisitRepository;
        _disciplineRepository = disciplineRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public  async Task<IActionResult> LecturesListAsync(int id, [FromServices] LecturesListUseCase lecturesListUseCase)
    {

        var result = await lecturesListUseCase.ExecuteAsync(id);

        ViewBag.Discipline = result.DisciplineName;
        ViewBag.DisciplineDetaildId = id;
        ViewBag.MaxLectures = result.MaxLectures;

        var lectureListVM = _mapper.Map<List<LectureListVM>>(result.LectureDTO);

        return View(lectureListVM);
    }

    [HttpGet]
    public IActionResult LectureAddModal(int id)
    {
        var viewModal = new LectureAddVM
        {
            DisciplineDetailId = id
        };
        return PartialView("_LectureAddModal", viewModal);
    }

    [HttpPost]
    public IActionResult AddLecture(LectureAddVM viewModal)
    {
        if (ModelState.IsValid)
        {
            var existingLecturesCount = _lectureRepository.GetLectureCountByDisciplineDetailId(viewModal.DisciplineDetailId);
            var disciplineDetail = _disciplineDetailRepository.GetDisciplineDetailById(viewModal.DisciplineDetailId);
            int maxLectures = disciplineDetail.LectureCount;

            if (existingLecturesCount >= maxLectures)
            {
                ModelState.AddModelError("", "Достигнуто максимальное количество лекций.");
                return PartialView("_LectureAddModal", viewModal);
            }

            var lectureDTO = _mapper.Map<LectureDTO>(viewModal);
            _lectureRepository.AddLecture(lectureDTO);

            return Json(new { success = true, redirectUrl = Url.Action("LecturesList", new { id = viewModal.DisciplineDetailId }) });
        }

        return PartialView("_LectureAddModal", viewModal);
    }


    [HttpGet]
    public IActionResult LectureEditModal(int id)
    {
        var lectureDTO = _lectureRepository.GetLectureById(id);
        var lectureVM = _mapper.Map<LectureEditVM>(lectureDTO);

        return PartialView("_LectureEditModal", lectureVM);
    }

    [HttpPost]
    public IActionResult EditLecture(LectureEditVM viewModal)
    {
        if (ModelState.IsValid)
        {
            var lectureDTO = _mapper.Map<LectureDTO>(viewModal);
            _lectureRepository.UpdateLecture(lectureDTO);

            var disciplineDId = viewModal.DisciplineDetailId;

            return Json(new { success = true, redirectUrl = Url.Action("LecturesList", new { id = disciplineDId }) });
        }

        return PartialView("_LectureEditModal", viewModal);
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

        var attendanceVM = new LectureAttendanceVM
        {
            LectureId = lectureId,
            DisciplineDetailId = disciplineDetailId,
            LectureNumber = lecture.Number, // Заполняем номер лекции
            DisciplineName = _disciplineRepository.GetDisciplineById(disciplineDetail.DisciplineId).Name,
            Students = students.Select(s => new StudentAttendanceVM
            {
                StudentId = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Patronymic = s.Patronymic,
                IsPresent = existingVisits.ContainsKey(s.Id) ? existingVisits[s.Id].isVisit : false
            }).ToList()
        };

        return View("LectureAttendance", attendanceVM);
    }



    [HttpPost]
    public IActionResult SaveAttendance(LectureAttendanceVM viewModal)
    {
        foreach (var studentAttendance in viewModal.Students)
        {
            var lectureVisitDTO = new LectureVisitDTO
            {
                LectureId = viewModal.LectureId,
                StudentId = studentAttendance.StudentId,
                isVisit = studentAttendance.IsPresent,
                PointsCount = studentAttendance.IsPresent ? CalculatePointsForLecture(viewModal.LectureId) : 0
            };

            _lectureVisitRepository.AddOrUpdateLectureVisit(lectureVisitDTO);
        }

        return RedirectToAction("LecturesList", new { id = viewModal.DisciplineDetailId });
    }


    private decimal CalculatePointsForLecture(int lectureId)
    {
        // Реализуйте логику расчета баллов
        var lecture = _lectureRepository.GetLectureById(lectureId);
        return lecture.PointsCount;
    }


}
