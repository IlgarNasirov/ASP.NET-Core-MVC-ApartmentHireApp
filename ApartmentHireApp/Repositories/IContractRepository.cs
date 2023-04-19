using ApartmentHireApp.DTOs;

namespace ApartmentHireApp.Repositories
{
    public interface IContractRepository
    {
        public IQueryable<string> AllNos();
        public Task<CustomReturnDTO> AddContract(AddContractDTO addContractDTO);
        public Task<AddContractDTO> GetContract(int id);
        public Task<CustomReturnDTO> UpdateContract(AddContractDTO addContractDTO);
        public Task<CustomReturnDTO> DeleteContract(int id);
        public IQueryable<AllContractsDTO> AllContracts();
        public Task<CustomReturnDTO> CheckContracts();
    }
}
