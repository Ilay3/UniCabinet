using System;
using System.ComponentModel.DataAnnotations;

namespace UniCabinet.Core.Models.ViewModel.Practical
{
    public class PracticalEditVM
    {
        public int Id { get; set; }
        public int DisciplineDetailId { get; set; }

        [Required]
        [Display(Name = "Номер практической работы")]
        public int PracticalNumber { get; set; }

        [Required]
        [Display(Name = "Дата проведения")]
        public DateTime Date { get; set; }
    }
}
