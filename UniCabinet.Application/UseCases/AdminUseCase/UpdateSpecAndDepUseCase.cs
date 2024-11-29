using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCabinet.Application.Interfaces;
using UniCabinet.Core.DTOs.SpecializationManagement;
using UniCabinet.Core.DTOs.UserManagement;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.UseCases.AdminUseCase
{
    public class UpdateSpecAndDepUseCase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UpdateSpecAndDepUseCase> _logger;

        public UpdateSpecAndDepUseCase(IUserService userService, ILogger<UpdateSpecAndDepUseCase> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task ExecuteAsync(string UserId, int DepartmentId, int SpecialytyId)
        {
            var user = await _userService.GetUserByIdAsync(UserId);

            user.DepartmentId = DepartmentId;
            user.SpecializationId = SpecialytyId;

            await _userService.UpdateUserSpecAndDepAsync(user);
        }
    }
}
