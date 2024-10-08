using UniCabinet.Domain.DTO;
using UniCabinet.Web.ViewModel;

namespace UniCabinet.Web.Extension
{
    public static class GroupsViewModelExtension
    {
        public static GroupViewModel GetGroupViewModel(this GroupDTO groupsDTO, int courseNumber, int semesterNumber)
        {
            var groups = new GroupViewModel
            {
                Name = groupsDTO.Name,
                CourseNumber = courseNumber,
                SemesterNumber = semesterNumber,
                TypeGroup = groupsDTO.TypeGroup,
            };

            return groups;
        }
    }
}
