using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs;
using UniCabinet.Core.DTOs.LectureManagement;
using UniCabinet.Core.Models.ViewModel.Lecture;

namespace UniCabinet.Application.UseCases.LectureUseCase
{
    public class SaveLectureAttendanceUseCase
    {
        private readonly ILectureVisitRepository _lectureVisitRepository;


        public SaveLectureAttendanceUseCase(
            ILectureVisitRepository lectureVisitRepository)
        {
            _lectureVisitRepository = lectureVisitRepository;
        }

        public async Task ExecuteAsync(LectureAttendanceDTO lectureAttendanceDTO)
        {
            var lectureVisitDTOs = lectureAttendanceDTO.Students.Select(studentAttendance => new LectureVisitDTO
            {
                LectureId = lectureAttendanceDTO.LectureId,
                StudentId = studentAttendance.StudentId,
                IsVisit = studentAttendance.IsPresent,
                PointsCount = studentAttendance.IsPresent ? 5 : 0
            }).ToList();

            await _lectureVisitRepository.AddOrUpdateLectureVisitsAsync(lectureVisitDTOs);
        }




    }
}
