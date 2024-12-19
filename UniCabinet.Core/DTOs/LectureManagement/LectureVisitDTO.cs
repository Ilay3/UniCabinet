namespace UniCabinet.Core.DTOs.LectureManagement
{
    public class LectureVisitDTO
    {
        public int Id { get; set; }

        public string StudentId { get; set; }

        public string SudentFirstName { get; set; }

        public string StudentLastName { get; set; }

        public string StudentPatronymic { get; set; }

        public int LectureId { get; set; }

        public string LectureName { get; set; }

        /// <summary>
        /// Посещаемость
        /// </summary>
        public bool IsVisit { get; set; }

        /// <summary>
        /// Начисленные баллы
        /// </summary>
        public decimal PointsCount { get; set; }
    }
}
