using UniCabinet.Domain.DTO;
using UniCabinet.Web.ViewModel.Lecture;

namespace UniCabinet.Web.Extension.Lecture
{
    public static class LectureViewModelExtension
    {
        public static async Task<IEnumerable<LectureViewModel>> GetLectureViewModelAsync(this IEnumerable<LectureDTO> viewModels)
        {
            return await Task.Run(() =>
            {
                return viewModels.Select(viewModel => new LectureViewModel
                {
                    Id = viewModel.Id,
                    LectureNumber = viewModel.LectureNumber,
                    Date = viewModel.Date,
                }).ToList();
            });
        }
        
    }
}
