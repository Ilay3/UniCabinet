using AutoMapper;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.DisciplineDetailManagment;

namespace UniCabinet.Application.UseCases.DisciplineDetailUseCase
{
    public class GetDetailByIdUseCase
    {
        private readonly IDisciplineDetailRepository _repository;

        public GetDetailByIdUseCase(IDisciplineDetailRepository repository)
        {
            _repository = repository;

        }

        public async Task<List<DisciplineDetailDTO>> ExecuteAsync(int disciplineId, string teacherId, int? courseId, int? semesterId, int? groupId)
        {
            return await _repository.GetByDisciplineTeacherAndFiltersAsync(disciplineId, teacherId, courseId, semesterId, groupId);
        }

    }
}
