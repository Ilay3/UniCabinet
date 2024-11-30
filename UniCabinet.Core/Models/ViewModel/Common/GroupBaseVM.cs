namespace UniCabinet.Core.Models.ViewModel.Common
{
    public class GroupBaseVM
    {

        public string Name { get; set; }

        /// <summary>
        /// Очно/Заочно 
        /// </summary>
        public string TypeGroup { get; set; }

        /// <summary>
        /// Номер курса
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// Номер семестра
        /// </summary>
        public int SemesterId { get; set; }
    }
}
