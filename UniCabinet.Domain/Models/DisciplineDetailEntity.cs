﻿namespace UniCabinet.Domain.Entities;

/// <summary>
/// Детали дисцеплины
/// </summary>
public class DisciplineDetailEntity
{
    public int Id { get; set; }

    public int DisciplineId { get; set; }
    public DisciplineEntity Discipline { get; set; }

    public int GroupId { get; set; }
    public GroupEntity Group { get; set; }

    public int SemesterId { get; set; }
    public SemesterEntity Semester { get; set; }

    public int CourseId { get; set; }
    public CourseEntity Course { get; set; }

    public string TeacherId { get; set; }
    public UserEntity Teacher { get; set; }

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

    public ICollection<LectureEntity> Lectures { get; set; }
    public ICollection<PracticalEntity> Practicals { get; set; }
    public ICollection<ExamEntity> Exams { get; set; }
    public ICollection<StudentProgressEntity> StudentProgresses { get; set; }
}