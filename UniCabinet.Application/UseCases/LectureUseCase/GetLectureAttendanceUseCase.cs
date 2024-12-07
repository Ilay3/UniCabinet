using UniCabinet.Application.Interfaces;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.LectureManagement;
using UniCabinet.Core.DTOs.StudentManagement;
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

        public async Task<LectureAttendanceDTO> ExecuteAsync(int lectureId)
        {
            var lecture = await _lectureRepository.GetLectureByIdAsync(lectureId);
            if (lecture == null)
            {
                return null;
            }

            int disciplineDetailId = lecture.DisciplineDetailId;

            var disciplineDetail = await _disciplineDetailRepository.GetByIdAsync(disciplineDetailId);
            if (disciplineDetail == null)
            {
                return null;
            }

            int groupId = disciplineDetail.GroupId;

            var students = await _userRepository.GetStudentsByGroupIdAsync(groupId);


            var existingVisitsList = await _lectureVisitRepository.GetLectureVisitsByLectureIdAsync(lectureId);
            var existingVisits = existingVisitsList.ToDictionary(lv => lv.StudentId, lv => lv);

            var discipline = await _disciplineRepository.GetDisciplineByIdAsync(disciplineDetail.DisciplineId);
            string disciplineName = discipline != null ? discipline.Name : string.Empty;

            var attendanceDTO = new LectureAttendanceDTO
            {
                LectureId = lectureId,
                DisciplineDetailId = disciplineDetailId,
                LectureNumber = lecture.Number,
                DisciplineName = disciplineName,
                Students = students.Select(s => new StudentAttendanceDTO
                {
                    StudentId = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Patronymic = s.Patronymic,
                    IsPresent = existingVisits.ContainsKey(s.Id) ? existingVisits[s.Id].IsVisit : false
                }).ToList()
            };

            return attendanceDTO;
        }
    }
}
