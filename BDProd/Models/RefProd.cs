using System.ComponentModel.DataAnnotations;

namespace BDProd.Models
{
    public class RefProd
    {
        [Key]
        public int REF_ID { get; set; }

        [StringLength(13)]
        public string REF_CODE13 { get; set; }

        public int? REF_LABIDMAJ { get; set; }

        [StringLength(41)]
        public string REF_NOM { get; set; }

        public int? REF_ID_REMPLPAR { get; set; }

        public bool? REF_SORTIE { get; set; }

        public bool? REF_MAJ { get; set; }
    }
}
