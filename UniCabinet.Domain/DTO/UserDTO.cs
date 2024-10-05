// UniCabinet.Application/DTOs/UserDTO.cs
using System.Collections.Generic;

namespace UniCabinet.Domain.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public IList<string> Roles { get; set; }
        public List<GroupDTO> Groups { get; set; }
    }
}
