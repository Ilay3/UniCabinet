// Файл: UniCabinet.Web/Extension/Lecture/LectureViewModelExtension.cs
using UniCabinet.Domain.DTO;
using UniCabinet.Web.ViewModel.Lecture;

namespace UniCabinet.Web.Extension.Lecture
{
    public static class LectureViewModelExtension
    {
        public static LectureViewModel GetLectureViewModel(this LectureDTO dto)
        {
            return new LectureViewModel
            {
                Id = dto.Id,
                Date = dto.Date,
                Number = dto.Number,
            };
        }
    }
}
