using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCabinet.Core.DTOs.SpecializationManagement;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface ISpecializationRepository
    {
        Task<List<SpecializationDTO>> GetAllSpecialization();
    }
}
