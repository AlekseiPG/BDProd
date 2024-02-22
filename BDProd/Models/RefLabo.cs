using System.ComponentModel.DataAnnotations;

namespace BDProd.Models
{
    public class RefLabo
    {
        [Key]
        public int RLAB_ID { get; set; }

        [StringLength(31)]
        public string RLAB_NOM { get; set; }

        [StringLength(255)]
        public string RLAB_ADRESSE1 { get; set; }

        [StringLength(255)]
        public string RLAB_ADRESSE2 { get; set; }

        [StringLength(8)]
        public string RLAB_CPOSTAL { get; set; }

        [StringLength(39)]
        public string RLAB_VILLE { get; set; }

        [StringLength(15)]
        public string RLAB_TEL { get; set; }

        [StringLength(15)]
        public string RLAB_FAX { get; set; }

        [StringLength(80)]
        public string RLAB_URL { get; set; }

        public byte? RLAB_SRC { get; set; }

        public int? RLAB_ID_SRC { get; set; }

        [Required]
        [StringLength(5)]
        public string RLAB_codeWP { get; set; }

        public DateTime? RLAB_DELETED { get; set; }
    }
}
