using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace UniCabinet.Web.ViewModel.User
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime? DateBirthday { get; set; }
        public List<string> Roles { get; set; }
        public string GroupName { get; set; }
        public int? GroupId { get; set; }

        // Добавляем свойство FullName
        public string FullName => $"{FirstName} {LastName} {Patronymic}".Trim();
    }



}
