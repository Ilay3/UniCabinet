using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCabinet.Application.Interfaces;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Application.UseCases.DepartmentUseCase;
using UniCabinet.Core.DTOs.SpecializationManagement;
using UniCabinet.Core.DTOs.UserManagement;

namespace UniCabinet.Application.UseCases.AdminUseCase
{
    public class SpecAndDepUseCase
    {
        private readonly ISpecializationRepository _specificationRepository;
        private readonly GetDepartmnetDataUseCase _departmentUseCase;
        private readonly IUserService _userService;

        public SpecAndDepUseCase(
            ISpecializationRepository specializationRepository,
            GetDepartmnetDataUseCase getDepartmnetDataUseCase,
            IUserService userService
        )
        {
            _departmentUseCase = getDepartmnetDataUseCase;
            _specificationRepository = specializationRepository;
            _userService = userService;
        }

        public async Task<SpecAndDepDTO> ExecuteAsync(string UserId)
        {
            var specializationn = await _specificationRepository.GetAllSpecialization();

            var departmnet = await _departmentUseCase.ExecutreAsync();

            var user = await _userService.GetUserByIdAsync(UserId);

            var rezult = new SpecAndDepDTO
            {
                SpecializationVM = specializationn,
                DepartmetVM = departmnet,
                UserId = user.Id,
                FullName = $"{user.LastName} {user.FirstName} {user.Patronymic}"
            };

            return rezult;
        }
    }

    
}
