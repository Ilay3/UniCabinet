using UniCabinet.Domain.DTO;
using UniCabinet.Domain.Entities;
using UniCabinet.Web.ViewModel.Discipline;

namespace UniCabinet.Web.Mapping.Discipline
{
    public static class DisciplineAddToDTO
    {
        public static DisciplineDTO GetDisciplineDTO(this DisciplineAddViewModel viewModel)
        {
            var discipline = new DisciplineDTO
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
            };

            return discipline;
        }
    }
}
