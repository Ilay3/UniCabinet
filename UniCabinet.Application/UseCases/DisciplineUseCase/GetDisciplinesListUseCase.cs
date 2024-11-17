using AutoMapper;
using UniCabinet.Application.Interfaces;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.CourseManagement;
using UniCabinet.Core.Models.ViewModel.Discipline;

namespace UniCabinet.Application.UseCases.DisciplineUseCase
{
    public class GetDisciplinesListUseCase
    {
        private readonly IDisciplineRepository _disciplineRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetDisciplinesListUseCase(IDisciplineRepository disciplineRepository, IMapper mapper, IUserRepository userRepository)
        {
            _disciplineRepository = disciplineRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<List<DisciplineDTO>> ExecuteAsync(string userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {userId} not found");

            // Получить дисциплины, связанные с пользователем через SpecialtyId
            var disciplines = await _disciplineRepository.GetDisciplinesBySpecialtyIdAsync(user.SpecialtyId);

            return _mapper.Map<List<DisciplineDTO>>(disciplines);
        }
    }
}
