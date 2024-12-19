namespace UniCabinet.Core.Models.ViewModel.Lecture
{
    public class LectureBaseVM
    {
        public int DisciplineDetailId { get; set; }
        /// <summary>
        /// Номер лекции
        /// </summary>
        public string Name{ get; set; }
        public DateTime Date { get; set; }
    }
}
