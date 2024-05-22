using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToyStore.Application.Interfaces;
using ToyStore.Domain.Entities;
using ToyStore.Mvc.Models;

[Authorize]
public class ToyController : Controller
{
    private readonly IToyService _toyService;
    private readonly IStoreService _storeService;

    public ToyController(IToyService toyService, IStoreService storeService)
    {
        _toyService = toyService;
        _storeService = storeService;
    }

    public async Task<IActionResult> Index()
    {
        var toys = await _toyService.GetAllToysWithStoresAsync(); 

        var model = toys.Select(x => new ToyViewModel
        {
            Name = x.Name,
            Type = x.Type,
            StoreId = x.StoreId,
            Id = x.Id,
            Price = x.Price,
            Store = new StoreViewModel
            {
                Id = x.Store?.Id ?? 0,
                Name = x.Store?.Name ?? " - "
            }
        }).ToList();

        return View(model);
    }

    public async Task<IActionResult> Create()
    {
        var model = new ToyViewModel();
        ViewBag.Stores = await _storeService.GetAllStoresAsync();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ToyViewModel model)
    {
        if (ModelState.IsValid)
        {
            var toy = new Toy()
            {
                Id = model.Id,
                Name = model.Name,
                Type = model.Type,
                Price = model.Price,
                StoreId = model.StoreId,
            };

            await _toyService.AddToyAsync(toy);
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Stores = await _storeService.GetAllStoresAsync();
        return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var toy = await _toyService.GetToyDetailsAsync(id); 
            return View(toy);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(); 
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var toy = await _toyService.GetToyDetailsAsync(id);

        if (toy == null)
        {
            return NotFound();
        }

        var model = new ToyViewModel()
        {
            Id = toy.Id,
            Name = toy.Name,
            Type = toy.Type,
            Price = toy.Price,
            StoreId = toy.StoreId
        };

        ViewBag.Stores = await _storeService.GetAllStoresAsync();

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ToyViewModel model)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        var toy = new Toy()
        {
            Id = model.Id,
            Name = model.Name,
            Type = model.Type,
            Price = model.Price,
            StoreId = model.StoreId,
        };

        if (ModelState.IsValid)
        {
            await _toyService.UpdateToyAsync(toy);
            return RedirectToAction(nameof(Index));
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var toy = await _toyService.GetToyDetailsAsync(id);
        if (toy == null)
        {
            return NotFound();
        }
        return View(toy);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var toy = await _toyService.GetToyDetailsAsync(id);
        if (toy == null)
        {
            return NotFound();
        }

        await _toyService.DeleteToyAsync(id);
        
        return RedirectToAction(nameof(Index));
    }
}
