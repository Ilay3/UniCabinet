using UniCabinet.Domain.DTO;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IGroupRepository
    {
        Task AddGroupAsync(GroupDTO groupDTO);
        Task DeleteGroup(int id);
        List<GroupDTO> GetAllGroups();
        GroupDTO GetGroupById(int id);
        void UpdateGroup(GroupDTO groupDTO);
    }
}