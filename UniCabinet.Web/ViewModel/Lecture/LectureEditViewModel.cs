namespace UniCabinet.Web.ViewModel.Lecture
{
    public class LectureEditViewModel
    {
        public int Id { get; set; }

        public int DisciplineDetailId { get; set; }

        /// <summary>
        /// Номер лекции
        /// </summary>
        public int Number { get; set; }

        public DateTime Date { get; set; }
    }
}
