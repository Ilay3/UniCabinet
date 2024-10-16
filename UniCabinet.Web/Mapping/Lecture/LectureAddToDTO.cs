using UniCabinet.Domain.DTO;
using UniCabinet.Web.ViewModel.Lecture;

namespace UniCabinet.Web.Mapping.Lecture
{
    public static class LectureAddToDTO
    {
        public static LectureDTO GetLectureDTO(this LectureAddViewModel viewModel)
        {
            var lecture = new LectureDTO
            {
                DisciplineDetailId = viewModel.DisciplineDetailId,
                Date = viewModel.Date,
                LectureNumber = viewModel.LectureNumber,
            };

            return lecture;
        }
    }
}
