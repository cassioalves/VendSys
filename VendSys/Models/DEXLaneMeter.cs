using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VendSys.Models
{
    public class DEXLaneMeter
    {
        [Key]
        public int Id { get; set; }

        public int DEXMeterId { get; set; }

        [ForeignKey("DEXMeterId")]
        public DEXMeter? DEXMeter { get; set; }

        public string ProductIdentifier { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int NumberOfVends { get; set; }

        public decimal ValueOfPaidSalves { get; set; }
    }
}
