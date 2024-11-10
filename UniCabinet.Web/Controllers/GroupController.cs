using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.Entites;
using UniCabinet.Core.Models.ViewModel;
using UniCabinet.Core.Models.ViewModel.Group;

namespace UniCabinet.Web.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupRepository _groupRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ISemesterRepository _semesterRepository;
        private readonly IMapper _mapper;
        public GroupController(IGroupRepository groupRepository, ICourseRepository courseRepository, ISemesterRepository semesterRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _courseRepository = courseRepository;
            _semesterRepository = semesterRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> GroupsListAsync()
        {
            var groupListDTO = _groupRepository.GetAllGroups();

            var groupVMList = new List<GroupListVM>();

            foreach (var dto in groupListDTO)
            {
                var courseGroup = await _courseRepository.GetCourseById(dto.CourseId);
                var semesterGroup = _semesterRepository.GetSemesterById(dto.SemesterId);

                var groupVM = new GroupListVM
                {
                    CourseId = courseGroup.Number,
                    SemesterId = semesterGroup.Number,
                    Id = dto.Id,
                    Name = dto.Name,
                    TypeGroup = dto.TypeGroup,
                };

                groupVMList.Add(groupVM);
            }

            return View(groupVMList);
        }


        public IActionResult GroupAddModal()
        {
            var currentSemester = _semesterRepository.GetCurrentSemester(DateTime.Now);
            var viewModal = new GroupAddVM
            {
                SemesterId = currentSemester != null ? currentSemester.Number : 0
            };
            return PartialView("_GroupAddModal", viewModal);
        }

        public IActionResult GroupEditModal(int id)
        {
            var groupDTO = _groupRepository.GetGroupById(id);
            var groupVM = new GroupEditVM
            {
                Id = groupDTO.Id,
                Name = groupDTO.Name,
                TypeGroup = groupDTO.TypeGroup,
                CourseId = groupDTO.CourseId,
            };


            var currentSemester = _semesterRepository.GetCurrentSemester(DateTime.Now);
            groupVM.SemesterId = currentSemester != null ? currentSemester.Number : 0;

            if (groupVM.TypeGroup == "Очно")
            {
                groupVM.TypeGroup = "1";
            }
            else if (groupVM.TypeGroup == "Заочно")
            {
                groupVM.TypeGroup = "2";
            }

            return PartialView("_GroupEditModal", groupVM);
        }


        [HttpPost]
        public IActionResult AddGroup(GroupAddVM viewModal)
        {
            if (!ModelState.IsValid) return PartialView("_GroupAddModal", viewModal);

            if (viewModal.TypeGroup == "1")
            {
                viewModal.TypeGroup = "Очно";
            }

            if (viewModal.TypeGroup == "2")
            {
                viewModal.TypeGroup = "Заочно";
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
                return PartialView("_GroupAddModal", viewModal);
            }

            var groupDTO = new GroupDTO
            {
                Name = viewModal.Name,
                TypeGroup = viewModal.TypeGroup,
                CourseId = viewModal.CourseId,

            };
            groupDTO.SemesterId = currentSemester.Id; // Автоматическое присваивание SemesterId

            _groupRepository.AddGroup(groupDTO);

            return Json(new { success = true, redirectUrl = Url.Action("GroupsList") });
        }



        [HttpPost]
        public IActionResult EditGroup(GroupEditVM viewModal)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_GroupEditModal", viewModal);
            }

            if (viewModal.TypeGroup == "1")
            {
                viewModal.TypeGroup = "Очно";
            }
            else if (viewModal.TypeGroup == "2")
            {
                viewModal.TypeGroup = "Заочно";
            }

            // Определение текущего семестра
            var currentSemester = _semesterRepository.GetCurrentSemester(DateTime.Now);
            if (currentSemester == null)
            {
                ModelState.AddModelError("", "Текущий семестр не определён.");
                return PartialView("_GroupEditModal", viewModal);
            }

            var groupDTO = new GroupDTO
            {
                Id = viewModal.Id,
                Name = viewModal.Name,
                TypeGroup = viewModal.TypeGroup,
                CourseId = viewModal.CourseId,

            };
            groupDTO.SemesterId = currentSemester.Id; // Автоматическое присваивание SemesterId

            _groupRepository.UpdateGroup(groupDTO);

            return Json(new { success = true });
        }
    }
}
