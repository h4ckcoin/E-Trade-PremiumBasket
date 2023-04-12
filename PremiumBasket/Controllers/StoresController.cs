#nullable disable
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MvcWebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StoresController : Controller
    {
        private readonly IStoreService _storeService;

        public StoresController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        public IActionResult Index()
        {
            List<StoreModel> storeList = _storeService.Query().ToList();
            return View(storeList);
        }

        public IActionResult Details(int id)
        {
            StoreModel store = _storeService.Query().SingleOrDefault(s => s.Id == id);
            if (store == null)
            {
                return View("_Error", "Store not found!");
            }
            return View(store);
        }

        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StoreModel store)
        {
            if (ModelState.IsValid)
            {
                var result = _storeService.Add(store);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }
            return View(store);
        }

        public IActionResult Edit(int id)
        {
            StoreModel store = _storeService.Query().SingleOrDefault(s => s.Id == id);
            if (store == null)
            {
                return View("_Error", "Store not found!");
            }
            return View(store);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StoreModel store)
        {
            if (ModelState.IsValid)
            {
                var result = _storeService.Update(store);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }
            return View(store);
        }

        public IActionResult Delete(int id)
        {
            StoreModel store = _storeService.Query().SingleOrDefault(s => s.Id == id);
            if (store == null)
            {
                return View("_Error", "Store not found!");
            }
            return View(store);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _storeService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
	}
}
