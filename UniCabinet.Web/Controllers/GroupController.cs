using Microsoft.AspNetCore.Mvc;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Domain.DTO;
using UniCabinet.Web.Extension;
using UniCabinet.Web.Mapping;
using UniCabinet.Web.ViewModel;

namespace UniCabinet.Web.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupRepository _groupRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ISemesterRepository _semesterRepository;

        public GroupController(IGroupRepository groupRepository, ICourseRepository courseRepository, ISemesterRepository semesterRepository)
        {
            _groupRepository = groupRepository;
            _courseRepository = courseRepository;
            _semesterRepository = semesterRepository;
        }

        public IActionResult GroupsList()
        {
            var groupListDTO = _groupRepository.GetAllGroups();

            var groupViewModelList = new List<GroupViewModel>();

            foreach (var dto in groupListDTO)
            {
                var courseGroup = _courseRepository.GetCourseById(dto.CourseId);
                var semesterGroup = _semesterRepository.GetSemesterById(dto.SemesterId);

                var groupViewModel = dto.GetGroupViewModel(courseGroup.Number, semesterGroup.Number);
                groupViewModelList.Add(groupViewModel);
            }

            return View(groupViewModelList);
        }


        public IActionResult GroupAddModal()
        {
            var currentSemester = _semesterRepository.GetCurrentSemester(DateTime.Now);
            var viewModel = new GroupCreateEditViewModel
            {
                CurrentSemester = currentSemester != null ? $"Семестр №{currentSemester.Number}" : "Не определён"
            };
            return PartialView("_GroupAddModal", viewModel);
        }

        public IActionResult GroupEditModal(int id)
        {
            var groupDTO = _groupRepository.GetGroupById(id);
            var groupViewModel = groupDTO.GetGroupCreateEditViewModel();

            var currentSemester = _semesterRepository.GetCurrentSemester(DateTime.Now);
            groupViewModel.CurrentSemester = currentSemester != null ? $"Семестр №{currentSemester.Number}" : "Не определён";

            if (groupViewModel.TypeGroup == "Очно")
            {
                groupViewModel.TypeGroup = "1";
            }
            else if (groupViewModel.TypeGroup == "Заочно")
            {
                groupViewModel.TypeGroup = "2";
            }

            return PartialView("_GroupEditModal", groupViewModel);
        }


        [HttpPost]
        public IActionResult AddGroup(GroupCreateEditViewModel viewModel)
        {
            if (!ModelState.IsValid) return PartialView("_GroupAddModal", viewModel);

            if (viewModel.TypeGroup == "1")
            {
                viewModel.TypeGroup = "Очно";
            }

            if (viewModel.TypeGroup == "2")
            {
                viewModel.TypeGroup = "Заочно";
            }

            // Определение текущего семестра
            SemesterDTO currentSemester;
            try
            {
                currentSemester = _semesterRepository.GetCurrentSemester(DateTime.Now);
            }
            catch (InvalidOperationException)
            {
                ModelState.AddModelError("", "Текущий семестр не определён.");
                return PartialView("_GroupAddModal", viewModel);
            }

            var groupDTO = viewModel.GetGroupDTO();
            groupDTO.SemesterId = currentSemester.Id; // Автоматическое присваивание SemesterId

            _groupRepository.AddGroup(groupDTO);

            return Json(new { success = true, redirectUrl = Url.Action("GroupsList") });
        }



        [HttpPost]
        public IActionResult EditGroup(GroupCreateEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_GroupEditModal", viewModel);
            }

            if (viewModel.TypeGroup == "1")
            {
                viewModel.TypeGroup = "Очно";
            }
            else if (viewModel.TypeGroup == "2")
            {
                viewModel.TypeGroup = "Заочно";
            }

            // Определение текущего семестра
            var currentSemester = _semesterRepository.GetCurrentSemester(DateTime.Now);
            if (currentSemester == null)
            {
                ModelState.AddModelError("", "Текущий семестр не определён.");
                return PartialView("_GroupEditModal", viewModel);
            }

            var groupDTO = viewModel.GetGroupDTO();
            groupDTO.SemesterId = currentSemester.Id; // Автоматическое присваивание SemesterId

            _groupRepository.UpdateGroup(groupDTO);

            return Json(new { success = true });
        }
    }
}
