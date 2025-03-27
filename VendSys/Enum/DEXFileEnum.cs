namespace VendSys.Enum
{
    public class DEXFileEnum : Enumeration
    {
        public static readonly DEXFileEnum SERIAL_NUMBER = new DEXFileEnum("ID1", 1);
        public static readonly DEXFileEnum VALUE_OF_PAID_VENDS = new DEXFileEnum("VA1", 1);
        public static readonly DEXFileEnum PRODUCT_IDENTIFIER = new DEXFileEnum("PA1", 1);
        public static readonly DEXFileEnum PRICE = new DEXFileEnum("PA1", 2);
        public static readonly DEXFileEnum NUMBER_OF_VENDS = new DEXFileEnum("PA2", 1);
        public static readonly DEXFileEnum VALUE_OF_PAID_SALES = new DEXFileEnum("PA2", 2);
        public DEXFileEnum() { }

        private DEXFileEnum(string value, int index) : base(value, index) { }
    }
}
