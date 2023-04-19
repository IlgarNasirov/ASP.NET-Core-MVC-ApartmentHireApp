using ApartmentHireApp.DTOs;
using ApartmentHireApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentHireApp.Controllers
{
    public class BlockController : Controller
    {
        private readonly IBlockRepository _blockRepository;
        public BlockController(IBlockRepository blockRepository)
        {
            _blockRepository= blockRepository;
        }
        public IActionResult Index()
        {
            return View(_blockRepository.AllBlocks());
        }
        public IActionResult AddBlock()
        {
            ViewData["blockTitle"] = "Add block";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBlock(AddBlockDTO addBlockDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _blockRepository.AddBlock(addBlockDTO);
                if (result.Type == true)
                {
                    TempData["success"] = result.Message;
                    return RedirectToAction("Index", "Block");
                }
            }
            ViewData["blockTitle"] = "Add block";
            return View();
        }
        public async Task<IActionResult> UpdateBlock(int id)
        {
            if (id <= 0)
            {
                return RedirectToAction("Index", "Block");
            }
            var result= await _blockRepository.GetBlock(id);
            if (result == null)
            {
                return RedirectToAction("Index", "Block");
            }
            ViewData["blockTitle"] = "Update block";
            return View("AddBlock", result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateBlock(AddBlockDTO addBlockDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _blockRepository.UpdateBlock(addBlockDTO);
                if (result.Type == true)
                {
                    TempData["success"] = result.Message;
                }
                return RedirectToAction("Index", "Block");
            }
            ViewData["blockTitle"] = "Update block";
            return View("AddBlock");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBlock(int id)
        {
            if (id <= 0)
            {
                return RedirectToAction("Index", "Block");
            }
            var result = await _blockRepository.DeleteBlock(id);
                if(result.Type==true)
                {
                TempData["success"] = result.Message;
                }
            return RedirectToAction("Index", "Block");
        }
    }
}