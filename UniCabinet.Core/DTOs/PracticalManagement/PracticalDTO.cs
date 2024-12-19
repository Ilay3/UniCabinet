namespace UniCabinet.Core.DTOs.PracticalManagement
{
    public class PracticalDTO
    {
        public int Id { get; set; }

        public int DisciplineDetailId { get; set; }

        /// <summary>
        /// Номер практической
        /// </summary>
        public string PracticalName { get; set; }

        /// <summary>
        /// Дата проведения
        /// </summary>
        public DateTime Date { get; set; }
        public bool IsProcessed { get; set; }

    }
}
