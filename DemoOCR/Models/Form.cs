namespace DemoOCR.Models
{
    public class Form
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ModuleId { get; set; }
        public string FormName { get; set; }
        public string Description { get; set; }

        // Navigation property
        public Module Module { get; set; }
        public ICollection<FormField> FormFields { get; set; } = new List<FormField>();
    }
}
