namespace ApartmentHireApp.DTOs
{
    public class AllApartmentsDTO
    {
        public int Id { get; set; }
        public string BlockName { get; set; }
        public string No { get; set; }
        public int NumberOfRooms { get; set; }
        public float Size { get; set; }
        public float Cost { get; set; }
        public bool Status { get; set; }
    }
}
