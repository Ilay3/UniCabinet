namespace UniCabinet.Domain.Entities;

public class LectureEntity
{
    public int Id { get; set; }

    public int DisciplineDetailId { get; set; }

    /// <summary>
    /// Номер лекции
    /// </summary>
    public string Name { get; set; }

    public DateTime Date { get; set; }
    public bool IsProcessed { get; set; }

    // Навигационные свойства
    public DisciplineDetailEntity DisciplineDetails { get; set; }
    public ICollection<LectureVisitEntity> LectureVisits { get; set; }
}
