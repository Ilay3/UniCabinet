namespace UniCabinet.Domain.Entities
{
    public class PracticalResult
    {
        public int ResultId { get; set; }
        public string StudentId { get; set; }
        public User Student { get; set; }

        public int PracticalId { get; set; }
        public Practical Practical { get; set; }

        public int Grade { get; set; }
        public int PointsAwarded { get; set; }
    }
}
