using UniCabinet.Domain.Entities;

namespace UniCabinet.Domain.Models
{
    public class DepartmentEntity
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }

        public ICollection<UserEntity> User { get; set; }
        public ICollection<DisciplineEntity> Discipline { get; set; }

    }
}
