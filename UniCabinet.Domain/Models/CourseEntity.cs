namespace UniCabinet.Domain.Entities;

public class CourseEntity
{
    public int Id { get; set; }

    /// <summary>
    /// Номер курса
    /// </summary>
    public int Number { get; set; }

    // Навигационные свойства
    public ICollection<GroupEntity> Groups { get; set; }
    public ICollection<DisciplineDetailEntity> DisciplineDetails { get; set; }
}
