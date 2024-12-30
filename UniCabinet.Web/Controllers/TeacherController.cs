using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UniCabinet.Application.UseCases.DisciplineUseCase;
using UniCabinet.Application.UseCases.TeacherUseCase;
using UniCabinet.Core.Models.ViewModel.Discipline;
using UniCabinet.Core.Models.ViewModel.Group;
using UniCabinet.Core.Models.ViewModel.Student;
using UniCabinet.Core.Models.ViewModel.Teacher;
namespace UniCabinet.Web.Controllers;

public class TeacherController : Controller
{
    private readonly IMapper _mapper;
    public TeacherController(
        IMapper mapper
        )
    {
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Dashboard([FromServices] GetDisciplinesListUseCase getDisciplinesListUseCase)
    {
        var teacherId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var result = await getDisciplinesListUseCase.ExecuteAsync(teacherId);
        var disciplineVMs = _mapper.Map<List<DisciplineListVM>>(result);
        return View(disciplineVMs);
    }

    [HttpGet]
    public async Task<IActionResult> GetGroupsForDisciplineModal(int disciplineId, DateTime? filetDate,
        [FromServices] GetGroupsForDisciplineUseCase getGroupsForDisciplineUseCase)
    {
        var groups = await getGroupsForDisciplineUseCase.ExecuteAsync(disciplineId, filetDate);
        var groupVMs = _mapper.Map<List<GroupListVM>>(groups);
        ViewBag.DisciplineId = disciplineId;
        ViewBag.FilteredDate = filetDate;
        return PartialView("_GroupsForDisciplineModal", groupVMs);
    }


    [HttpGet]
    public async Task<IActionResult> GroupStudentsProgress(
        [FromServices] GroupStudentsProgressUseCase groupStudentsProgressUseCase,
        int groupId,
        int disciplineId)
    {
        var result = await groupStudentsProgressUseCase.ExecuteAsync(groupId, disciplineId);

        return View(_mapper.Map<GroupStudentsProgressPageVM>(result));
    }


    [HttpGet]
    public async Task<IActionResult> StudentDetail([FromServices] StudentDetailUseCase studentDetailUseCase,
        string studentId, int disciplineId, int groupId)
    {
        var result = await studentDetailUseCase.ExecuteAsync(studentId, disciplineId, groupId);
        return View(_mapper.Map<List<StudentProgressVM>>(result));
    }
}