using UniCabinet.Domain.DTO;
using UniCabinet.Web.ViewModel.Discipline;

namespace UniCabinet.Web.Mapping
{
    public static class DisciplineEditToDTO
    {
        public static DisciplineDTO GetDisciplineDTO(this DisciplineEditViewModel viewModel)
        {
            var discipline = new DisciplineDTO
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Description = viewModel.Description,
            };

            return discipline;
        }
    }
}
