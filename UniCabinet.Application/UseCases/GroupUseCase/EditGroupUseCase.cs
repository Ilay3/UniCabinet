using UniCabinet.Application.Interfaces.Repository;
using System;
using UniCabinet.Core.DTOs.CourseManagement;

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

        public async Task<bool> ExecuteAsync(GroupDTO groupDTO)
        {


            await _groupRepository.UpdateGroupAsync(groupDTO);
            return true;
        }
    }
}
