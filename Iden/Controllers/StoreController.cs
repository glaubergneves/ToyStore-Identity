using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToyStore.Application.Interfaces;
using ToyStore.Domain.Entities;
using ToyStore.Infra;

[Authorize]
public class StoreController : Controller
{
    private readonly IStoreService _service;

    public StoreController(IStoreService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var stores = await _service.GetAllStoresAsync();
        return View(stores);
    }

    public async Task<IActionResult> Details(int id)
    {
        var store = await _service.GetStoreById(id);
        if (store == null)
        {
            return NotFound();
        }
        return View(store);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Store model)
    {
        if (ModelState.IsValid)
        {
            var store = new Store()
            {
                Id = model.Id,
                Name = model.Name
            };

            await _service.AddStoreAsync(store);
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var store = await _service.GetStoreById(id);
        if (store == null)
        {
            return NotFound();
        }
        return View(store);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Store store)
    {
        if (id != store.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            await _service.UpdateStoreAsync(store);
            return RedirectToAction(nameof(Index));
        }
        return View(store);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var store = await _service.GetStoreById(id);
        if (store == null)
        {
            return NotFound();
        }
        return View(store);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var store = await _service.GetStoreById(id);
        if (store == null)
        {
            return NotFound();
        }

        await _service.DeleteStoreAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
