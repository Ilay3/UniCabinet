namespace UniCabinet.Domain.Entities
{
    public class StudentProgress
    {
        public int Id { get; set; }

        public string StudentId { get; set; }
        public User Student { get; set; }

        public int DisciplineDetailId { get; set; }
        public DisciplineDetail DisciplineDetails { get; set; }

        /// <summary>
        /// Сумма баллов за лекций
        /// </summary>
        public int TotalLecturePoints { get; set; }

        /// <summary>
        /// Сумма баллов за практику
        /// </summary>
        public int TotalPracticalPoints { get; set; }

        /// <summary>
        /// Общая сумма баллов
        /// </summary>
        public int TotalPoints { get; set; }

        /// <summary>
        /// Итоговая оценка
        /// </summary>
        public int FinalGrade { get; set; }

        /// <summary>
        /// Требуется пересдача
        /// </summary>
        public bool NeedsRetake { get; set; }
    }
}
