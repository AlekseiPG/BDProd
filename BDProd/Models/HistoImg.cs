using System.ComponentModel.DataAnnotations;

namespace BDProd.Models
{
    public class HistoImg
    {
        [Key]
        public int id { get; set; }

        [Required]
        public DateTime tm { get; set; }

        public int? opr_id { get; set; }

        [Required]
        public int event_id { get; set; }

        public int? ref_id { get; set; }

        public int? npp { get; set; }

        public short? cnt_img { get; set; }

        public short? cnt_add { get; set; }

        public short? cnt_mod { get; set; }

        public short? cnt_del { get; set; }
    }
}
