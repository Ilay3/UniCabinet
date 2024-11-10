using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.Entites;
using System;

namespace UniCabinet.Application.UseCases.GroupUseCase
{
    public class GetGroupEditModalUseCase
    {
        private readonly IGroupRepository _groupRepository;
        private readonly ISemesterRepository _semesterRepository;

        public GetGroupEditModalUseCase(IGroupRepository groupRepository, ISemesterRepository semesterRepository)
        {
            _groupRepository = groupRepository;
            _semesterRepository = semesterRepository;
        }

        public async Task<GroupDTO> Execute(int id)
        {
            var groupDTO = await _groupRepository.GetGroupByIdAsync(id);
            var currentSemester = await _semesterRepository.GetCurrentSemesterAsync(DateTime.Now);

            groupDTO.SemesterId = currentSemester?.Number ?? 0;
            groupDTO.TypeGroup = groupDTO.TypeGroup == "Очно" ? "1" : "2";

            return groupDTO;
        }
    }
}
