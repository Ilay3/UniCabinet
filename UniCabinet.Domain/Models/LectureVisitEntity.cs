namespace UniCabinet.Domain.Entities;

public class LectureVisitEntity
{
    public int Id { get; set; }

    public string StudentId { get; set; }

    public UserEntity Student { get; set; }

    public int LectureId { get; set; }

    public LectureEntity Lecture { get; set; }

    /// <summary>
    /// Посещаемость
    /// </summary>
    public bool IsVisit { get; set; }

    /// <summary>
    /// Начисленные баллы
    /// </summary>
    public decimal PointsCount { get; set; }
}
