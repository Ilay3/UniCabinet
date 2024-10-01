using System.Collections.Generic;

namespace UniCabinet.Domain.Entities
{
    /// <summary>
    /// Детали дисцеплины
    /// </summary>
    public class DisciplineDetail
    {
        public int Id { get; set; }

        public int DisciplineId { get; set; }
        public Discipline Discipline { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }

        public int SemesterId { get; set; }
        public Semester Semester { get; set; }

        public string TeacherId { get; set; }
        public User Teacher { get; set; }

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

        // Навигационные свойства
        public ICollection<Lecture> Lectures { get; set; }

        public ICollection<Practical> Practicals { get; set; }

        public ICollection<Exam> Exams { get; set; }

        public ICollection<StudentProgress> StudentProgresses { get; set; }
    }
}
