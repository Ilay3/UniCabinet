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
            var disciplineDListDTO = _disciplineDetailRepository.GetAllDisciplineDetails();

            var disciplineDListViewModel = disciplineDListDTO
                .Select(dd => dd.GetDisciplineDetailViewModel())
                .ToList();

            return View(disciplineDListViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DisciplineDetailAddModal(int? id)
        {
            // Await asynchronous repository methods
            var disciplineList =  _disciplineRepository.GetAllDisciplines();
            var semesterList =  _semesterRepository.GetAllSemesters();
            var groupList =  _groupRepository.GetAllGroups();
            var teacherList = await _userRepository.GetAllUsersWithRolesAsync();
            var courseList = await _courseRepository.GetAllCourseAsync();

            // Assign the awaited results to ViewBag
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
                // Reload the select lists asynchronously for re-rendering the form
                await LoadSelectListsAsync();
                return PartialView("_DisciplineDetailAddModal", viewModel);
            }

            var disciplineDetailDTO = viewModel.GetDisciplineDetailDTO();

            _disciplineDetailRepository.AddDisciplineDetail(disciplineDetailDTO);

            return Json(new { success = true });
        }

        private async Task LoadSelectListsAsync()
        {
            // Await asynchronous repository methods
            ViewBag.DisciplineList =  _disciplineRepository.GetAllDisciplines();
            ViewBag.SemesterList = _semesterRepository.GetAllSemesters();
            ViewBag.Group =  _groupRepository.GetAllGroups();
            ViewBag.Teacher = await _userRepository.GetAllUsersWithRolesAsync();
            ViewBag.Course = await _courseRepository.GetAllCourseAsync();
        }
    }
}
