using System.Collections.Generic;

namespace UniCabinet.Domain.Entities
{
    public class DisciplineOffering
    {
        public int DisciplineOfferingId { get; set; }
        public int DisciplineId { get; set; }
        public Discipline Discipline { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }

        public int SemesterId { get; set; }
        public Semester Semester { get; set; }

        public string TeacherId { get; set; }
        public User Teacher { get; set; }

        public int LectureCount { get; set; }
        public int PracticalCount { get; set; }
        public int CreditCount { get; set; }
        public int ExamCount { get; set; }
        public int MinLecturesRequired { get; set; }
        public int MinPracticalsRequired { get; set; }
        public int AutoExamThreshold { get; set; }
        public int PassingScore { get; set; }

        // Навигационные свойства
        public ICollection<Lecture> Lectures { get; set; }
        public ICollection<Practical> Practicals { get; set; }
        public ICollection<Exam> Exams { get; set; }
        public ICollection<StudentProgress> StudentProgresses { get; set; }
    }
}
