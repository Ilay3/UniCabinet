namespace UniCabinet.Domain.Entities;

public class GroupEntity
{
    public int Id { get; set; }

    public string Name { get; set; }

    /// <summary>
    /// Очно/Заочно 
    /// </summary>
    public string TypeGroup { get; set; }

    public int CourseId { get; set; }
    public CourseEntity Course { get; set; }

    public int SemesterId { get; set; }
    public SemesterEntity Semester { get; set; }

    // Навигационные свойства
    public ICollection<UserEntity> Users { get; set; }

    public ICollection<DisciplineDetailEntity> DisciplineDetails { get; set; }
}
