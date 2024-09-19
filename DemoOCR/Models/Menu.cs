using System.ComponentModel.DataAnnotations;

namespace DemoOCR.Models
{
    public class Menu
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ActionName { get; set; }
        [Required]
        public string ControllerName { get; set; }
        public int? ParentMenuId { get; set; }
        public bool IsVisible { get; set; }
        public string Role { get; set; }

        // Navigation property for child menus
        public ICollection<Menu> SubMenus { get; set; } = new List<Menu>();
    }
}
