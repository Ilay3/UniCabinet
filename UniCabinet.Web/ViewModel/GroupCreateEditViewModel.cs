namespace UniCabinet.Web.ViewModel
{
    public class GroupCreateEditViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Очно/Заочно 
        /// </summary>
        public string TypeGroup { get; set; }

        public int CourseId { get; set; }

        public string CurrentSemester { get; set; }

    }
}
