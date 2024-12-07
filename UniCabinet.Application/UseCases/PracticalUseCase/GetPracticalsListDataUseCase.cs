using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.PracticalManagement;

namespace UniCabinet.Application.UseCases.PracticalUseCase
{
    public class GetPracticalsListDataUseCase
    {
        private readonly IPracticalRepository _practicalRepository;
        private readonly IDisciplineDetailRepository _disciplineDetailRepository;
        private readonly IDisciplineRepository _disciplineRepository;

        public GetPracticalsListDataUseCase(
            IPracticalRepository practicalRepository,
            IDisciplineDetailRepository disciplineDetailRepository,
            IDisciplineRepository disciplineRepository)
        {
            _practicalRepository = practicalRepository;
            _disciplineDetailRepository = disciplineDetailRepository;
            _disciplineRepository = disciplineRepository;
        }

        public async Task<PracticalsListDataDTO> ExecuteAsync(int disciplineDetailId)
        {
            var practicals = await _practicalRepository.GetPracticalListByDisciplineDetailIdAsync(disciplineDetailId);
            var disciplineDetail = await _disciplineDetailRepository.GetByIdAsync(disciplineDetailId);
            var discipline = await _disciplineRepository.GetDisciplineByIdAsync(disciplineDetail.DisciplineId);

            var result = new PracticalsListDataDTO
            {
                DisciplineName = discipline.Name,
                MaxPracticals = disciplineDetail.PracticalCount,
                PracticalDTO = practicals
            };

            return result;
        }
    }
}
