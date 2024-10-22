
using UniCabinet.Domain.DTO;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IGroupRepository
    {
        /// <summary>
        /// Получить группу по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор группы.</param>
        /// <returns>DTO объекта группы или null, если не найдено.</returns>
        Task<GroupDTO> GetGroupByIdAsync(int id);

        /// <summary>
        /// Получить список всех групп.
        /// </summary>
        /// <returns>Список DTO групп.</returns>
        Task<List<GroupDTO>> GetAllGroupsAsync();

        /// <summary>
        /// Добавить новую группу.
        /// </summary>
        /// <param name="groupDTO">DTO новой группы.</param>
        /// <returns>Задача асинхронного выполнения.</returns>
        Task AddGroupAsync(GroupDTO groupDTO);

        /// <summary>
        /// Удалить группу по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор группы.</param>
        /// <returns>Задача асинхронного выполнения.</returns>
        Task DeleteGroupAsync(int id);

        /// <summary>
        /// Обновить существующую группу.
        /// </summary>
        /// <param name="groupDTO">DTO группы для обновления.</param>
        /// <returns>Задача асинхронного выполнения.</returns>
        Task UpdateGroupAsync(GroupDTO groupDTO);

        /// <summary>
        /// Пакетное обновление SemesterId для списка групп.
        /// </summary>
        /// <param name="groupsToUpdate">Список DTO групп для обновления.</param>
        /// <returns>Задача асинхронного выполнения.</returns>
        Task UpdateGroupsSemesterAsync(List<GroupDTO> groupsToUpdate);

        /// <summary>
        /// Сохранить изменения в базе данных.
        /// </summary>
        /// <returns>Задача асинхронного выполнения.</returns>
        Task SaveChangesAsync();
    }
}
