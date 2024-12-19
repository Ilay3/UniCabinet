namespace UniCabinet.Domain.Entities;

public class ExamResultEntity
{
    public int Id { get; set; }

    public string StudentId { get; set; }
    public UserEntity Student { get; set; }

    public int ExamId { get; set; }
    public ExamEntity Exam { get; set; }

    /// <summary>
    /// Средняя оценка по баллам
    /// </summary>
    public decimal GradeAvarage { get; set; }

    /// <summary>
    /// Окончательная оценка
    /// </summary>
    public decimal FinalGrade { get; set; }
    
    /// <summary>
    /// Согласен ли препод с оценкой
    /// </summary>
    public bool IsAutomatic { get; set; }
}
