using UniCabinet.Core.DTOs.CourseManagement;
using UniCabinet.Core.DTOs.UserManagement;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IGroupRepository
    {
        Task<List<GroupDTO>> GetGroupsByIdsAsync(List<int> groupIds);

        Task<GroupDTO> GetGroupByIdAsync(int id);
        Task<List<GroupDTO>> GetAllGroupsAsync();
        Task AddGroupAsync(GroupDTO groupDTO);
        Task UpdateGroupAsync(GroupDTO groupDTO);
        Task SaveChangesAsync();
        Task UpdateGroupsSemesterAsync(List<GroupDTO> groupsToUpdate);
        Task<List<UserDTO>> GetUsersByGroupIdAsync(int groupId);
        Task UpdateGroupsCourseAsync(List<GroupDTO> groupsToUpdate);
        Task UpdateUsersGroupAsync(List<UserDTO> usersToUpdate);
    }
}
