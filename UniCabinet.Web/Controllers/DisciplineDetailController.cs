using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniCabinet.Application.Interfaces;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Domain.Entities;
using UniCabinet.Web.Extension.DisciplineDetail;
using UniCabinet.Web.Mapping.DisciplineDetail;
using UniCabinet.Web.ViewModel.DisiciplineDetail;

namespace UniCabinet.Web.Controllers
{
    public class DisciplineDetailController : Controller
    {
        private readonly IDisciplineDetailRepository _disciplineDetailRepository;
        private readonly IDisciplineRepository _disciplineRepository;
        private readonly ISemesterRepository _semesterRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;
        public DisciplineDetailController(IDisciplineDetailRepository disciplineDetailRepository, 
            IDisciplineRepository disciplineRepository,
            ISemesterRepository semesterRepository,
            IGroupRepository groupRepository,
            IUserRepository userRepository,
            ICourseRepository courseRepository)
        {
            _disciplineDetailRepository = disciplineDetailRepository;
            _disciplineRepository = disciplineRepository;
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _semesterRepository = semesterRepository;
            _disciplineDetailRepository = disciplineDetailRepository;
            _courseRepository = courseRepository;
        }

        [HttpGet]
        public async Task<IActionResult> DisciplineDetailsList()
        {
            var disciplineDListDTO = await _disciplineDetailRepository.GetAllDisciplineDetails();

            var tasks = disciplineDListDTO
                .Select(async dto =>
                {
                    var discipline = await _disciplineRepository.GetDisciplineById(dto.DisciplineId);
                    var semester = await _semesterRepository.GetSemesterById(dto.SemesterId);
                    var group = await _groupRepository.GetGroupById(dto.GroupId);
                    var teacher = await _userRepository.GetUserById(dto.TeacherId);
                    var course = await _courseRepository.GetCourseById(group.CourseId);

                    return dto.GetDisciplineDetailViewModel(semester.Number, course.Number, group.Name, discipline.Name, 
                        teacher.FirstName, teacher.LastName, teacher.Patronymic);
                })
                .ToList();

            var disciplineDListViewModel = await Task.WhenAll(tasks);

            return View(disciplineDListViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DisciplineDetailAddModal()
        {
            var disciplineList = await _disciplineRepository.GetAllDisciplines();
            var semesterList = await _semesterRepository.GetAllSemesters();
            var group = await _groupRepository.GetAllGroups();
            var teacher = await _userRepository.GetAllUsersWithRolesAsync();
            var course = await _courseRepository.GetAllCourse();

            ViewBag.DisciplineList = disciplineList;
            ViewBag.SemesterList = semesterList;
            ViewBag.Group = group;
            ViewBag.Teacher = teacher;
            ViewBag.Course = course;

            return PartialView("_DisciplineDetailAddModal");
        }

        [HttpPost]
        public async Task<IActionResult> AddDisciplineDetail(DisciplineDetailAddViewModel viewModel)
        {
            if (!ModelState.IsValid) return PartialView("_DisciplineDetailAddModal", viewModel);

            var disciplineDViewModel = viewModel.GetDisciplineDetailDTO();
            
            await _disciplineDetailRepository.AddDisciplineDetailAsync(disciplineDViewModel);

            return RedirectToAction("DisciplineDetailsList");
        }
    }
}
