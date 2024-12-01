using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.CourseManagement;
using UniCabinet.Core.DTOs.UserManagement;

namespace UniCabinet.Core.UseCases
{
    public class GetDepartmentDisciplinesUseCase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public GetDepartmentDisciplinesUseCase(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<DepartmentWithDisciplinesDTO> ExecuteAsync(string headUserId)
        {
            var department = await _departmentRepository.GetDepartmentWithDisciplinesAndTeachersAsync(headUserId);
            if (department == null)
                throw new Exception("Кафедра не найдена или пользователь не является заведующей.");

            // Маппинг сущностей в DTO
            var departmentDTO = new DepartmentWithDisciplinesDTO
            {
                Id = department.Id,
                DepartmentName = department.DepartmentName,
                Disciplines = department.Discipline.Select(d => new DisciplineWithTeachersDTO
                {
                    Id = d.Id,
                    Name = d.Name,
                    SpecialtyName = d.Specialty != null ? d.Specialty.Name : string.Empty,
                    Description = d.Description,
                    Teachers = d.Specialty != null
                        ? d.Specialty.Teachers.Select(t => new UserDTO
                        {
                            Id = t.Id,
                            Email = t.Email,
                            FirstName = t.FirstName,
                            LastName = t.LastName,
                            Patronymic = t.Patronymic,
                            SpecializationId = t.SpecialtyId,
                            DepartmentId = t.DepartmentId
                        }).ToList()
                        : new List<UserDTO>()
                }).ToList()
            };

            return departmentDTO;
        }
    }
}
