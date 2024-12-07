using System.Collections.Generic;
using UniCabinet.Core.DTOs.StudentManagement;

namespace UniCabinet.Core.DTOs.PracticalManagement
{
    public class PracticalAttendanceDTO
    {
        public int PracticalId { get; set; }
        public int DisciplineDetailId { get; set; }
        public int PracticalNumber { get; set; }
        public string DisciplineName { get; set; }
        public List<StudentGradeDTO> Students { get; set; }
    }
}
