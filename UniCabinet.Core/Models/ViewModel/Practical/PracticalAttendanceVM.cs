using System.Collections.Generic;

namespace UniCabinet.Core.Models.ViewModel.Practical
{
    public class PracticalAttendanceVM
    {
        public int PracticalId { get; set; }
        public int DisciplineDetailId { get; set; }
        public int PracticalNumber { get; set; }
        public string DisciplineName { get; set; }
        public List<StudentGradeVM> Students { get; set; }
    }
}
