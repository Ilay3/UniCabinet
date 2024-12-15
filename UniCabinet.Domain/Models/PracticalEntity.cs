namespace UniCabinet.Domain.Entities;

public class PracticalEntity
{
    public int Id { get; set; }

    public int DisciplineDetailId { get; set; }
    public DisciplineDetailEntity DisciplineDetails { get; set; }

    /// <summary>
    /// Номер практической
    /// </summary>
    public int PracticalNumber { get; set; }

    /// <summary>
    /// Дата проведения
    /// </summary>
    public DateTime Date { get; set; }
    public bool IsProcessed { get; set; }


    // Навигационные свойства
    public ICollection<PracticalResultEntity> PracticalResults { get; set; }
}
