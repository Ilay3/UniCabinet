using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using UniCabinet.Application.UseCases.AdminUseCase;
using UniCabinet.Core.Models.ViewModel.User;
using UniCabinet.Core.Models.ViewModel;
using UniCabinet.Domain.Entities;
using UniCabinet.Core.DTOs.UserManagement;
using UniCabinet.Application.UseCases.DepartmentUseCase;
using UniCabinet.Core.Models.ViewModel.Departmet;

public class AdminController : Controller
{
    private readonly IMapper _mapper;

    public AdminController(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<IActionResult> VerifiedUsers(
        [FromServices] GetVerifiedUsersUseCase getVerifiedUsersUseCase,
        string role,
        string query = null,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var studentGroupDTO = await getVerifiedUsersUseCase.Execute(role, query, pageNumber, pageSize);

        var studentGroupVM = _mapper.Map<StudentGroupVM>(studentGroupDTO);

        ViewBag.SelectedRole = role;
        ViewBag.Roles = new List<string> { "Student", "Teacher", "Administrator", "Verified" }
            .Select(r => new SelectListItem
            {
                Value = r,
                Text = r,
                Selected = r == role
            })
            .ToList();

        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            return PartialView("_UserTablePartial", studentGroupVM);
        }

        return View(studentGroupVM);
    }

    [HttpGet]
    public async Task<IActionResult> SearchUsers(
        string query,
        string role,
        [FromServices] SearchUsersUseCase searchUsersUseCase)
    {
        var filteredUsersDTO = await searchUsersUseCase.Execute(query, role);
        var userVMs = _mapper.Map<List<UserVM>>(filteredUsersDTO);

        return Json(userVMs);
    }

    [HttpGet]
    public async Task<IActionResult> RoleEditModal(
        string userId,
        [FromServices] GetRoleEditModalUseCase getRoleEditModalUseCase)
    {
        var userRolesDTO = await getRoleEditModalUseCase.Execute(userId);
        if (userRolesDTO == null)
        {
            return NotFound();
        }

        var viewModel = _mapper.Map<UserRolesVM>(userRolesDTO);

        return PartialView("_RoleModal", viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateUserRoles(
        UserRolesVM model,
        [FromServices] UpdateUserRolesUseCase updateUserRolesUseCase)
    {
        if (!ModelState.IsValid)
        {
            return PartialView("_RoleModal", model);
        }

        var userRolesDTO = _mapper.Map<UserRolesDTO>(model);
        var result = await updateUserRolesUseCase.Execute(userRolesDTO);

        if (!result.Success)
        {
            ModelState.AddModelError("", result.ErrorMessage);
            return PartialView("_RoleModal", model);
        }

        return RedirectToAction("VerifiedUsers");
    }

    [HttpGet]
    public async Task<IActionResult> GroupEditModal(
        string userId,
        [FromServices] GetGroupEditAdminModalUseCase getGroupEditModalUseCase)
    {
        var userGroupDTO = await getGroupEditModalUseCase.Execute(userId);
        if (userGroupDTO == null)
        {
            return NotFound();
        }

        var viewModel = _mapper.Map<UserGroupVM>(userGroupDTO);

        return PartialView("_GroupModal", viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateUserGroup(
        UserGroupVM model,
        [FromServices] UpdateUserGroupUseCase updateUserGroupUseCase,
        [FromServices] UserManager<UserEntity> userManager)
    {
      

        var user = await userManager.FindByIdAsync(model.UserId);
        if (user == null)
        {
            return NotFound();
        }

        var userRoles = await userManager.GetRolesAsync(user);
        var updateResult = await updateUserGroupUseCase.Execute(model.UserId, model.GroupId, userRoles);

        if (!updateResult.Success)
        {
            ModelState.AddModelError("", updateResult.ErrorMessage);
            return PartialView("_GroupModal", model);
        }

        return RedirectToAction("VerifiedUsers");
    }

    [HttpGet]
    public async Task<IActionResult> UserDetailModal(
        string userId,
        [FromServices] GetUserDetailModalUseCase getUserDetailModalUseCase)
    {
        var userDetailDTO = await getUserDetailModalUseCase.Execute(userId);
        if (userDetailDTO == null)
        {
            return NotFound();
        }

        var viewModel = _mapper.Map<UserDetailVM>(userDetailDTO);

        return PartialView("_UserDetailModal", viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateUserDetails(
        UserDetailVM model,
        [FromServices] UpdateUserDetailsUseCase updateUserDetailsUseCase)
    {
        if (!ModelState.IsValid)
        {
            return PartialView("_UserDetailModal", model);
        }

        var userDetailDTO = _mapper.Map<UserDetailDTO>(model);
        var updateResult = await updateUserDetailsUseCase.Execute(userDetailDTO);

        if (!updateResult)
        {
            ModelState.AddModelError("", "Ошибка при обновлении деталей пользователя.");
            return PartialView("_UserDetailModal", model);
        }

        return RedirectToAction("VerifiedUsers");
    }

    public async Task<IActionResult> GetDepatrmentData([FromServices] GetDepartmnetDataUseCase getDepartmnetDataUseCase)
    {
        var result = await getDepartmnetDataUseCase.ExecutreAsync();

        var viewModel = _mapper.Map<List<DepartmantVM>>(result);
        return PartialView("_SpecializationAndDepartmentModal",viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateSpecAndDep(
     string UserId,
     int DepartmentId,
     int SpecializationId,
     [FromServices] UpdateSpecAndDepUseCase updateSpecAndDepUseCase)
    {
        if (!ModelState.IsValid)
        {

            return PartialView("_SpecializationAndDepartmentModal");
        }

        try
        {
            await updateSpecAndDepUseCase.ExecuteAsync(UserId, DepartmentId, SpecializationId);

            return RedirectToAction("VerifiedUsers");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Произошла ошибка при обновлении данных.");
            return PartialView("_SpecializationAndDepartmentModal");
        }
    }


    [HttpGet]
    public async Task<IActionResult> SpecAndDepEditModal([FromServices] SpecAndDepUseCase specAndDepUseCase , string UserId)
    {
        var rezult = await specAndDepUseCase.ExecuteAsync(UserId);
        var viewModel = _mapper.Map<SpecAndDepVM>(rezult);

        return PartialView("_SpecializationAndDepartmentModal", viewModel);

    }
}
