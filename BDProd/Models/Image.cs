namespace BDProd.Models
{
    public class Image
    {
        public int Id { get; set; }
        public short NPO { get; set; }
        public string? ImagePath { get; set; }
        public string? ImageName { get; set; }
        public string? RefProd { get; set; }
        public DateTime? TempsModification { get; set; }
        public DateTime? TempsCreation { get; set; }
        public bool? EnCours { get; set; }
        public bool? Supprime { get; set; }
    }
}
