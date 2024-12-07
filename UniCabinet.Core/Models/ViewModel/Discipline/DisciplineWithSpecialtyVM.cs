namespace UniCabinet.Core.Models.ViewModel.Discipline
{
    public class DisciplineWithSpecialtyVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SpecialtyId { get; set; } // ID специальности
        public string SpecialtyName { get; set; } // Название специальности
        public string Description { get; set; }
    }
}
