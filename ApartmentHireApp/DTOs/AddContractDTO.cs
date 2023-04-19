using System.ComponentModel.DataAnnotations;

namespace ApartmentHireApp.DTOs
{
    public class AddContractDTO
    {
        public int Id { get; set; }
        public IQueryable<string>? Nos { get; set; }
        [Required]
        public string NoName { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? EndDate { get; set; }
        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(3)]
        public string LastName { get; set; }
        [Required]
        [Range(1, float.MaxValue, ErrorMessage = "Please enter the correct value.")]
        public float? Cost { get; set; }
    }
}