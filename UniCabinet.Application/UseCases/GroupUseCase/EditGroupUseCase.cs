using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.Entites;
using System;

namespace UniCabinet.Application.UseCases.GroupUseCase
{
    public class EditGroupUseCase
    {
        private readonly IGroupRepository _groupRepository;
        private readonly ISemesterRepository _semesterRepository;

        public EditGroupUseCase(IGroupRepository groupRepository, ISemesterRepository semesterRepository)
        {
            _groupRepository = groupRepository;
            _semesterRepository = semesterRepository;
        }

        public async Task<bool> Execute(GroupDTO groupDTO)
        {
            var currentSemester = _semesterRepository.GetCurrentSemesterAsync(DateTime.Now);
            if (currentSemester == null)
                throw new InvalidOperationException("Текущий семестр не определён.");

            groupDTO.SemesterId = currentSemester.Id;
            await _groupRepository.UpdateGroupAsync(groupDTO);
            return true;
        }
    }
}
