using ApartmentHireApp.DTOs;
using ApartmentHireApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentHireApp.Controllers
{
    public class ContractController : Controller
    {
        private readonly IContractRepository _contractRepository;
        public ContractController(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }
        [HttpGet]
        public async Task<IActionResult> CheckContracts()
        {
            var result=await _contractRepository.CheckContracts();
            if (result.Type == true)
            {
                return Ok(new { message = result.Message });
            }
            return BadRequest(new {message="Error!"});
        }
        public IActionResult Contracts()
        {
            return View("Index", _contractRepository.AllContracts());
        }
        public IActionResult AddContract()
        {
            ViewData["contractTitle"] = "Add contract";
            var result = _contractRepository.AllNos();
            return View(new AddContractDTO { Nos = result });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddContract(AddContractDTO addContractDTO)
        {
            if (addContractDTO.StartDate < new DateTime(1850, 1, 1) || addContractDTO.StartDate> new DateTime(9999, 12, 31))
            {
                ModelState.AddModelError("dateError", "Please enter the correct start date.");
            }
            if (addContractDTO.EndDate > new DateTime(9999, 12, 31)|| addContractDTO.EndDate<new DateTime(1850, 1, 1))
            {
                ModelState.AddModelError("dateError", "Please enter the correct end date.");
            }
            if (addContractDTO.StartDate > addContractDTO.EndDate)
            {
                ModelState.AddModelError("dateError", "The end date must be greater than the start date.");
            }
            if (ModelState.IsValid)
            {
                var result = await _contractRepository.AddContract(addContractDTO);
                if (result.Type == true)
                {
                    TempData["success"] = result.Message;
                    return RedirectToAction("Contracts", "Contract");
                }
            }
            ViewData["contractTitle"] = "Add contract";
            var nos = _contractRepository.AllNos();
            return View(new AddContractDTO { Nos = nos });
        }
        public async Task<IActionResult> UpdateContract(int id)
        {
            if (id <= 0)
            {
                return RedirectToAction("Contracts", "Contract");
            }
            var result = await _contractRepository.GetContract(id);
            if (result == null)
            {
                return RedirectToAction("Contracts", "Contract");
            }
            ViewData["contractTitle"] = "Update contract";
            return View("AddContract", result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateContract(AddContractDTO addContractDTO)
        {
            if (addContractDTO.StartDate > addContractDTO.EndDate)
            {
                ModelState.AddModelError("dateError", "The end date must be greater than the start date.");
            }
            if (ModelState.IsValid)
            {
                var result = await _contractRepository.UpdateContract(addContractDTO);
                if (result.Type == true)
                {
                    TempData["success"] = result.Message;
                }
                return RedirectToAction("Contracts", "Contract");
            }
            ViewData["apartmentTitle"] = "Update apartment";
            return View("AddContract");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteContract(int id)
        {
            if (id <= 0)
            {
                return RedirectToAction("Contracts", "Contract");
            }
            var result = await _contractRepository.DeleteContract(id);
            if (result.Type == true)
            {
                TempData["success"] = result.Message;
            }
            return RedirectToAction("Contracts", "Contract");
        }
    }
}
