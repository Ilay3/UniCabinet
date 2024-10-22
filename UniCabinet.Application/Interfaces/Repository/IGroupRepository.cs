// Файл: UniCabinet.Application/Interfaces/Repository/IGroupRepository.cs

using System.Collections.Generic;
using System.Threading.Tasks;
using UniCabinet.Domain.DTO;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IGroupRepository
    {
        // Существующие синхронные методы
        GroupDTO GetGroupById(int id);
        List<GroupDTO> GetAllGroups();
        void AddGroup(GroupDTO groupDTO);
        void DeleteGroup(int id);
        void UpdateGroup(GroupDTO groupDTO);

        // Добавленные асинхронные методы
        Task<List<GroupDTO>> GetAllGroupsAsync();
        Task SaveChangesAsync();
        Task UpdateGroupsSemesterAsync(List<GroupDTO> groupsToUpdate);
    }
}
