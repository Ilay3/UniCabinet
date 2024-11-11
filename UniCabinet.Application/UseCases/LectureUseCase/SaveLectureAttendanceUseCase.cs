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

        public void Execute(LectureAttendanceVM viewModel)
        {
            decimal points = CalculatePointsForLecture(viewModel.LectureId);

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

        private decimal CalculatePointsForLecture(int lectureId)
        {
            var lecture = _lectureRepository.GetLectureByIdAsync(lectureId);
            return lecture.PointsCount;
        }
    }
}
