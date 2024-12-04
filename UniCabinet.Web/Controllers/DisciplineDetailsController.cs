using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UniCabinet.Application.UseCases.DisciplineDetailUseCase;
using UniCabinet.Core.Models.ViewModel.Common;
using UniCabinet.Core.Models.ViewModel.DisciplineDetail;

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
        public async Task<IActionResult> TeacherDetails(
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
                FilterOptions = filterOptions
            };

            if (isPartial || Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_DisciplineDetailsTablePartial", viewModel);
            }

            return View("DisciplineDetailsListForDep", viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> DetailInfo([FromServices] GetDetailsByTeacherUseCase getDetailsByTeacherUseCase, int detailId)
        {
            var detail = await getDetailsByTeacherUseCase.ExecuteAsyn(detailId);

            var model = _mapper.Map<DisciplineDetailInfoVM>(detail);

            return PartialView("_DisciplineDetailInfoModal", model);
        }
    }
}
