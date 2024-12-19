using System.Collections.Generic;

namespace UniCabinet.Core.Models.ViewModel.Practical
{
    public class PracticalAttendanceVM
    {
        public int PracticalId { get; set; }
        public int DisciplineDetailId { get; set; }
        public string PracticalName { get; set; }
        public string DisciplineName { get; set; }
        public List<StudentGradeVM> Students { get; set; }
    }
}
