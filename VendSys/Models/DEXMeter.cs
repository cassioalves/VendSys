using System.ComponentModel.DataAnnotations;

namespace VendSys.Models
{
    public class DEXMeter
    {
        [Key]
        public int Id { get; set; }

        public string Machine { get; set; } = string.Empty;

        public DateTime DEXDateTime { get; set; }

        public string MachineSerialNumber { get; set; } = string.Empty;

        public decimal ValueOfPaidVends { get; set; }

        public List<DEXLaneMeter> LaneMeters { get; set; } = new();
    }
}