using ApartmentHireApp.DTOs;
using ApartmentHireApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ApartmentHireApp.Repositories
{
    public class BlockRepository:IBlockRepository
    {
        private readonly ApartmentHireContext _apartmentHireContext;
        public BlockRepository(ApartmentHireContext apartmentHireContext)
        {
            _apartmentHireContext= apartmentHireContext;
        }
        public async Task<AddBlockDTO>GetBlock(int id)
        {
            var block = await _apartmentHireContext.Blocks.FindAsync(id);
            if (block == null)
            {
                return null;
            }
            var addBlockDTO=new AddBlockDTO { Id=block.Id, Name=block.Name };
            return addBlockDTO;
        }
        public IQueryable<AllBlocksDTO>AllBlocks()
        {
            return from b in _apartmentHireContext.Blocks
                          select new AllBlocksDTO
                          {
                              Id = b.Id,
                              Name = b.Name
                          };
        }
        public async Task<CustomReturnDTO>AddBlock(AddBlockDTO addBlockDTO)
        {
            var block = new Block { Name=addBlockDTO.Name};
            await _apartmentHireContext.Blocks.AddAsync(block);
            await _apartmentHireContext.SaveChangesAsync();
            return new CustomReturnDTO { Type = true, Message = "New block added successfully!" };
        }
        public async Task<CustomReturnDTO>UpdateBlock(AddBlockDTO addBlockDTO)
        {
            var block = await _apartmentHireContext.Blocks.FindAsync(addBlockDTO.Id);
            if (block == null)
            {
                return new CustomReturnDTO { Type = false};
            }
            block.Name = addBlockDTO.Name;
            await _apartmentHireContext.SaveChangesAsync();
            return new CustomReturnDTO { Type = true, Message = "The block updated successfully!" };
        }
        public async Task<CustomReturnDTO>DeleteBlock(int id)
        {
            var block = await _apartmentHireContext.Blocks.FindAsync(id);
            if (block == null)
            {
                return new CustomReturnDTO { Type = false};
            }
            _apartmentHireContext.Blocks.Remove(block);
            await _apartmentHireContext.SaveChangesAsync();
            return new CustomReturnDTO { Type = true, Message = "The block deleted successfully!" };
        }
    }
}
