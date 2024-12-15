namespace UniCabinet.Domain.Entities;

public class PracticalResultEntity
{
    public int Id { get; set; }

    public string StudentId { get; set; }
    public UserEntity Student { get; set; }

    public int PracticalId { get; set; }
    public PracticalEntity Practical { get; set; }

    /// <summary>
    /// Оценка
    /// </summary>
    public int Grade { get; set; }


}
