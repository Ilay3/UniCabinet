using AutoMapper;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.DisciplineDetailManagment;

namespace UniCabinet.Application.UseCases.DisciplineDetailUseCase
{
    public class GetDetailsByTeacherUseCase
    {
        private readonly IDisciplineDetailRepository _repository;
        private readonly IMapper _mapper;

        public GetDetailsByTeacherUseCase(IDisciplineDetailRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DisciplineDetailDTO> ExecuteAsyn(int detailId)
        {
            return  await _repository.GetByIdAsync(detailId);
        }
    }
}
