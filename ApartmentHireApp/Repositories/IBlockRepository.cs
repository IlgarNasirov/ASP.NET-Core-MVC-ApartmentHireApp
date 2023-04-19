using ApartmentHireApp.DTOs;

namespace ApartmentHireApp.Repositories
{
    public interface IBlockRepository
    {
        public Task<AddBlockDTO> GetBlock(int id);
        public IQueryable<AllBlocksDTO> AllBlocks();
        public Task<CustomReturnDTO> AddBlock(AddBlockDTO addBlockDTO);
        public Task<CustomReturnDTO> UpdateBlock(AddBlockDTO addBlockDTO);
        public Task<CustomReturnDTO> DeleteBlock(int id);
    }
}