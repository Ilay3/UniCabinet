using UniCabinet.Domain.DTO;
using UniCabinet.Web.ViewModel;

public static class GroupsViewModelExtension
{
    public static GroupViewModel GetGroupViewModel(this GroupDTO dto, int courseNumber, int semesterNumber)
    {
        return new GroupViewModel
        {
            CourseNumber = courseNumber,
            SemesterNumber = semesterNumber,
            Id = dto.Id,
            Name = dto.Name,
            TypeGroup = dto.TypeGroup,
        };
    }
}
