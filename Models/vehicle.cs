using System.ComponentModel.DataAnnotations;

namespace bus.Models
{
    public class vehicle
    {
        [Key]
        public int id { get; set; }
        public string manufacturer { get; set; }
        public string model { get; set; }
        public decimal xshowroomprice { get; set; }
    }
}
