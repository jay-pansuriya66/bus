using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bus.Models
{
    [Table("sales")]
    public class salesDto
    {
        [Key]
        public int sid { get; set; }
        public DateTime date { get; set; }
        [ForeignKey("customer")]
        public int cid { get; set; }
        [ForeignKey("vehicle")]
        public int vid { get; set; }
        public decimal onroadprice { get; set; }
        public decimal gst { get; set; }
    }
}
