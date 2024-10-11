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
            var groupDTO = _groupRepository.GetAllGroups();

            var groupViewModel = groupDTO
                .Select(dto =>
                {
                    var courseGroup = _courseRepository.GetCourseById(dto.CourseId);
                    var semesterGroup = _semesterRepository.GetSemesterById(dto.SemesterId);

                    return dto.GetGroupViewModel(courseGroup.Number, semesterGroup.Number);
                })
                .ToList();

            return View(groupViewModel);
        }

        public IActionResult GroupAddModal()
        {
            return PartialView("_GroupAddModal");
        }

        public IActionResult GroupEditModal(int id)
        {
            var groupDTO = _groupRepository.GetGroupById(id);
            var groupViewModel = groupDTO.GetGroupCreateEditViewModel();

            if (groupViewModel.TypeGroup == "Очно")
            {
                groupViewModel.TypeGroup = "1";
            }

            if (groupViewModel.TypeGroup == "Заочно")
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

            var groupViewModel = viewModel.GetGroupDTO();
            
            await _groupRepository.AddGroupAsync(groupViewModel);

            return RedirectToAction("GroupsList");
        }

        public IActionResult EditGroup(int id, GroupCreateEditViewModel viewModel)
        {
            if (id != viewModel.Id) return NotFound();
            
            if(!ModelState.IsValid) return PartialView("_GroupEditModal", viewModel);

            if (viewModel.TypeGroup == "1")
            {
                viewModel.TypeGroup = "Очно";
            }

            if (viewModel.TypeGroup == "2")
            {
                viewModel.TypeGroup = "Заочно";
            }

            var groupDTO = viewModel.GetGroupDTO();
            _groupRepository.UpdateGroup(groupDTO);

            return RedirectToAction("GroupsList");
            
        }

    }
}
