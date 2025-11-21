using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bus.Models
{
    public class sales
    {
        [Key]
        public int sid { get; set; }
        public DateTime date { get; set; }
        [ForeignKey("customer")]
        public int cid { get; set; }
        public customer customer { get; set; }
        [ForeignKey("vehicle")]
        public int vid { get; set; }
        public vehicle vehicle { get; set; }
        public decimal onroadprice { get; set; }
        public decimal gst { get; set; }
    }
}
