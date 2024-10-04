using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Domain.DTO;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Repository
{
    public class StudentProgressRepository : IStudentProgressRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentProgressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public StudentProgressDTO GetStudentProfressById(int id)
        {
            var studentProgressEntity = _context.StudentProgresses.Find(id);
            if (studentProgressEntity == null) return null;

            return new StudentProgressDTO
            {
                DisciplineDetailId = studentProgressEntity.DisciplineDetailId,
                StudentId = studentProgressEntity.StudentId,
                FinalGrade = studentProgressEntity.FinalGrade,
                NeedsRetake = studentProgressEntity.NeedsRetake,
                TotalLecturePoints = studentProgressEntity.TotalLecturePoints,
                TotalPoints = studentProgressEntity.TotalPoints,
                TotalPracticalPoints = studentProgressEntity.TotalPracticalPoints,
            };
        }

        public List<StudentProgressDTO> GetAllStudentProgress()
        {
            var studentProgressEntity = _context.StudentProgresses.ToList();

            return studentProgressEntity.Select(d => new StudentProgressDTO
            {
                Id = d.Id,
                StudentId = d.StudentId,
                DisciplineDetailId = d.DisciplineDetailId,
                FinalGrade = d.FinalGrade,
                NeedsRetake = d.NeedsRetake,
                TotalLecturePoints = d.TotalLecturePoints,
                TotalPoints = d.TotalPoints,
                TotalPracticalPoints = d.TotalPracticalPoints,
            }).ToList();
        }

        public async Task AddStudentProgressAsync(StudentProgressDTO studentProgressDTO)
        {
            var studentProgressEntity = new StudentProgress
            {
                StudentId = studentProgressDTO.StudentId,
                DisciplineDetailId = studentProgressDTO.DisciplineDetailId,
                FinalGrade = studentProgressDTO.FinalGrade,
                NeedsRetake = studentProgressDTO.NeedsRetake,
                TotalLecturePoints = studentProgressDTO.TotalLecturePoints,
                TotalPracticalPoints = studentProgressDTO.TotalPracticalPoints,
                TotalPoints = studentProgressDTO.TotalPoints,
            };

            await _context.StudentProgresses.AddAsync(studentProgressEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudentProgress(int id)
        {
            var studentProgressEntity = await _context.StudentProgresses.FindAsync(id);
            if (studentProgressEntity != null)
            {
                _context.StudentProgresses.Remove(studentProgressEntity);
                await _context.SaveChangesAsync();
            }
        }

        public void UpdateStudentProgress(StudentProgressDTO studentProgressDTO)
        {
            var studentProgressEntity = _context.StudentProgresses.FirstOrDefault(d => d.Id == studentProgressDTO.Id);
            if (studentProgressEntity == null) return;

            studentProgressEntity.TotalLecturePoints = studentProgressDTO.TotalLecturePoints;
            studentProgressEntity.NeedsRetake = studentProgressDTO.NeedsRetake;
            studentProgressEntity.FinalGrade = studentProgressDTO.FinalGrade;
            studentProgressEntity.TotalPracticalPoints = studentProgressDTO.TotalPracticalPoints;
            studentProgressEntity.DisciplineDetailId = studentProgressDTO.DisciplineDetailId;
            studentProgressEntity.StudentId = studentProgressDTO.StudentId;
            studentProgressEntity.TotalPoints = studentProgressDTO.TotalPoints;

            _context.SaveChanges();
        }
    }
}
