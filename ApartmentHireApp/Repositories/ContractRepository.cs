using ApartmentHireApp.DTOs;
using ApartmentHireApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ApartmentHireApp.Repositories
{
    public class ContractRepository:IContractRepository
    {
        private readonly ApartmentHireContext _apartmentHireContext;
        public ContractRepository(ApartmentHireContext apartmentHireContext)
        {
            _apartmentHireContext = apartmentHireContext;
        }
        public IQueryable<string> AllNos()
        {
            return from a in _apartmentHireContext.Apartments where a.Status==true select a.No;
        }
        public async Task<CustomReturnDTO> AddContract(AddContractDTO addContractDTO)
        {
            var apartment = await _apartmentHireContext.Apartments.Where(a=>a.No==addContractDTO.NoName).FirstOrDefaultAsync();
            var id = apartment.Id;
            apartment.Status = false;
            var contract = new Contract { Apartmentid = id, Cost = (float)addContractDTO.Cost, Enddate = (DateTime)addContractDTO.EndDate, Firstname = addContractDTO.FirstName, Lastname = addContractDTO.LastName, Startdate = (DateTime)addContractDTO.StartDate };
            await _apartmentHireContext.Contracts.AddAsync(contract);
            await _apartmentHireContext.SaveChangesAsync();
            return new CustomReturnDTO { Type = true, Message = "New contract added successfully!" };
        }
        public async Task<AddContractDTO>GetContract(int id)
        {
            var contract = await _apartmentHireContext.Contracts.FindAsync(id);
            if (contract == null)
            {
                return null;
            }
            var noName = (await _apartmentHireContext.Apartments.FindAsync(contract.Apartmentid)).No;
            var addContractDTO = new AddContractDTO { Cost=(float)contract.Cost, EndDate=contract.Enddate, FirstName=contract.Firstname, Id=contract.Id, LastName=contract.Lastname, NoName=noName, Nos=AllNos(), StartDate=contract.Startdate };
            return addContractDTO;
        }
        public async Task<CustomReturnDTO> UpdateContract(AddContractDTO addContractDTO)
        {
            var contract = await _apartmentHireContext.Contracts.FindAsync(addContractDTO.Id);
            if (contract == null)
            {
                return new CustomReturnDTO { Type = false };
            }
            var id = _apartmentHireContext.Apartments.Where(a => a.No == addContractDTO.NoName).FirstOrDefault().Id;
            if (contract.Apartmentid != id)
            {
                var apartment = await _apartmentHireContext.Apartments.FindAsync(contract.Apartmentid);
                apartment.Status = true;
                var newApartment = await _apartmentHireContext.Apartments.FindAsync(id);
                newApartment.Status = false;
                contract.Apartmentid = id;
            }
            contract.Startdate = (DateTime)addContractDTO.StartDate;
            contract.Enddate = (DateTime)addContractDTO.EndDate;
            contract.Cost = (float)addContractDTO.Cost;
            contract.Firstname = addContractDTO.FirstName;
            contract.Lastname = addContractDTO.LastName;
            await _apartmentHireContext.SaveChangesAsync();
            return new CustomReturnDTO { Type = true, Message = "The contract updated successfully!" };
        }
        public async Task<CustomReturnDTO> DeleteContract(int id)
        {
            var contract = _apartmentHireContext.Contracts.Find(id);
            if (contract == null)
            {
                return new CustomReturnDTO { Type = false };
            }
            var apartment = _apartmentHireContext.Apartments.Where(a => a.Id == contract.Apartmentid).FirstOrDefault();
            _apartmentHireContext.Contracts.Remove(contract);
            if (apartment != null)
            {
                apartment.Status = true;
            }
            _apartmentHireContext.SaveChanges();
            return new CustomReturnDTO { Type = true, Message = "The contract deleted successfully!" };
        }
        public IQueryable<AllContractsDTO> AllContracts()
        {
            return from a in _apartmentHireContext.Apartments
                   join c in _apartmentHireContext.Contracts
                   on a.Id equals c.Apartmentid
                   select new AllContractsDTO
                   {
                       Cost=(float)c.Cost,
                       EndDate=c.Enddate,
                       FirstName=c.Firstname,
                       LastName=c.Lastname,
                       Id=c.Id,
                       StartDate=c.Startdate,
                       NoName=a.No
                   };
        }
        public async Task<CustomReturnDTO> CheckContracts()
        {
            var query = _apartmentHireContext.Contracts;
            List<int> Ids = new List<int>();
            foreach (var item in query)
            {
                if ((DateTime.Now - item.Enddate).Days > 0)
                {
                    Ids.Add(item.Id);
                }
            }
            if(Ids.Count > 0)
            {
                foreach (var id in Ids)
                {
                    await DeleteContract(id);
                }
            }
            return new CustomReturnDTO { Type=true, Message="Success!" };
        }
    }
}
