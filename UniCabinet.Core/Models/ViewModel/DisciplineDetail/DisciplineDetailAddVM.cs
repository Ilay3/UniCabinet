using UniCabinet.Core.Models.ViewModel.Common;

namespace UniCabinet.Core.Models.ViewModel.DisciplineDetail;

public class DisciplineDetailAddVM
{
    public int DisciplineId { get; set; }
    public int CourseId { get; set; }
    public int GroupId { get; set; }
    public int SemesterId { get; set; }
    public int LectureCount { get; set; }
    public int PracticalCount { get; set; }

    public List<SelectListItemVM>? Courses { get; set; }
    public List<SelectListItemVM>? Groups { get; set; }
    public List<SelectListItemVM>? Semesters { get; set; }
}
