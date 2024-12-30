using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.CourseManagement;
namespace UniCabinet.Application.UseCases.TeacherUseCase
{
    public class GetGroupsForDisciplineUseCase
    {
        private readonly IDisciplineDetailRepository _disciplineDetailRepository;
        private readonly IGroupRepository _groupRepository;
        public GetGroupsForDisciplineUseCase(IDisciplineDetailRepository disciplineDetailRepository, IGroupRepository groupRepository)
        {
            _disciplineDetailRepository = disciplineDetailRepository;
            _groupRepository = groupRepository;
        }
        public async Task<List<GroupDTO>> ExecuteAsync(int disciplineId, DateTime? filetDate)
        {
            try
            {
                filetDate ??= DateTime.Now;

                var disciplineDetails = await _disciplineDetailRepository.GetByDisciplineIdAsync(disciplineId);
                var groupIds = disciplineDetails
                    .Where(date => date.CreatedDate.Year == filetDate.Value.Year)
                    .Select(dd => dd.GroupId)
                    .Distinct()
                    .ToList();
                var groups = await _groupRepository.GetGroupsByIdsAsync(groupIds);
                return groups;
            }
            catch
            {
                throw;
            }
        }
    }
}
