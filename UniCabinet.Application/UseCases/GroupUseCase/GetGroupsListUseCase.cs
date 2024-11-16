using UniCabinet.Application.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniCabinet.Core.DTOs.CourseManagement;

namespace UniCabinet.Application.UseCases.GroupUseCase
{
    public class GetGroupsListUseCase
    {
        private readonly IGroupRepository _groupRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ISemesterRepository _semesterRepository;

        public GetGroupsListUseCase(IGroupRepository groupRepository, ICourseRepository courseRepository, ISemesterRepository semesterRepository)
        {
            _groupRepository = groupRepository;
            _courseRepository = courseRepository;
            _semesterRepository = semesterRepository;
        }

        public async Task<List<GroupDTO>> Execute()
        {
            var groupList = await _groupRepository.GetAllGroupsAsync();
            var groupDTOs = new List<GroupDTO>();

            foreach (var dto in groupList)
            {
                var course = await _courseRepository.GetCourseById(dto.CourseId);
                var semester = _semesterRepository.GetSemesterById(dto.SemesterId);

                groupDTOs.Add(new GroupDTO
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    TypeGroup = dto.TypeGroup,
                    CourseId = course.Number,
                    SemesterId = semester.Number
                });
            }

            return groupDTOs;
        }
    }
}
