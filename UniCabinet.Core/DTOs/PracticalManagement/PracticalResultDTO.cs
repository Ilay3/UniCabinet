namespace UniCabinet.Core.DTOs.PracticalManagement
{
    public class PracticalResultDTO
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public int PracticalId { get; set; }
        public int Grade { get; set; }


        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string StudentPatronymic { get; set; }
        public int PracticalNumber { get; set; }
    }
}
