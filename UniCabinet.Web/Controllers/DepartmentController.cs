using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UniCabinet.Application.UseCases.DepartmentUseCase;
using UniCabinet.Application.UseCases.DisciplineUseCase;
using UniCabinet.Core.Models.ViewModel.Departmet;
using UniCabinet.Core.Models.ViewModel.Discipline;

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
        public async Task<IActionResult> DisciplinesListAsync([FromServices] GetDisciplinesByHeadUseCase getDisciplinesByHeadUseCase)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var result = await getDisciplinesByHeadUseCase.ExecuteAsync(userId);
            var disciplineVMs = _mapper.Map<List<DisciplineListVM>>(result);

            return View(disciplineVMs);
        }
    }
}
