using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UniCabinet.Application.UseCases.LectureUseCase;
using UniCabinet.Core.DTOs.LectureManagement;
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
    public async Task<IActionResult> AddLectureAsync(
        LectureAddVM viewModal,
        [FromServices] AddLectureUseCase addLectureUseCase)
    {
        viewModal.PointsCount = 5;
        if (ModelState.IsValid)
        {
            var lectureDTO = _mapper.Map<LectureDTO>(viewModal);

            var success = await addLectureUseCase.ExecuteAsync(lectureDTO, ModelState);

            if (success)
            {
                return RedirectToAction("LecturesList", new { id = viewModal.DisciplineDetailId });
            }
        }

        return PartialView("_LectureAddModal", viewModal);
    }

    [HttpGet]
    public IActionResult LectureEditModal(int id, [FromServices] GetLectureForEditUseCase getLectureForEditUseCase)
    {
        var lectureDTO = getLectureForEditUseCase.ExecuteAsync(id);
        var lectureVM = _mapper.Map<LectureEditVM>(lectureDTO);

        return PartialView("_LectureEditModal", lectureVM);
    }

    [HttpPost]
    public async Task<IActionResult> EditLectureAsync(
        LectureEditVM viewModal,
        [FromServices] UpdateLectureUseCase updateLectureUseCase)
    {
        if (ModelState.IsValid)
        {
            var lectureDTO = _mapper.Map<LectureDTO>(viewModal);

           await updateLectureUseCase.ExecuteAsync(lectureDTO);
            var disciplineDId = viewModal.DisciplineDetailId;

            return RedirectToAction("LecturesList", new { id = disciplineDId });
        }

        return PartialView("_LectureEditModal", viewModal);
    }

    [HttpGet]
    public async Task<IActionResult> LectureAttendance(
        int lectureId,
        [FromServices] GetLectureAttendanceUseCase getLectureAttendanceUseCase)
    {
        var result = await getLectureAttendanceUseCase.ExecuteAsync(lectureId);
        var attendanceVM = _mapper.Map<LectureAttendanceVM>(result);
        if (attendanceVM == null)
        {
            return NotFound();
        }
        return View("LectureAttendance", attendanceVM);
    }

    [HttpPost]
    public async Task<IActionResult> SaveAttendanceAsync(
        LectureAttendanceVM viewModal,
        [FromServices] SaveLectureAttendanceUseCase saveLectureAttendanceUseCase)
    {
        var lectureAttendanceDTO = _mapper.Map< LectureAttendanceDTO >(viewModal);
        await saveLectureAttendanceUseCase.ExecuteAsync(lectureAttendanceDTO);
        return RedirectToAction("LecturesList", new { id = viewModal.DisciplineDetailId });
    }
}
