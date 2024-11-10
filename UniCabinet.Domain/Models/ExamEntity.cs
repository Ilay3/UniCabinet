namespace UniCabinet.Domain.Entities;

public class ExamEntity
{
    public int Id { get; set; }

    public int DisciplineDetailId { get; set; }
    public DisciplineDetailEntity DisciplineDetails { get; set; }

    public DateTime Date { get; set; }

    // Навигационные свойства
    public ICollection<ExamResultEntity> ExamResults { get; set; }
}
