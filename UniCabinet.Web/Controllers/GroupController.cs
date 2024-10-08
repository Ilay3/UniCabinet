using Microsoft.AspNetCore.Mvc;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Web.Extension;

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

        public IActionResult GroupsView()
        {
            var groupDTO = _groupRepository.GetAllGroups();

            var groupViewModel = groupDTO
                .Select(dto =>
                {
                    var courseGroup = _courseRepository.GetCourseById(dto.CourseId);
                    var semesterGroup = _semesterRepository.GetSemesterById(dto.SemesterId);

                    return dto.GetGroupViewModel(courseGroup.Number, semesterGroup.Number);
                }).ToList();

            return View(groupViewModel);
        }
    }
}
