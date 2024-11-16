using UniCabinet.Application.Interfaces.Repository;
using System;
using UniCabinet.Core.DTOs.CourseManagement;

namespace UniCabinet.Application.UseCases.GroupUseCase
{
    public class AddGroupUseCase
    {
        private readonly IGroupRepository _groupRepository;
        private readonly ISemesterRepository _semesterRepository;

        public AddGroupUseCase(IGroupRepository groupRepository, ISemesterRepository semesterRepository)
        {
            _groupRepository = groupRepository;
            _semesterRepository = semesterRepository;
        }

        public async Task<bool> Execute(GroupDTO groupDTO)
        {
            var currentSemester = await _semesterRepository.GetCurrentSemesterAsync(DateTime.Now);
            if (currentSemester == null)
                throw new InvalidOperationException("Текущий семестр не определён.");

            groupDTO.SemesterId = currentSemester.Id;
            await _groupRepository.AddGroupAsync(groupDTO);
            return true;
        }
    }
}
