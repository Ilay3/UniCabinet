using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.PracticalManagement;

namespace UniCabinet.Application.UseCases.PracticalUseCase
{
    public class SavePracticalAttendanceUseCase
    {
        private readonly IPracticalResultRepository _practicalResultRepository;

        public SavePracticalAttendanceUseCase(IPracticalResultRepository practicalResultRepository)
        {
            _practicalResultRepository = practicalResultRepository;
        }

        public async Task ExecuteAsync(PracticalAttendanceDTO practicalAttendanceDTO)
        {
            foreach (var student in practicalAttendanceDTO.Students)
            {
                var practicalResultDTO = new PracticalResultDTO
                {
                    PracticalId = practicalAttendanceDTO.PracticalId,
                    StudentId = student.StudentId,
                    Grade = student.Grade ?? 0
                };

                await _practicalResultRepository.AddOrUpdatePracticalResultAsync(practicalResultDTO);
            }
        }
    }
}
