namespace UniCabinet.Web.ViewModel
{
    public class GroupViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Очно/Заочно 
        /// </summary>
        public string TypeGroup { get; set; }

        /// <summary>
        /// Номер курса
        /// </summary>
        public int CourseNumber { get; set; }

        /// <summary>
        /// Номер семестра
        /// </summary>
        public int SemesterNumber { get; set; }
    }
}
