namespace DemoOCR.Models
{
    public class Module
    {
        public int Id { get; set; }
        public string ModuleName { get; set; }
        public string Description { get; set; }
        public ICollection<Form> Forms { get; set; } = new List<Form>();
    }
}
