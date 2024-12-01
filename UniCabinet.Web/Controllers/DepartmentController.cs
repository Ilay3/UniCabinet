using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UniCabinet.Application.UseCases.DepartmentUseCase;
using UniCabinet.Core.DTOs.DepartmentManagmnet;
using UniCabinet.Core.Models.ViewModel.Department;
using UniCabinet.Core.Models.ViewModel.Departmet;
using UniCabinet.Core.UseCases;

namespace UniCabinet.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IMapper _mapper;

        public DepartmentController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> DepartmentListAsync([FromServices] GetDepartmentDisciplinesUseCase getDisciplinesByHeadUseCase)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var result = await getDisciplinesByHeadUseCase.ExecuteAsync(userId);
            var getDepartmantAndUserVMVMs = _mapper.Map<DepartmentWithDisciplinesVM>(result);

            return View(getDepartmantAndUserVMVMs);
        }

        [HttpGet]
        public async Task<IActionResult> DepartmentsAdminList(
        [FromServices] GetDepartmentsWithUsersUseCase getDepartmentsWithUsersUseCase)
        {
            var departmentsDTO = await getDepartmentsWithUsersUseCase.ExecuteAsync();
            var departmentsVM = _mapper.Map<List<DepartmentsWithUsersVM>>(departmentsDTO);
            return View(departmentsVM);
        }

        [HttpGet]
        public IActionResult AddDepartmentModal()
        {
            var model = new DepartmentAddEditVM();
            return PartialView("_AddDepartmentModal", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(DepartmentAddEditVM model,
            [FromServices] AddDepartmentUseCase addDepartmentUseCase)
        {
            if (ModelState.IsValid)
            {
                var departmentDTO = _mapper.Map<DepartmentDTO>(model);
                await addDepartmentUseCase.ExecuteAsync(departmentDTO);

                return RedirectToAction("DepartmentsAdminList");
            }
            else
            {
                return PartialView("_AddDepartmentModal", model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditDepartmentModal(int departmentId,
            [FromServices] GetDepartmentByIdUseCase getDepartmentByIdUseCase)
        {
            var departmentDTO = await getDepartmentByIdUseCase.ExecuteAsync(departmentId);
            if (departmentDTO == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<DepartmentAddEditVM>(departmentDTO);
            return PartialView("_EditDepartmentModal", model);
        }

        [HttpPost]
        public async Task<IActionResult> EditDepartment(DepartmentAddEditVM model,
            [FromServices] EditDepartmentUseCase editDepartmentUseCase)
        {
            if (ModelState.IsValid)
            {
                var departmentDTO = _mapper.Map<DepartmentDTO>(model);
                await editDepartmentUseCase.ExecuteAsync(departmentDTO);

                return RedirectToAction("DepartmentsAdminList");
            }
            else
            {
                return PartialView("_EditDepartmentModal", model);
            }
        }


    }
}
