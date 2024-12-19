using System;
using System.ComponentModel.DataAnnotations;

namespace UniCabinet.Core.Models.ViewModel.Practical
{
    public class PracticalAddVM
    {
        public int DisciplineDetailId { get; set; }

        [Required]
        [Display(Name = "Название практической работы")]
        public string PracticalName { get; set; }

        [Required]
        [Display(Name = "Дата проведения")]
        public DateTime Date { get; set; }
    }
}
