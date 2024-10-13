using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Application.Interfaces.Services;
using UniCabinet.Domain.DTO;

namespace UniCabinet.Application.Services
{
    public class LectureService : ILectureService
    {
        private readonly ILectureRepository _lectureRepository;
        private readonly IDisciplineDetailRepository _disciplineDetailRepository;
        private readonly IDisciplineRepository _disciplineRepository;
        public LectureService(ILectureRepository lectureRepository,
            IDisciplineDetailRepository disciplineDetailRepository,
            IDisciplineRepository disciplineRepository)
        {
            _lectureRepository = lectureRepository;
            _disciplineDetailRepository = disciplineDetailRepository;
            _disciplineRepository = disciplineRepository;
        }

        public async Task<string> GetDisciplineById(int id)
        {
            var disciplineDetailDTO = await _disciplineDetailRepository.GetDisciplineDetailById(id);
            var disciplineDTO = await _disciplineRepository.GetDisciplineById(disciplineDetailDTO.DisciplineId);

            return disciplineDTO.Name;
        }
    }
}
