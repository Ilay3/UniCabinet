using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Text.RegularExpressions;
using UniCabinet.Application.UseCases.DisciplineDetailUseCase;
using UniCabinet.Application.UseCases.GroupUseCase;
using UniCabinet.Core.DTOs.DisciplineDetailManagment;
using UniCabinet.Core.Models.ViewModel.Common;
using UniCabinet.Core.Models.ViewModel.DisciplineDetail;
using UniCabinet.Core.Models.ViewModel.Group;

namespace UniCabinet.Api.Controllers
{
    public class DisciplineDetailsController  : Controller
    {
        private readonly IMapper _mapper;

        public DisciplineDetailsController (IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> TeacherDetailsWithUserId(
            [FromServices] GetDetailByIdUseCase getDetailByIdUseCase,
            int disciplineId,
            string teacherId,
            int? courseId = null,
            int? semesterId = null,
            int? groupId = null,
            bool isPartial = false)
        {
            var details = await getDetailByIdUseCase.ExecuteAsync(disciplineId, teacherId, courseId, semesterId, groupId);
            var model = _mapper.Map<List<DisciplineDetailVM>>(details);

            var filterOptions = new FilterOptionsVM
            {
                Courses = model.Select(m => new SelectListItemVM { Value = m.CourseId.ToString(), Text = m.CourseName }).Distinct().ToList(),
                Semesters = model.Select(m => new SelectListItemVM { Value = m.SemesterId.ToString(), Text = m.SemesterName }).Distinct().ToList(),
                Groups = model.Select(m => new SelectListItemVM { Value = m.GroupId.ToString(), Text = m.GroupName }).Distinct().ToList()
            };

            var viewModel = new DisciplineDetailsModalVM
            {
                DisciplineId = disciplineId,
                TeacherId = teacherId,
                Details = model,
                FilterOptions = filterOptions,
                IsTeacherView = false
            };

            if (isPartial || Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_DisciplineDetailsTablePartial", viewModel);
            }

            return View("DisciplineDetailsListForDep", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> TeacherDetails(
        [FromServices] GetDetailByIdUseCase getDetailByIdUseCase,
        int disciplineId,
        int? courseId = null,
        int? semesterId = null,
        int? groupId = null,
        bool isPartial = false)
        {
            var teacherId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var details = await getDetailByIdUseCase.ExecuteAsync(disciplineId, teacherId, courseId, semesterId, groupId);
            var model = _mapper.Map<List<DisciplineDetailVM>>(details);

            var filterOptions = new FilterOptionsVM
            {
                Courses = model.Select(m => new SelectListItemVM { Value = m.CourseId.ToString(), Text = m.CourseName }).Distinct().ToList(),
                Semesters = model.Select(m => new SelectListItemVM { Value = m.SemesterId.ToString(), Text = m.SemesterName }).Distinct().ToList(),
                Groups = model.Select(m => new SelectListItemVM { Value = m.GroupId.ToString(), Text = m.GroupName }).Distinct().ToList()
            };

            var viewModel = new DisciplineDetailsModalVM
            {
                DisciplineId = disciplineId,
                Details = model,
                FilterOptions = filterOptions,
                IsTeacherView = true
            };

            if (isPartial || Request.Headers.XRequestedWith == "XMLHttpRequest")
            {
                return PartialView("_DisciplineDetailsTablePartial", viewModel);
            }

            return View("DisciplineDetailsListForTeach", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DetailInfo([FromServices] GetDetailsByTeacherUseCase getDetailsByTeacherUseCase, int detailId)
        {
            var detail = await getDetailsByTeacherUseCase.ExecuteAsyn(detailId);

            var model = _mapper.Map<DisciplineDetailInfoVM>(detail);

            return PartialView("_DisciplineDetailInfoModal", model);
        }
        [HttpGet]
        public async Task<IActionResult> AddDisciplineDetailModalAsync(
               int disciplineId,
               [FromServices] GetAddDisciplineDetailModalDataUseCase getDataUseCase)
        {
            var (courses, groups, semesters) = await getDataUseCase.ExecuteAsync();

            var vm = new DisciplineDetailAddVM
            {
                DisciplineId = disciplineId,
                Courses = courses.Select(c => new SelectListItemVM { Value = c.Id.ToString(), Text = c.Number.ToString() + " курс" }).Distinct().ToList(),
                Groups = groups.Select(g => new SelectListItemVM { Value = g.Id.ToString(), Text = g.Name}).Distinct().ToList(),
                Semesters = semesters.Select(s => new SelectListItemVM { Value = s.Id.ToString(), Text = "Семестр " + s.Number.ToString() }).Distinct().ToList(),
            };

            return PartialView("_AddDisciplineDetailModal", vm);
        }

        [HttpGet]
        public async Task<IActionResult> EditDisciplineDetailModalAsync(
            int detailId,
            [FromServices] GetEditDisciplineDetailModalDataUseCase getDataUseCase)
        {
            var (detail, courses, groups, semesters) = await getDataUseCase.ExecuteAsync(detailId);



            var vm = _mapper.Map<DisciplineDetailEditVM>(detail);
            vm.Courses = courses.Select(c => new SelectListItemVM { Value = c.Id.ToString(), Text = c.Number.ToString() + " курс" }).Distinct().ToList();
            vm.Groups = groups.Select(g => new SelectListItemVM { Value = g.Id.ToString(), Text = g.Name }).Distinct().ToList();
            vm.Semesters = semesters.Select(s => new SelectListItemVM { Value = s.Id.ToString(), Text = "Семестр " + s.Number.ToString() }).Distinct().ToList();

            return PartialView("_EditDisciplineDetailsModal", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddDisciplineDetailAsync(
            DisciplineDetailAddVM viewModel,
            [FromServices] AddDisciplineDetailUseCase addUseCase, [FromServices] GetAddDisciplineDetailModalDataUseCase getDataUseCase)
        {
            if (!ModelState.IsValid)
            {
                var (courses, groups, semesters) = await getDataUseCase.ExecuteAsync();

                var vm = new DisciplineDetailAddVM
                {
                    DisciplineId = viewModel.DisciplineId,
                    Courses = courses.Select(c => new SelectListItemVM { Value = c.Id.ToString(), Text = c.Number.ToString() + " курс" }).Distinct().ToList(),
                    Groups = groups.Select(g => new SelectListItemVM { Value = g.Id.ToString(), Text = g.Name }).Distinct().ToList(),
                    Semesters = semesters.Select(s => new SelectListItemVM { Value = s.Id.ToString(), Text = "Семестр " + s.Number.ToString() }).Distinct().ToList(),
                };

                return PartialView("_AddDisciplineDetailModal", vm);
            }

            var dto = _mapper.Map<DisciplineDetailDTO>(viewModel);
            dto.TeacherId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            await addUseCase.ExecuteAsync(dto);
            return RedirectToAction("TeacherDetails", "DisciplineDetails", new { disciplineId = viewModel.DisciplineId });
        }

        [HttpPost]
        public async Task<IActionResult> EditDisciplineDetailAsync(
            DisciplineDetailEditVM viewModel,
            [FromServices] EditDisciplineDetailUseCase editUseCase)
        {
            if (!ModelState.IsValid) return PartialView("_EditDisciplineDetailModal", viewModel);

            var dto = _mapper.Map<DisciplineDetailDTO>(viewModel);

            await editUseCase.ExecuteAsync(dto);
            return RedirectToAction("TeacherDetails", "DisciplineDetails", new { disciplineId = viewModel.DisciplineId });
        }
    }
}
