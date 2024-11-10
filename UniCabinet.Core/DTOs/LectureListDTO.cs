using UniCabinet.Core.DTOs.Entites;

namespace UniCabinet.Core.DTOs
{
    public class LectureListDTO
    {
        public string DisciplineName { get; set; }
        public int MaxLectures { get; set; }
        public List<LectureDTO> LectureDTO { get; set; }
    }
}
