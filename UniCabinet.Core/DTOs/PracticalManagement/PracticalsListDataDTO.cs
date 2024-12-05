using System.Collections.Generic;

namespace UniCabinet.Core.DTOs.PracticalManagement
{
    public class PracticalsListDataDTO
    {
        public string DisciplineName { get; set; }
        public int MaxPracticals { get; set; }
        public List<PracticalDTO> PracticalDTO { get; set; }
    }
}
