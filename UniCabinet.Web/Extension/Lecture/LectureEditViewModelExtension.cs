using UniCabinet.Domain.DTO;
using UniCabinet.Web.ViewModel.Lecture;

namespace UniCabinet.Web.Extension.Lecture
{
    public static class LectureEditViewModelExtension
    {
        public static LectureEditViewModel GetLectureEditViewModel(this LectureDTO modelDTO)
        {
            var lecture = new LectureEditViewModel
            {
                Id = modelDTO.Id,
                Date = modelDTO.Date,
                DisciplineDetailId = modelDTO.DisciplineDetailId,
                Number = modelDTO.Number,
            };

            return lecture;
        }
    }
}
