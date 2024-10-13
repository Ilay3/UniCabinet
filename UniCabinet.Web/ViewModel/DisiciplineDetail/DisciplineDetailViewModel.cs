namespace UniCabinet.Web.ViewModel.DisiciplineDetail
{
    public class DisciplineDetailViewModel
    {
        public int Id { get; set; }

        public string DisciplineName { get; set; }

        public string GroupName { get; set; }

        public int CourseNumber { get; set; }

        public int SemesterNumber { get; set; }

        public string TeacherFirstName { get; set; }

        public string TeacherLastName { get; set; }

        public string TeacherPatronymic { get; set; }

        /// <summary>
        /// Количество лекций
        /// </summary>
        public int LectureCount { get; set; }

        /// <summary>
        /// Количество практических
        /// </summary>
        public int PracticalCount { get; set; }

        /// <summary>
        /// Количество зачетов
        /// </summary>
        public int SubExamCount { get; set; }

        /// <summary>
        /// Количество экзаменов
        /// </summary>
        public int ExamCount { get; set; }

        /// <summary>
        /// Минимум посещений лекций
        /// </summary>
        public int MinLecturesRequired { get; set; }

        /// <summary>
        /// Минимум посещений практических
        /// </summary>
        public int MinPracticalsRequired { get; set; }

        /// <summary>
        /// Минимум для автомата
        /// </summary>
        public int AutoExamThreshold { get; set; }

        /// <summary>
        /// Минимальный балл для прохождения
        /// </summary>
        public int PassCount { get; set; }
    }
}
