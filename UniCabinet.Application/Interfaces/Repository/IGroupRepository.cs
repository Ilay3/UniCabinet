using UniCabinet.Domain.DTO;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IGroupRepository
    {
        Task AddGroupAsync(GroupDTO groupDTO);
        Task DeleteGroup(int id);
        Task<List<GroupDTO>> GetAllGroups();
        Task<GroupDTO> GetGroupById(int id);
        void UpdateGroup(GroupDTO groupDTO);
    }
}