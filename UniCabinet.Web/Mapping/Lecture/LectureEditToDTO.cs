using UniCabinet.Domain.DTO;
using UniCabinet.Web.ViewModel.Lecture;

namespace UniCabinet.Web.Mapping.Lecture
{
    public static class LectureEditToDTO
    {
        public static LectureDTO GetLectureDTO(this LectureEditViewModel viewModel)
        {
            var lecture = new LectureDTO
            {
                Id = viewModel.Id,
                Date = viewModel.Date,
                DisciplineDetailId = viewModel.DisciplineDetailId,
                Number = viewModel.Number,
                PointsCount = viewModel.PointsCount
            };

            return lecture;
        }
    }
}
