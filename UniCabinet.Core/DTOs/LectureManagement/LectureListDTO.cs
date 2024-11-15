namespace UniCabinet.Core.DTOs.LectureManagement
{
    public class LectureListDTO
    {
        public string DisciplineName { get; set; }
        public int MaxLectures { get; set; }
        public List<LectureDTO> LectureDTO { get; set; }
    }
}
