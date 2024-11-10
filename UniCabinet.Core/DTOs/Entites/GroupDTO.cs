namespace UniCabinet.Core.DTOs.Entites
{
    public class GroupDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Очно/Заочно 
        /// </summary>
        public string TypeGroup { get; set; }

        public int CourseId { get; set; }

        public int SemesterId { get; set; }
    }
}
