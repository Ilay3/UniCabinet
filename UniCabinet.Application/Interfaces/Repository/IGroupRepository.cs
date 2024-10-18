using UniCabinet.Domain.DTO;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IGroupRepository
    {
        void AddGroup(GroupDTO groupDTO);
        void DeleteGroup(int id);
        List<GroupDTO> GetAllGroups();
        GroupDTO GetGroupById(int id);
        void UpdateGroup(GroupDTO groupDTO);
    }
}