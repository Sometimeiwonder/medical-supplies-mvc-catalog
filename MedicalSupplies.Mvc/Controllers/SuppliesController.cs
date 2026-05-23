using MedicalSupplies.Mvc.Models;
using MedicalSupplies.Mvc.Services;
using MedicalSupplies.Mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MedicalSupplies.Mvc.Controllers;

public class SuppliesController : Controller
{
    private readonly MedicalSupplyService _service;
    public SuppliesController(MedicalSupplyService service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        var supplies = _service.GetAll()
            .Select(ToListItemViewModel)
            .ToList();
        return View(supplies);
    }

    public IActionResult Detail(int id)
    {
        var supply = _service.GetById(id);
        if (supply == null)
            return NotFound($"Không tìm thấy vật tư có Id = {id}");
        var vm = ToDetailViewModel(supply);
        return View(vm);
    }

    public IActionResult Stats()
    {
        var stats = _service.GetStats();
        return View(stats);
    }

    public IActionResult Welcome()
    {
        return Content("Welcome to Medical Supplies Catalog MVC!");
    }

    public IActionResult SupplyJson()
    {
        var supplies = _service.GetAll()
            .Select(s => new { s.Id, s.Code, s.Name, s.Category, s.UnitPrice, s.Quantity });
        return Json(supplies);
    }

    public IActionResult GoToList()
    {
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Force404()
    {
        return NotFound("Đây là response 404 demo từ action Force404.");
    }

    // Chuyển đổi sang ViewModel:
    private static SupplyListItemViewModel ToListItemViewModel(MedicalSupply s) => 
        new()
        {
            Id = s.Id, Code = s.Code, Name = s.Name, Category = s.Category,
            Supplier = s.Supplier, UnitPrice = s.UnitPrice,
            Quantity = s.Quantity, MinStock = s.MinStock
        };

    private static SupplyDetailViewModel ToDetailViewModel(MedicalSupply s) => 
        new()
        {
            Id = s.Id, Code = s.Code, Name = s.Name, Category = s.Category,
            Supplier = s.Supplier, UnitPrice = s.UnitPrice,
            Quantity = s.Quantity, MinStock = s.MinStock, LastUpdatedAt = s.LastUpdatedAt
        };
}