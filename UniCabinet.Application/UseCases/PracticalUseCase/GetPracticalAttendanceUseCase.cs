using AutoMapper;
using UniCabinet.Application.Interfaces;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.PracticalManagement;
using UniCabinet.Core.DTOs.StudentManagement;
using UniCabinet.Core.DTOs.UserManagement;

namespace UniCabinet.Application.UseCases.PracticalUseCase
{
    public class GetPracticalAttendanceUseCase
    {
        private readonly IPracticalRepository _practicalRepository;
        private readonly IDisciplineDetailRepository _disciplineDetailRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPracticalResultRepository _practicalResultRepository;
        private readonly IDisciplineRepository _disciplineRepository;
        private readonly IMapper _mapper;

        public GetPracticalAttendanceUseCase(
            IPracticalRepository practicalRepository,
            IDisciplineDetailRepository disciplineDetailRepository,
            IUserRepository userRepository,
            IPracticalResultRepository practicalResultRepository,
            IDisciplineRepository disciplineRepository,
            IMapper mapper)
        {
            _practicalRepository = practicalRepository;
            _disciplineDetailRepository = disciplineDetailRepository;
            _userRepository = userRepository;
            _practicalResultRepository = practicalResultRepository;
            _disciplineRepository = disciplineRepository;
            _mapper = mapper;
        }

        public async Task<PracticalAttendanceDTO> ExecuteAsync(int practicalId)
        {
            var practical = await _practicalRepository.GetPracticalByIdAsync(practicalId);
            if (practical == null)
            {
                return null;
            }

            int disciplineDetailId = practical.DisciplineDetailId;

            var disciplineDetail = await _disciplineDetailRepository.GetByIdAsync(disciplineDetailId);
            if (disciplineDetail == null)
            {
                return null;
            }

            int groupId = disciplineDetail.GroupId;

            var studentUsers = await _userRepository.GetStudentsByGroupIdAsync(groupId);

            var existingResultsList = await _practicalResultRepository.GetPracticalResultsByPracticalIdAsync(practicalId);
            var existingResults = existingResultsList.ToDictionary(pr => pr.StudentId, pr => pr);

            var discipline = await _disciplineRepository.GetDisciplineByIdAsync(disciplineDetail.DisciplineId);
            string disciplineName = discipline != null ? discipline.Name : string.Empty;

            var attendanceDTO = new PracticalAttendanceDTO
            {
                PracticalId = practicalId,
                DisciplineDetailId = disciplineDetailId,
                PracticalNumber = practical.PracticalNumber,
                DisciplineName = disciplineName,
                Students = studentUsers.Select(s => new StudentGradeDTO
                {
                    StudentId = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Patronymic = s.Patronymic,
                    Grade = existingResults.ContainsKey(s.Id) ? existingResults[s.Id].Grade : (int?)null
                }).ToList()
            };

            return attendanceDTO;
        }
    }
}
