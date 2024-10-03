namespace UniCabinet.Domain.Entities
{
    public class LectureVisit
    {
        public int Id { get; set; }

        public string StudentId { get; set; }

        public User Student { get; set; }

        public int LectureId { get; set; }

        public Lecture Lecture { get; set; }

        /// <summary>
        /// Посещаемость
        /// </summary>
        public bool IsVisit { get; set; }

        /// <summary>
        /// Начисленные баллы
        /// </summary>
        public decimal PointsCount { get; set; }
    }
}
