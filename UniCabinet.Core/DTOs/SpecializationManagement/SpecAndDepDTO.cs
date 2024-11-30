using UniCabinet.Core.DTOs.DepartmentManagmnet;


namespace UniCabinet.Core.DTOs.SpecializationManagement
{
    public class SpecAndDepDTO
    {
        public List<DepartmentDTO> DepartmetVM { get; set; }

        public List<SpecializationDTO> SpecializationVM { get; set; }

        public string UserId { get; set; }

        public string FullName { get; set; }
    }
}
