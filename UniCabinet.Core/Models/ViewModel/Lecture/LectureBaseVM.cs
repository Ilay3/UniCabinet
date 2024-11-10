namespace UniCabinet.Core.Models.ViewModel.Lecture
{
    public class LectureBaseVM
    {
        public int DisciplineDetailId { get; set; }
        /// <summary>
        /// Номер лекции
        /// </summary>
        public int Number { get; set; }
        public decimal PointsCount { get; set; }
        public DateTime Date { get; set; }
    }
}
