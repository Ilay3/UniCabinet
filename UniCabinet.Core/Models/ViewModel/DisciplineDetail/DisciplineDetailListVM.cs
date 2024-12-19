namespace UniCabinet.Core.Models.ViewModel.DisciplineDetail;

public class DisciplineDetailListVM 
{
    public int Id { get; set; }

    public string DisciplineName { get; set; }

    public string GroupName { get; set; }

    public int CourseNumber { get; set; }

    public int SemesterNumber { get; set; }

    public string TeacherFirstName { get; set; }

    public string TeacherLastName { get; set; }

    public string TeacherPatronymic { get; set; }

    /// <summary>
    /// Количество лекций
    /// </summary>
    public int LectureCount { get; set; }

    /// <summary>
    /// Количество практических
    /// </summary>
    public int PracticalCount { get; set; }

    
}
