namespace UniCabinet.Core.DTOs.Entites
{
    public class ExamResultDTO
    {
        public int Id { get; set; }

        public string StudentId { get; set; }

        public int ExamId { get; set; }

        /// <summary>
        /// Средняя оценка по баллам
        /// </summary>
        public decimal PointAvarage { get; set; }

        /// <summary>
        /// Окончательная оценка
        /// </summary>
        public decimal FinalPoint { get; set; }

        /// <summary>
        /// Согласен ли препод с оценкой
        /// </summary>
        public bool IsAutomatic { get; set; }
    }
}
