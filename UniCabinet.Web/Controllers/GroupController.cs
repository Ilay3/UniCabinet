using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UniCabinet.Application.UseCases.GroupUseCase;
using UniCabinet.Core.DTOs.Entites;
using UniCabinet.Core.Models.ViewModel.Group;

namespace UniCabinet.Web.Controllers
{
    public class GroupController : Controller
    {
        private readonly IMapper _mapper;

        public GroupController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IActionResult> GroupsListAsync([FromServices] GetGroupsListUseCase getGroupsListUseCase)
        {
            var groupDTOs = await getGroupsListUseCase.Execute();
            var groupVMList = _mapper.Map<List<GroupListVM>>(groupDTOs);
            return View(groupVMList);
        }

        public async Task<IActionResult> GroupAddModalAsync([FromServices] GetGroupAddModalUseCase getGroupAddModalUseCase)
        {
            var groupDTO = await getGroupAddModalUseCase.Execute();
            var groupVM = _mapper.Map<GroupAddVM>(groupDTO);
            return PartialView("_GroupAddModal", groupVM);
        }

        public async Task<IActionResult> GroupEditModalAsync(int id, [FromServices] GetGroupEditModalUseCase getGroupEditModalUseCase)
        {
            var groupDTO = await getGroupEditModalUseCase.Execute(id);
            var groupVM = _mapper.Map<GroupEditVM>(groupDTO);
            return PartialView("_GroupEditModal", groupVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddGroupAsync(GroupAddVM viewModel, [FromServices] AddGroupUseCase addGroupUseCase)
        {
            if (!ModelState.IsValid) return PartialView("_GroupAddModal", viewModel);

            viewModel.TypeGroup = viewModel.TypeGroup == "1" ? "Очно" : "Заочно";
            var groupDTO = _mapper.Map<GroupDTO>(viewModel);

            try
            {
                await addGroupUseCase.Execute(groupDTO);
                return Json(new { success = true, redirectUrl = Url.Action("GroupsList") });
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return PartialView("_GroupAddModal", viewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditGroupAsync(GroupEditVM viewModel, [FromServices] EditGroupUseCase editGroupUseCase)
        {
            if (!ModelState.IsValid) return PartialView("_GroupEditModal", viewModel);

            viewModel.TypeGroup = viewModel.TypeGroup == "1" ? "Очно" : "Заочно";
            var groupDTO = _mapper.Map<GroupDTO>(viewModel);

            try
            {
                await editGroupUseCase.Execute(groupDTO);
                return Json(new { success = true });
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return PartialView("_GroupEditModal", viewModel);
            }
        }
    }
}
