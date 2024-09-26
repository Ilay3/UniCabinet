using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace UniCabinet.Domain.Entities
{
    public class User : IdentityUser<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int? GroupId { get; set; }
        public Group Group { get; set; }

        // Навигационные свойства
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<LectureAttendance> LectureAttendances { get; set; }
        public ICollection<PracticalResult> PracticalResults { get; set; }
        public ICollection<ExamResult> ExamResults { get; set; }
        public ICollection<StudentProgress> StudentProgresses { get; set; }
    }
}
