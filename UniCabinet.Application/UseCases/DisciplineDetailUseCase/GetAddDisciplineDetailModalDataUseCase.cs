using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.CourseManagement;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.UseCases.DisciplineDetailUseCase
{
    public class GetAddDisciplineDetailModalDataUseCase
    {
        private readonly ICourseRepository _courseRepo;
        private readonly IGroupRepository _groupRepo;
        private readonly ISemesterRepository _semesterRepo;

        public GetAddDisciplineDetailModalDataUseCase(ICourseRepository courseRepo, IGroupRepository groupRepo, ISemesterRepository semesterRepo)
        {
            _courseRepo = courseRepo;
            _groupRepo = groupRepo;
            _semesterRepo = semesterRepo;
        }

        public async Task<(List<CourseDTO>, List<GroupDTO>, List<SemesterDTO>)> ExecuteAsync()
        {
            var courses = await _courseRepo.GetAllCourseAsync();
            var groups = await _groupRepo.GetAllGroupsAsync();
            var semesters = await _semesterRepo.GetAllSemesters();

            return (courses, groups, semesters);
        }
    }

}
