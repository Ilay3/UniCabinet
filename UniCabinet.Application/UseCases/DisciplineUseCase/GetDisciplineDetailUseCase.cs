using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.CourseManagement;

namespace UniCabinet.Application.UseCases.DisciplineUseCase;

public class GetDisciplineDetailUseCase
{
    private readonly IDisciplineDetailRepository _disciplineDetailRepository;

    public GetDisciplineDetailUseCase(IDisciplineDetailRepository disciplineDetailRepository)
    {
        _disciplineDetailRepository = disciplineDetailRepository;
    }
    public async Task<DisciplineDetailDTO> ExecuteAsync(string userId, int disciplineId)
    {
        // Проверка входных параметров
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentException("User ID cannot be null or empty", nameof(userId));

        // Получение деталей дисциплины
        var disciplineDetail = await _disciplineDetailRepository.GetDetailByUserAndDisciplineAsync(userId, disciplineId);

        if (disciplineDetail == null)
            throw new KeyNotFoundException($"Discipline detail not found for User ID: {userId} and Discipline ID: {disciplineId}");

        return disciplineDetail;
    }
}
