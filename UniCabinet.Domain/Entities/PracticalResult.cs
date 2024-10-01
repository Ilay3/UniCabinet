namespace UniCabinet.Domain.Entities
{
    public class PracticalResult
    {
        public int Id { get; set; }

        public string StudentId { get; set; }
        public User Student { get; set; }

        public int PracticalId { get; set; }
        public Practical Practical { get; set; }

        /// <summary>
        /// Оценка
        /// </summary>
        public int Grade { get; set; }

        /// <summary>
        /// Баллы
        /// </summary>
        public int Point { get; set; }
    }
}
