using UniCabinet.Domain.DTO;
using UniCabinet.Web.ViewModel.Discipline;

namespace UniCabinet.Web.Extension.Discipline
{
    public static class DisciplineViewModelExtension
    {
        public static DisciplineViewModel GetDisciplineViewModel(this DisciplineDTO disciplineDTO)
        {
            var disciplines = new DisciplineViewModel
            {
                Id = disciplineDTO.Id,
                Description = disciplineDTO.Description,
                Name = disciplineDTO.Name,
            };

            return disciplines;
        }
    }
}
