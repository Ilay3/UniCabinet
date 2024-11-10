namespace UniCabinet.Domain.Entities;

public class LectureEntity
{
    public int Id { get; set; }

    public int DisciplineDetailId { get; set; }

    /// <summary>
    /// Номер лекции
    /// </summary>
    public int Number { get; set; }
    /// <summary>
    /// Количество баллов за лекцию
    /// </summary>
    public decimal PointsCount { get; set; }

    public DateTime Date { get; set; }

    // Навигационные свойства
    public DisciplineDetailEntity DisciplineDetails { get; set; }
    public ICollection<LectureVisitEntity> LectureVisits { get; set; }
}
