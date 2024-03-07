using System.ComponentModel.DataAnnotations;

namespace BDProd.Models
{
    public class PGroup
    {
        [Key]
        public int REF_ID1 { get; set; }

        [Key]
        public int REF_ID2 { get; set; }

        public DateTime? TMCREATE { get; set; }

        public int? OP_ID { get; set; }
    }
}
