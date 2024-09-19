namespace DemoOCR.Models
{
    public class FormField
    {
        public int Id { get; set; }
        public int FormId { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; } // TextBox, Dropdown, Checkbox
        public string Label { get; set; }
        public bool IsRequired { get; set; }

        // Navigation property
        public Form Form { get; set; }
    }
}
