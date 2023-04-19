using ApartmentHireApp.DTOs;
using ApartmentHireApp.Models;
using System.Drawing;

namespace ApartmentHireApp.Repositories
{
    public class ApartmentRepository:IApartmentRepository
    {
        private readonly ApartmentHireContext _apartmentHireContext;
        public ApartmentRepository(ApartmentHireContext apartmentHireContext)
        {
            _apartmentHireContext = apartmentHireContext;
        }
        public async Task<AddApartmentDTO> GetApartment(int id)
        {
            var apartment = await _apartmentHireContext.Apartments.FindAsync(id);
            if (apartment == null)
            {
                return null;
            }
            var blockName= (await _apartmentHireContext.Blocks.FindAsync(apartment.Blockid)).Name;
            var addApartmentDTO = new AddApartmentDTO { Id=apartment.Id, BlockName=blockName, Blocks=AllBlocks(), Cost=(float)apartment.Cost, No = apartment.No.Split("-")[1], NumberOfRooms=apartment.Numberofrooms, Size=(float)apartment.Size};
            return addApartmentDTO;
        }
        public IQueryable<string> AllBlocks()
        {
            return from b in _apartmentHireContext.Blocks select b.Name;
        }
        public async Task<CustomReturnDTO>AddApartment(AddApartmentDTO addApartmentDTO)
        {
            var id=_apartmentHireContext.Blocks.Where(b=>b.Name== addApartmentDTO.BlockName).FirstOrDefault().Id;
            var apartment = new Apartment { Blockid = id, Cost = (float)addApartmentDTO.Cost, No = addApartmentDTO.BlockName + "-" + addApartmentDTO.No, Numberofrooms = (int)addApartmentDTO.NumberOfRooms, Size = (float)addApartmentDTO.Size };
            await _apartmentHireContext.AddAsync(apartment);
            await _apartmentHireContext.SaveChangesAsync();
            return new CustomReturnDTO { Type = true, Message = "New apartment added successfully!" };
        }
        public async Task<CustomReturnDTO> UpdateApartment(AddApartmentDTO addApartmentDTO)
        {
            var apartment = await _apartmentHireContext.Apartments.FindAsync(addApartmentDTO.Id);
            if (apartment == null)
            {
                return new CustomReturnDTO { Type = false};
            }
            var id=_apartmentHireContext.Blocks.Where(b => b.Name == addApartmentDTO.BlockName).FirstOrDefault().Id;
            apartment.Blockid = id;
            apartment.Cost= (float)addApartmentDTO.Cost;
            apartment.No = addApartmentDTO.BlockName + "-" + addApartmentDTO.No;
            apartment.Numberofrooms = (int)addApartmentDTO.NumberOfRooms;
            apartment.Size = (float)addApartmentDTO.Size;
            await _apartmentHireContext.SaveChangesAsync();
            return new CustomReturnDTO { Type = true, Message = "The apartment updated successfully!" };
        }
        public async Task<CustomReturnDTO>DeleteApartment(int id)
        {
            var apartment = await _apartmentHireContext.Apartments.FindAsync(id);
            if (apartment == null)
            {
                return new CustomReturnDTO { Type = false };
            }
            _apartmentHireContext.Apartments.Remove(apartment);
            await _apartmentHireContext.SaveChangesAsync();
            return new CustomReturnDTO { Type = true, Message = "The apartment deleted successfully!" };
        }
        public IQueryable<AllApartmentsDTO> AllApartments()
        {
            return from b in _apartmentHireContext.Blocks
                   join a in _apartmentHireContext.Apartments
                   on b.Id equals a.Blockid
                   select new AllApartmentsDTO
                   {
                       BlockName=b.Name,
                       Id=a.Id,
                       Cost=(float)a.Cost,
                       No=a.No,
                       NumberOfRooms=a.Numberofrooms,
                       Size = (float)a.Size,
                       Status= (bool)a.Status
                   };
        }
    }
}