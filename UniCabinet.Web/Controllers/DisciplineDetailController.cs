using Microsoft.AspNetCore.Mvc;
using UniCabinet.Application.Interfaces;
using UniCabinet.Application.Interfaces.Repository;
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

        public DisciplineDetailController(
            IDisciplineDetailRepository disciplineDetailRepository,
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
            _courseRepository = courseRepository;
        }

        [HttpGet]
        public async Task<IActionResult> DisciplineDetailsList()
        {
            var disciplineDListDTO = await _disciplineDetailRepository.GetAllDisciplineDetails();

            var tasks = disciplineDListDTO.Select(async dto =>
            {
                var discipline = await _disciplineRepository.GetDisciplineById(dto.DisciplineId);
                var semester = await _semesterRepository.GetSemesterById(dto.SemesterId);
                var group = await _groupRepository.GetGroupById(dto.GroupId);
                var teacher = await _userRepository.GetUserById(dto.TeacherId);
                var course = await _courseRepository.GetCourseById(group.CourseId);

                return dto.GetDisciplineDetailViewModel(
                    semester.Number,
                    course.Number,
                    group.Name,
                    discipline.Name,
                    teacher.FirstName,
                    teacher.LastName,
                    teacher.Patronymic);
            });

            var disciplineDListViewModel = await Task.WhenAll(tasks);

            return View(disciplineDListViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DisciplineDetailAddModal()
        {
            var disciplineList = await _disciplineRepository.GetAllDisciplines();
            var semesterList = await _semesterRepository.GetAllSemesters();
            var groupList = await _groupRepository.GetAllGroups();
            var teacherList = await _userRepository.GetAllUsersWithRolesAsync();
            var courseList = await _courseRepository.GetAllCourse();

            ViewBag.DisciplineList = disciplineList;
            ViewBag.SemesterList = semesterList;
            ViewBag.Group = groupList;
            ViewBag.Teacher = teacherList;
            ViewBag.Course = courseList;

            var viewModel = new DisciplineDetailAddViewModel();

            return PartialView("_DisciplineDetailAddModal", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddDisciplineDetail(DisciplineDetailAddViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // Перезагрузим списки в ViewBag для повторного отображения формы
                await LoadSelectListsAsync();
                return PartialView("_DisciplineDetailAddModal", viewModel);
            }

            var disciplineDetailDTO = viewModel.GetDisciplineDetailDTO();

            await _disciplineDetailRepository.AddDisciplineDetailAsync(disciplineDetailDTO);

            return Json(new { success = true });
        }

        // Метод для загрузки списков в ViewBag
        private async Task LoadSelectListsAsync()
        {
            ViewBag.DisciplineList = await _disciplineRepository.GetAllDisciplines();
            ViewBag.SemesterList = await _semesterRepository.GetAllSemesters();
            ViewBag.Group = await _groupRepository.GetAllGroups();
            ViewBag.Teacher = await _userRepository.GetAllUsersWithRolesAsync();
            ViewBag.Course = await _courseRepository.GetAllCourse();
        }
    }
}
