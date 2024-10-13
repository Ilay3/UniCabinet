using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniCabinet.Domain.Entities
{
    public class User : IdentityUser<string>
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Patronymic { get; set; }

        public DateTime? DateBirthday { get; set; }

        public bool IsVerified()
        {
            return !string.IsNullOrEmpty(FirstName) &&
                   !string.IsNullOrEmpty(LastName) &&
                   !string.IsNullOrEmpty(Patronymic) &&
                   DateBirthday.HasValue &&
                   EmailConfirmed;
        }

        [NotMapped]
        public override string PhoneNumber { get; set; }

        [NotMapped]
        public override bool TwoFactorEnabled { get; set; }

        [NotMapped]
        public override bool PhoneNumberConfirmed { get; set; }


        public int? GroupId { get; set; }

        public Group Group { get; set; }

        public int? SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }

        // Навигационные свойства
        public ICollection<LectureVisit> LectureVisits { get; set; }
        public ICollection<PracticalResult> PracticalResults { get; set; }
        public ICollection<ExamResult> ExamResults { get; set; }
        public ICollection<StudentProgress> StudentProgresses { get; set; }
    }
}
