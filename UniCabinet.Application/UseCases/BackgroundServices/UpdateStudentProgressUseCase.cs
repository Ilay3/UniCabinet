using Microsoft.Extensions.Logging;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.StudentManagement;
using UniCabinet.Core.DTOs.StudentProgressManagment;
using UniCabinet.Domain.Entities;

public class UpdateStudentProgressUseCase
{
    private readonly ILectureRepository _lectureRepository;
    private readonly IPracticalRepository _practicalRepository;
    private readonly IPracticalResultRepository _practicalResultRepository;
    private readonly IStudentProgressRepository _studentProgressRepository;
    private readonly ILogger<UpdateStudentProgressUseCase> _logger;

    private const int maxLecturePoints = 5;     // Максимальное количество баллов за лекции - 5
    private const int maxPracticalPoints = 15; // Максимальное количество баллов за практические занятия - 15

    public UpdateStudentProgressUseCase(
        ILectureRepository lectureRepository,
        IPracticalRepository practicalRepository,
        IPracticalResultRepository practicalResultRepository,
        IStudentProgressRepository studentProgressRepository,
        ILogger<UpdateStudentProgressUseCase> logger)
    {
        _lectureRepository = lectureRepository;
        _practicalRepository = practicalRepository;
        _practicalResultRepository = practicalResultRepository;
        _studentProgressRepository = studentProgressRepository;
        _logger = logger;
    }

    public async Task ExecuteAsync()
    {
        var today = DateTime.UtcNow.Date;

        await ProcessLecturesAsync(today);
        await ProcessPracticalsAsync(today);
    }

    private async Task ProcessLecturesAsync(DateTime date)
    {
        var lectures = await _lectureRepository.GetUnprocessedLecturesForDateAsync(date);

        if (!lectures.Any())
        {
            _logger.LogInformation("No unprocessed lectures found for today.");
            return;
        }

        foreach (var lecture in lectures)
        {
            var disciplineDetail = lecture.DisciplineDetails;

            if (disciplineDetail == null || disciplineDetail.LectureCount == 0)
            {
                _logger.LogWarning($"Discipline details missing or invalid LectureCount for Lecture ID: {lecture.Id}");
                continue;
            }

            int totalLecturesPlanned = disciplineDetail.LectureCount;

            // Группируем посещения студентов по StudentId
            var groupedVisits = lecture.LectureVisits
                .Where(lv => lv.IsVisit) // Берём только посещённые
                .GroupBy(lv => lv.StudentId);

            foreach (var group in groupedVisits)
            {
                var studentId = group.Key;

                // Посчитали количество посещённых лекций студентом (k_факт)
                int attendedLectures = group.Count();

                // Расчёт баллов по формуле
                int points = CalculateLecturePoints(attendedLectures, totalLecturesPlanned, maxLecturePoints);

                // Обновляем прогресс студента
                await UpdateStudentProgressLectureAsync(studentId, disciplineDetail.Id, points);
            }

            lecture.IsProcessed = true;
        }

        await _lectureRepository.SaveChangesAsync();
    }


    private async Task ProcessPracticalsAsync(DateTime date)
    {
        var practicals = await _practicalRepository.GetUnprocessedPracticalsForDateAsync(date);

        if (!practicals.Any())
        {
            _logger.LogInformation("No unprocessed practicals found for today.");
            return;
        }

        foreach (var practical in practicals)
        {
            var disciplineDetail = practical.DisciplineDetails;

            if (disciplineDetail == null || disciplineDetail.PracticalCount == 0)
            {
                _logger.LogWarning($"Discipline details missing or invalid PracticalCount for Practical ID: {practical.Id}");
                continue;
            }

            int totalPracticalsPlanned = disciplineDetail.PracticalCount;

            // Получаем оценки студентов по практическому занятию
            var practicalResults = await _practicalResultRepository.GetPracticalResultsByPracticalIdAsync(practical.Id);

            // Группируем результаты по студентам
            var groupedResults = practicalResults.GroupBy(pr => pr.StudentId);

            foreach (var group in groupedResults)
            {
                var studentId = group.Key;

                // Рассчитываем баллы за практические занятия
                int totalPoints = CalculatePracticalPoints(group.Select(pr => pr.Grade), totalPracticalsPlanned, maxPracticalPoints);

                // Обновляем прогресс студента
                await UpdateStudentProgressAsync(studentId, disciplineDetail.Id, totalPoints);
            }

            practical.IsProcessed = true;
        }

        await _practicalRepository.SaveChangesAsync();
    }


