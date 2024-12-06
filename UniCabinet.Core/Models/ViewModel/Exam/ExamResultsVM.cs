using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCabinet.Core.Models.ViewModel.Exam
{
    public class ExamResultsVM
    {
        public int ExamId { get; set; }
        public int DisciplineDetailId { get; set; }
        public List<ExamResultItemVM> Students { get; set; } = new List<ExamResultItemVM>();
    }

}
