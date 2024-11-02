using System.ComponentModel.DataAnnotations;

namespace UniCabinet.Web.ViewModel.Lecture
{
    public class LectureAddViewModel
    {
        /// <summary>
        /// Номер лекции
        /// </summary>
        public int Number { get; set; }

        public int DisciplineDetailId { get; set; }

        public DateTime Date { get; set; }

        [Display(Name = "Количество баллов за лекцию")]
        public decimal PointsCount { get; set; }
    }
}
