namespace UniCabinet.Domain.Entities;

public class DisciplineEntity
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int? SpecialtyId { get; set; }
    public SpecialtyEntity Specialty { get; set; }

    // Навигационные свойства
    public ICollection<DisciplineDetailEntity> DisciplineDetails { get; set; }
}
