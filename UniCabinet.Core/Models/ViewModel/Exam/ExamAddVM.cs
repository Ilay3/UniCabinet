using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCabinet.Core.Models.ViewModel.Exam
{
    public class ExamAddVM
    {
        public int DisciplineDetailId { get; set; }
        [Required(ErrorMessage = "Дата экзамена обязательна")]
        [DataType(DataType.Date)]

        public DateTime Date { get; set; }
    }

}
