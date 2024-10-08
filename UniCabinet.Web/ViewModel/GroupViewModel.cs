namespace UniCabinet.Web.ViewModel
{
    public class GroupViewModel
    {
        public string Name { get; set; }

        /// <summary>
        /// Очно/Заочно 
        /// </summary>
        public string TypeGroup { get; set; }

        public int CourseNumber { get; set; }

        public int SemesterNumber { get; set; }
    }
}
