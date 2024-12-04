namespace UniCabinet.Core.Models.ViewModel.Common
{
    public class SelectListItemVM
    {
        public string Value { get; set; }
        public string Text { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is SelectListItemVM other)
            {
                return Value == other.Value && Text == other.Text;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Text);
        }
    }

}
