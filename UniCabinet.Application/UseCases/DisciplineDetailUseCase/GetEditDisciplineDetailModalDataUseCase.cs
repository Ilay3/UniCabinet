using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.CourseManagement;
using UniCabinet.Core.DTOs.DisciplineDetailManagment;

namespace UniCabinet.Application.UseCases.DisciplineDetailUseCase
{
    public class GetEditDisciplineDetailModalDataUseCase
    {
        private readonly ICourseRepository _courseRepo;
        private readonly IGroupRepository _groupRepo;
        private readonly ISemesterRepository _semesterRepo;
        private readonly IDisciplineDetailRepository _disciplineDetailRepo;

        public GetEditDisciplineDetailModalDataUseCase(
            ICourseRepository courseRepo,
            IGroupRepository groupRepo,
            ISemesterRepository semesterRepo,
            IDisciplineDetailRepository disciplineDetailRepo)
        {
            _courseRepo = courseRepo;
            _groupRepo = groupRepo;
            _semesterRepo = semesterRepo;
            _disciplineDetailRepo = disciplineDetailRepo;
        }

        public async Task<(DisciplineDetailDTO detail, List<CourseDTO> courses, List<GroupDTO> groups, List<SemesterDTO> semesters)> ExecuteAsync(int detailId)
        {
            var detail = await _disciplineDetailRepo.GetByIdAsync(detailId);
            if (detail == null) throw new InvalidOperationException("Discipline detail not found.");

            var courses = await _courseRepo.GetAllCourseAsync();
            var groups = await _groupRepo.GetAllGroupsAsync();
            var semesters = await _semesterRepo.GetAllSemesters();

            return (detail, courses, groups, semesters);
        }
    }

}
