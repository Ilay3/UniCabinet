namespace UniCabinet.Core.Models.ViewModel.DisciplineDetail
{
    public class DisciplineDetailsModalVM
    {
        public List<DisciplineDetailVM> Details { get; set; }
        public FilterOptionsVM FilterOptions { get; set; }
        public string TeacherId { get; set; }
        public int DisciplineId { get; set; }
        public bool IsTeacherView { get; set; }
    }

}
