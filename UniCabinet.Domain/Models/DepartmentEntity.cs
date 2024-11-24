using UniCabinet.Domain.Entities;

namespace UniCabinet.Domain.Models
{
    public class DepartmentEntity
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string UserId { get; set; }

        public UserEntity User { get; set; }
        public ICollection<DisciplineEntity> Discipline { get; set; }

    }
}
