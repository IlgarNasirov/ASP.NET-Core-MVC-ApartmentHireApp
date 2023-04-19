using System.ComponentModel.DataAnnotations;

namespace ApartmentHireApp.DTOs
{
    public class AddBlockDTO
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
    }
}
