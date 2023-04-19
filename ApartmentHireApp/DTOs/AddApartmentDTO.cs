using System.ComponentModel.DataAnnotations;

namespace ApartmentHireApp.DTOs
{
    public class AddApartmentDTO
    {     
        public int Id { get; set; }
        public IQueryable<string>? Blocks { get; set; }
        [Required]
        public string BlockName { get; set; }
        [Required]
        public string No { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter the correct value.")]
        public int? NumberOfRooms { get; set; }
        [Required]
        [Range(1, float.MaxValue, ErrorMessage = "Please enter the correct value.")]
        public float? Size { get; set; }
        [Required]
        [Range(1, float.MaxValue, ErrorMessage = "Please enter the correct value.")]
        public float? Cost { get; set; }
    }
}
