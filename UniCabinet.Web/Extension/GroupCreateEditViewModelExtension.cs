using UniCabinet.Domain.DTO;
using UniCabinet.Web.ViewModel;

namespace UniCabinet.Web.Extension
{
    public static class GroupCreateEditViewModelExtension
    {
        public static GroupCreateEditViewModel GetGroupCreateEditViewModel(this GroupDTO groupsDTO)
        {
            var groups = new GroupCreateEditViewModel
            {
                Id = groupsDTO.Id,
                Name = groupsDTO.Name,
                CourseId = groupsDTO.CourseId,
                SemesterId = groupsDTO.SemesterId,
                TypeGroup = groupsDTO.TypeGroup,
            };

            return groups;
        }
    }
}
