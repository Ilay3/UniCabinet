using UniCabinet.Domain.DTO;
using UniCabinet.Web.ViewModel;

namespace UniCabinet.Web.Mapping
{
    public static class GroupToDTO
    {
        public static GroupDTO GetGroupDTO(this GroupCreateEditViewModel viewModel)
        {
            var group = new GroupDTO
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                CourseId = viewModel.CourseId,
                TypeGroup = viewModel.TypeGroup,
            };

            return group;
        }
    }
}
