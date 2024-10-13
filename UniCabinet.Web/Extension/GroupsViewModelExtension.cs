using UniCabinet.Domain.DTO;
using UniCabinet.Web.ViewModel;

namespace UniCabinet.Web.Extension
{
    public static class GroupsViewModelExtension
    {
        public static async Task<GroupViewModel> GetGroupViewModelAsync(this GroupDTO dto, int courseNumber, int semesterNumber)
        {
            return await Task.Run(() =>
            {
                return new GroupViewModel
                {
                    CourseNumber = courseNumber,
                    SemesterNumber = semesterNumber,
                    Id = dto.Id,
                    Name = dto.Name,
                    TypeGroup = dto.TypeGroup,
                };
            });
        }
    }
}
