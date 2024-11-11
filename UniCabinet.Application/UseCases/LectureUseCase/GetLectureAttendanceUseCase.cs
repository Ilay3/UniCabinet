using UniCabinet.Application.Interfaces;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.Models.ViewModel;
using UniCabinet.Core.Models.ViewModel.Lecture;

namespace UniCabinet.Application.UseCases.LectureUseCase
{
    public class GetLectureAttendanceUseCase
    {
        private readonly ILectureRepository _lectureRepository;
        private readonly IDisciplineDetailRepository _disciplineDetailRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILectureVisitRepository _lectureVisitRepository;
        private readonly IDisciplineRepository _disciplineRepository;

        public GetLectureAttendanceUseCase(
            ILectureRepository lectureRepository,
            IDisciplineDetailRepository disciplineDetailRepository,
            IUserRepository userRepository,
            ILectureVisitRepository lectureVisitRepository,
            IDisciplineRepository disciplineRepository)
        {
            _lectureRepository = lectureRepository;
            _disciplineDetailRepository = disciplineDetailRepository;
            _userRepository = userRepository;
            _lectureVisitRepository = lectureVisitRepository;
            _disciplineRepository = disciplineRepository;
        }

        public async Task<LectureAttendanceVM> ExecuteAsync(int lectureId)
        {
            var lecture = await _lectureRepository.GetLectureByIdAsync(lectureId);
            if (lecture == null)
            {
                return null;
            }

            int disciplineDetailId = lecture.DisciplineDetailId;
            var disciplineDetail = _disciplineDetailRepository.GetDisciplineDetailByIdAsync(disciplineDetailId);
            int groupId = disciplineDetail.GroupId;

            var students = _userRepository.GetStudentsByGroupIdAsync(groupId);
            var existingVisits = _lectureVisitRepository.GetLectureVisitsByLectureIdAsync(lectureId)
                .ToDictionary(lv => lv.StudentId, lv => lv);

            var attendanceVM = new LectureAttendanceVM
            {
                LectureId = lectureId,
                DisciplineDetailId = disciplineDetailId,
                LectureNumber = lecture.Number,
                DisciplineName = _disciplineRepository.GetDisciplineByIdAsync(disciplineDetail.DisciplineId).Name,
                Students = students.Select(s => new StudentAttendanceVM
                {
                    StudentId = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Patronymic = s.Patronymic,
                    IsPresent = existingVisits.ContainsKey(s.Id) ? existingVisits[s.Id].IsVisit : false
                }).ToList()
            };

            return attendanceVM;
        }
    }
}
