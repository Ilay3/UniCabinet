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

        public async Task<IActionResult> GroupsList()
        {
            var groupListDTO = await _groupRepository.GetAllGroups();

            var groupViewModelList = new List<GroupViewModel>();

            foreach (var dto in groupListDTO)
            {
                var courseGroup = await _courseRepository.GetCourseById(dto.CourseId);
                var semesterGroup = await _semesterRepository.GetSemesterById(dto.SemesterId);

                var groupViewModel = await dto.GetGroupViewModelAsync(courseGroup.Number, semesterGroup.Number);
                groupViewModelList.Add(groupViewModel);
            }

            return View(groupViewModelList);
        }

        public IActionResult GroupAddModal()
        {
            var viewModel = new GroupCreateEditViewModel();
            return PartialView("_GroupAddModal", viewModel);
        }

        public async Task<IActionResult> GroupEditModal(int id)
        {
            var groupDTO = await _groupRepository.GetGroupById(id); 
            var groupViewModel = groupDTO.GetGroupCreateEditViewModel();

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
        public async Task<IActionResult> AddGroup(GroupCreateEditViewModel viewModel)
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

            var groupDTO = viewModel.GetGroupDTO();
            
            await _groupRepository.AddGroupAsync(groupDTO);

            return RedirectToAction("GroupsList");
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

            var groupDTO = viewModel.GetGroupDTO();
            _groupRepository.UpdateGroup(groupDTO);

            return Json(new { success = true });
        }

    }
}
