namespace VendSys.Enum
{
    public abstract class Enumeration
    {
        private readonly int _index;
        private readonly string _value;

        protected Enumeration() { }

        protected Enumeration(string value, int index)
        {
            _value = value;
            _index = index;
        }

        public string Value => _value;
        public int Index => _index;
    }
}
