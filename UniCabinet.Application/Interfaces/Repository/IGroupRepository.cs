using UniCabinet.Core.DTOs.Entites;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IGroupRepository
    {
        Task<GroupDTO> GetGroupByIdAsync(int id);
        Task<List<GroupDTO>> GetAllGroupsAsync();
        Task AddGroupAsync(GroupDTO groupDTO);
        Task DeleteGroupAsync(int id);
        Task UpdateGroupAsync(GroupDTO groupDTO);
        Task SaveChangesAsync();
        Task UpdateGroupsSemesterAsync(List<GroupDTO> groupsToUpdate);
        Task<List<UserDTO>> GetUsersByGroupIdAsync(int groupId);
        Task UpdateGroupsCourseAsync(List<GroupDTO> groupsToUpdate);
        Task UpdateUsersGroupAsync(List<UserDTO> usersToUpdate);
    }
}
