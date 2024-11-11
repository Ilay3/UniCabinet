using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UniCabinet.Application.UseCases.LectureUseCase;
using UniCabinet.Core.Models.ViewModel.Lecture;

public class LectureController : Controller
{
    private readonly IMapper _mapper;

    public LectureController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> LecturesListAsync(int id, [FromServices] GetLecturesListDataUseCase lecturesListUseCase)
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
    public IActionResult AddLecture(
        LectureAddVM viewModal,
        [FromServices] AddLectureUseCase addLectureUseCase)
    {
        if (ModelState.IsValid)
        {
            var success = addLectureUseCase.Execute(viewModal, ModelState);

            if (success)
            {
                return Json(new { success = true, redirectUrl = Url.Action("LecturesList", new { id = viewModal.DisciplineDetailId }) });
            }
        }

        return PartialView("_LectureAddModal", viewModal);
    }

    [HttpGet]
    public IActionResult LectureEditModal(int id, [FromServices] GetLectureForEditUseCase getLectureForEditUseCase)
    {
        var lectureVM = getLectureForEditUseCase.Execute(id);
        return PartialView("_LectureEditModal", lectureVM);
    }

    [HttpPost]
    public IActionResult EditLecture(
        LectureEditVM viewModal,
        [FromServices] UpdateLectureUseCase updateLectureUseCase)
    {
        if (ModelState.IsValid)
        {
            updateLectureUseCase.Execute(viewModal);
            var disciplineDId = viewModal.DisciplineDetailId;

            return Json(new { success = true, redirectUrl = Url.Action("LecturesList", new { id = disciplineDId }) });
        }

        return PartialView("_LectureEditModal", viewModal);
    }

    [HttpGet]
    public IActionResult LectureAttendance(
        int lectureId,
        [FromServices] GetLectureAttendanceUseCase getLectureAttendanceUseCase)
    {
        var attendanceVM = getLectureAttendanceUseCase.Execute(lectureId);
        if (attendanceVM == null)
        {
            return NotFound();
        }
        return View("LectureAttendance", attendanceVM);
    }

    [HttpPost]
    public IActionResult SaveAttendance(
        LectureAttendanceVM viewModal,
        [FromServices] SaveLectureAttendanceUseCase saveLectureAttendanceUseCase)
    {
        saveLectureAttendanceUseCase.Execute(viewModal);
        return RedirectToAction("LecturesList", new { id = viewModal.DisciplineDetailId });
    }
}
