using UniCabinet.Core.DTOs.Entites;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IGroupRepository
    {
        // Существующие методы
        GroupDTO GetGroupById(int id);
        List<GroupDTO> GetAllGroups();
        void AddGroup(GroupDTO groupDTO);
        void DeleteGroup(int id);
        void UpdateGroup(GroupDTO groupDTO);
        Task UpdateGroupsSemesterAsync(List<GroupDTO> groupsToUpdate);
        Task SaveChangesAsync();

        List<UserDTO> GetUsersByGroupId(int groupId);
        void UpdateGroupsCourse(List<GroupDTO> groupsToUpdate);
        void UpdateUsersGroup(List<UserDTO> usersToUpdate);


    }
}
