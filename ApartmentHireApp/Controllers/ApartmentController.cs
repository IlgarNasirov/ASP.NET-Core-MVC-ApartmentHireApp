using ApartmentHireApp.DTOs;
using ApartmentHireApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentHireApp.Controllers
{
    public class ApartmentController : Controller
    {
        private readonly IApartmentRepository _apartmentRepository;
        public ApartmentController(IApartmentRepository apartmentRepository)
        {
            _apartmentRepository = apartmentRepository;
        }
        public IActionResult Apartments()
        {
            return View("Index", _apartmentRepository.AllApartments());
        }
        public IActionResult AddApartment()
        {
            ViewData["apartmentTitle"] = "Add apartment";
            var result = _apartmentRepository.AllBlocks();
            return View(new AddApartmentDTO { Blocks = result });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddApartment(AddApartmentDTO addApartmentDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _apartmentRepository.AddApartment(addApartmentDTO);
                if (result.Type == true)
                {
                    TempData["success"] = result.Message;
                    return RedirectToAction("Apartments", "Apartment");
                }
            }
            ViewData["apartmentTitle"] = "Add apartment";
            var blocks = _apartmentRepository.AllBlocks();
            return View(new AddApartmentDTO { Blocks = blocks });
        }
        public async Task<IActionResult> UpdateApartment(int id)
        {
            if (id <= 0)
            {
                return RedirectToAction("Apartments", "Apartment");
            }
            var result = await _apartmentRepository.GetApartment(id);
            if (result == null)
            {
                return RedirectToAction("Apartments", "Apartment");
            }
            ViewData["apartmentTitle"] = "Update apartment";
            return View("AddApartment", result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateApartment(AddApartmentDTO addApartmentDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _apartmentRepository.UpdateApartment(addApartmentDTO);
                if (result.Type == true)
                {
                    TempData["success"] = result.Message;
                }
                return RedirectToAction("Apartments", "Apartment");
            }
            ViewData["apartmentTitle"] = "Update apartment";
            return View("AddApartment");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteApartment(int id)
        {
            if (id <= 0)
            {
                return RedirectToAction("Apartments", "Apartment");
            }
            var result = await _apartmentRepository.DeleteApartment(id);
            if (result.Type == true)
            {
                TempData["success"] = result.Message;
            }
            return RedirectToAction("Apartments", "Apartment");
        }
    }
}