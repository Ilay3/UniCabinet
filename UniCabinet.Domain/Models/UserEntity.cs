using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using UniCabinet.Domain.Models;

namespace UniCabinet.Domain.Entities;

public class UserEntity : IdentityUser<string>
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

    public GroupEntity Group { get; set; }

    public int? SpecialtyId { get; set; }
    public SpecialtyEntity Specialty { get; set; }

    // Навигационные свойства
    public DepartmentEntity DepartmentEntity { get; set; }
    public ICollection<LectureVisitEntity> LectureVisits { get; set; }
    public ICollection<PracticalResultEntity> PracticalResults { get; set; }
    public ICollection<ExamResultEntity> ExamResults { get; set; }
    public ICollection<StudentProgressEntity> StudentProgresses { get; set; }
}
