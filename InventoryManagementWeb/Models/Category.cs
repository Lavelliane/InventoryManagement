using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace InventoryManagementWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public float price { get; set; }

        [Required]
        public string imgURL { get; set; }
        public string ImageName { get; set; }

        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        
    }
}