    private int CalculatePracticalPoints(IEnumerable<int> grades, int totalPracticalsPlanned, int maxPracticalPoints)
    {
        if (totalPracticalsPlanned <= 0)
            throw new ArgumentException("Total planned practicals must be greater than zero.");

        double multiplier = maxPracticalPoints / totalPracticalsPlanned;

        double totalPoints = grades.Sum(grade => multiplier * GetGradeCoefficient(grade));

        return (int)Math.Round(totalPoints);
    }

    private double GetGradeCoefficient(int grade)
    {
        return grade switch
        {
            5 => 1.0, // отлично
            4 => 2.0, // хорошо
            3 => 3.0, // удовлетворительно
            2 => 4.0, // неудовлетворительно
            _ => 4.0 // некорректная оценка по умолчанию
        };
    }

    private async Task UpdateStudentProgressAsync(string studentId, int disciplineDetailId, int points)
    {
        var studentProgress = await _studentProgressRepository.GetStudentProgressAsync(studentId, disciplineDetailId);

        if (studentProgress == null)
        {
            studentProgress = new StudentProgressDTO
            {
                StudentId = studentId,
                DisciplineDetailId = disciplineDetailId,
                TotalLecturePoints = 0,
                TotalPracticalPoints = points,
                TotalPoints = points,
                FinalGrade = CalculateFinalGrade(points),
                NeedsRetake = false
            };

            await _studentProgressRepository.AddStudentProgressAsync(studentProgress);
        }
        else
        {
            studentProgress.TotalPracticalPoints += points;
            studentProgress.TotalPoints += points;

            // Пересчёт итоговой оценки
            studentProgress.FinalGrade = CalculateFinalGrade(studentProgress.TotalPoints);
        }

        await _studentProgressRepository.SaveChangesAsync();
    }


    private int CalculateLecturePoints(int attendedLectures, int totalLecturesPlanned, int maxLecturePoints)
    {
        return (int)Math.Round((double)maxLecturePoints / totalLecturesPlanned * attendedLectures);
    }

    private async Task UpdateStudentProgressLectureAsync(string studentId, int disciplineDetailId, int points)
    {
        var studentProgress = await _studentProgressRepository.GetStudentProgressAsync(studentId, disciplineDetailId);

        if (studentProgress == null)
        {
            studentProgress = new StudentProgressDTO
            {
                StudentId = studentId,
                DisciplineDetailId = disciplineDetailId,
                TotalLecturePoints = points,
                TotalPracticalPoints = 0,
                TotalPoints = points,
                FinalGrade = CalculateFinalGrade(points),
                NeedsRetake = false
            };

            await _studentProgressRepository.AddStudentProgressAsync(studentProgress);
        }
        else
        {
            studentProgress.TotalLecturePoints += points;
            studentProgress.TotalPoints += points;

            // Пересчёт итоговой оценки
            studentProgress.FinalGrade = CalculateFinalGrade(studentProgress.TotalPoints);
        }

        await _studentProgressRepository.SaveChangesAsync();
    }


    private int CalculateFinalGrade(int totalPoints)
    {
        if (totalPoints >= 17) return 5;  // Отлично
        if (totalPoints >= 14) return 4;  // Хорошо
        if (totalPoints >= 11) return 3;  // Удовлетворительно
        return 2;                        // Неудовлетворительно
    }
}
