namespace ApartmentHireApp.DTOs
{
    public class AllContractsDTO
    {
        public int Id { get; set; }
        public string NoName { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public float Cost { get; set; }
    }
}
