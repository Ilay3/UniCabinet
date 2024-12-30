using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UniCabinet.Application.UseCases.StudentProgressUseCase;
using UniCabinet.Core.Models.ViewModel.Student;

namespace UniCabinet.Web.Controllers
{
    public class StudentProgressController : Controller
    {
        private readonly IMapper _mapper;

        public StudentProgressController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ProgressList([FromServices] GetAllProgressByStudentIdUseCase getAllProgressByStudentIdUseCase)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var dTOs = await getAllProgressByStudentIdUseCase.ExecuteAsync(userId);

            var result = _mapper.Map<List<StudentProgressVM>>(dTOs);
            return View(result);
        }
    }
}
