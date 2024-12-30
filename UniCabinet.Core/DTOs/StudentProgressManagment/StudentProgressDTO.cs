namespace UniCabinet.Core.DTOs.StudentProgressManagment
{
    public class StudentProgressDTO
    {
        public int Id { get; set; }

        public string StudentId { get; set; }

        public int DisciplineDetailId { get; set; }

        public string DisciplineName { get; set; }
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
