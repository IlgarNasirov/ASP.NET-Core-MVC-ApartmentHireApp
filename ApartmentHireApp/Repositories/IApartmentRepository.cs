using ApartmentHireApp.DTOs;

namespace ApartmentHireApp.Repositories
{
    public interface IApartmentRepository
    {
        public Task<AddApartmentDTO> GetApartment(int id);
        public IQueryable<string> AllBlocks();
        public Task<CustomReturnDTO> AddApartment(AddApartmentDTO addApartmentDTO);
        public Task<CustomReturnDTO> UpdateApartment(AddApartmentDTO addApartmentDTO);
        public Task<CustomReturnDTO> DeleteApartment(int id);
        public IQueryable<AllApartmentsDTO> AllApartments();
    }
}
