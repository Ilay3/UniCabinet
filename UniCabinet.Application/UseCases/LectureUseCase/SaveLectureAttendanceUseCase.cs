using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs;
using UniCabinet.Core.Models.ViewModel.Lecture;

namespace UniCabinet.Application.UseCases.LectureUseCase
{
    public class SaveLectureAttendanceUseCase
    {
        private readonly ILectureVisitRepository _lectureVisitRepository;
        private readonly ILectureRepository _lectureRepository;

        public SaveLectureAttendanceUseCase(
            ILectureVisitRepository lectureVisitRepository,
            ILectureRepository lectureRepository)
        {
            _lectureVisitRepository = lectureVisitRepository;
            _lectureRepository = lectureRepository;
        }

        public async Task ExecuteAsync(LectureAttendanceVM viewModel)
        {
            decimal points = await CalculatePointsForLectureAsync(viewModel.LectureId);

            foreach (var studentAttendance in viewModel.Students)
            {
                var lectureVisitDTO = new LectureVisitDTO
                {
                    LectureId = viewModel.LectureId,
                    StudentId = studentAttendance.StudentId,
                    isVisit = studentAttendance.IsPresent,
                    PointsCount = studentAttendance.IsPresent ? points : 0
                };

                _lectureVisitRepository.AddOrUpdateLectureVisitAsync(lectureVisitDTO);
            }
        }

        private async Task<decimal> CalculatePointsForLectureAsync(int lectureId)
        {
            var lecture = await _lectureRepository.GetLectureByIdAsync(lectureId);
            return lecture.PointsCount;
        }
    }
}
