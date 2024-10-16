using UniCabinet.Domain.DTO;
using UniCabinet.Web.ViewModel.Lecture;

namespace UniCabinet.Web.Extension.Lecture
{
    public static class LectureViewModelExtension
    {
        public static async Task<IEnumerable<LectureViewModel>> GetLectureViewModelAsync(this IEnumerable<LectureDTO> dtoModels)
        {
            return await Task.Run(() =>
            {
                return dtoModels.Select(viewModel => new LectureViewModel
                {
                    Id = viewModel.Id,
                    LectureNumber = viewModel.LectureNumber,
                    Date = viewModel.Date,
                }).ToList();
            });
        }

        public static LectureViewModel GetLectureViewNodel(this LectureDTO dto)
        {
            var lecture = new LectureViewModel
            {
                Id = dto.Id,
                Date = dto.Date,
                LectureNumber = dto.LectureNumber,
            };

            return lecture;
        }
        
    }
}
