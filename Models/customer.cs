using System.ComponentModel.DataAnnotations;

namespace bus.Models
{
    public class customer
    {
        [Key]
        public int cid { get; set; }
        public string customername { get; set; }
        public string city { get; set; }
    }
}
