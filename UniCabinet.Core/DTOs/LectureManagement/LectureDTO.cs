namespace UniCabinet.Core.DTOs.LectureManagement
{
    public class LectureDTO
    {
        public int Id { get; set; }

        public int DisciplineDetailId { get; set; }

        /// <summary>
        /// Номер лекции
        /// </summary>
        public string Name { get; set; }


        public DateTime Date { get; set; }
        public bool IsProcessed { get; set; }

    }
}
