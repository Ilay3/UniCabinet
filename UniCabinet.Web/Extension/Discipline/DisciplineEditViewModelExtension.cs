using UniCabinet.Domain.DTO;
using UniCabinet.Web.ViewModel.Discipline;

namespace UniCabinet.Web.Extension.Discipline
{
    public static class DisciplineEditViewModelExtension
    {
        public static DisciplineEditViewModel GetDisciplineEditViewModel(this DisciplineDTO disciplineDTO)
        {
            var disciplines = new DisciplineEditViewModel
            {
                Id = disciplineDTO.Id,
                Description = disciplineDTO.Description,
                Name = disciplineDTO.Name,
            };

            return disciplines;
        }
    }
}
