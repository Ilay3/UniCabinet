namespace UniCabinet.Core.DTOs.Entites
{
    public class PracticalDTO
    {
        public int Id { get; set; }

        public int DisciplineDetailId { get; set; }

        /// <summary>
        /// Номер практической
        /// </summary>
        public int PracticalNumber { get; set; }

        /// <summary>
        /// Дата проведения
        /// </summary>
        public DateTime Date { get; set; }
    }
}
