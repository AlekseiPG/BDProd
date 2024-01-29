namespace BDProd.Models
{
    public class FancyTreeNode
    {
        public string title { get; set; }
        public bool folder { get; set; }
        public List<FancyTreeNode> children { get; set; }
        public string path { get; set; }
        public string key { get; set; }
    }

}
