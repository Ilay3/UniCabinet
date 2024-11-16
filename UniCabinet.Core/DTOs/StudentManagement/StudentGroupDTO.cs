using UniCabinet.Core.DTOs.Common;
using UniCabinet.Core.DTOs.UserManagement;
using UniCabinet.Core.Models;

namespace UniCabinet.Core.DTOs.StudentManagement
{
    public class StudentGroupDTO
    {
        public List<UserDTO> Users { get; set; }
        public List<SelectListItemDTO> Groups { get; set; }
        public PaginationModel Pagination { get; set; }
    }
}
