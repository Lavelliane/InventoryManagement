using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementWeb.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
