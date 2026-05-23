using MedicalSupplies.Mvc.Models;
using MedicalSupplies.Mvc.ViewModels;

namespace MedicalSupplies.Mvc.Services;

public class MedicalSupplyService
{
    private readonly List<MedicalSupply> _supplies = new()
    {
        new MedicalSupply
        {
            Id = 1, Code = "MS-MSK-001", Name = "Khẩu trang y tế",
            Category = "Bảo hộ", Supplier = "VinMed",
            UnitPrice = 1200, Quantity = 500, MinStock = 200, LastUpdatedAt = new DateTime(2025,5,15,8,30,0)
        },
        new MedicalSupply
        {
            Id = 2, Code = "MS-GLO-002", Name = "Găng tay cao su",
            Category = "Bảo hộ", Supplier = "VietGlove",
            UnitPrice = 3400, Quantity = 180, MinStock = 200, LastUpdatedAt = new DateTime(2025,5,15,9,0,0)
        },
        new MedicalSupply
        {
            Id = 3, Code = "MS-THE-003", Name = "Nhiệt kế hồng ngoại",
            Category = "Thiết bị kiểm tra", Supplier = "Omron Vietnam",
            UnitPrice = 320000, Quantity = 8, MinStock = 10, LastUpdatedAt = new DateTime(2025,5,15,10,0,0)
        },
        new MedicalSupply
        {
            Id = 4, Code = "MS-BAN-004", Name = "Bông y tế",
            Category = "Tiêu hao", Supplier = "Medicare",
            UnitPrice = 28000, Quantity = 0, MinStock = 15, LastUpdatedAt = new DateTime(2025,5,15,9,30,0)
        },
        new MedicalSupply
        {
            Id = 5, Code = "MS-SYR-005", Name = "Bơm tiêm 5ml",
            Category = "Tiêu hao", Supplier = "Kim Tiêm Sài Gòn",
            UnitPrice = 1500, Quantity = 220, MinStock = 100, LastUpdatedAt = new DateTime(2025,5,15,8,45,0)
        }
    };

    public List<MedicalSupply> GetAll() => _supplies;
    public MedicalSupply? GetById(int id) => _supplies.FirstOrDefault(x => x.Id == id);

    public SupplyStatsViewModel GetStats()
    {
        var totalSupplies = _supplies.Count;
        var totalQuantity = _supplies.Sum(s => s.Quantity);
        var totalInventoryValue = _supplies.Sum(s => s.UnitPrice * s.Quantity);
        var outOfStock = _supplies.Count(s => s.Quantity <= 0);
        var needReorder = _supplies.Count(s => s.Quantity > 0 && s.Quantity <= s.MinStock);

        return new SupplyStatsViewModel
        {
            TotalSupplies = totalSupplies,
            TotalQuantity = totalQuantity,
            TotalInventoryValue = totalInventoryValue,
            OutOfStockCount = outOfStock,
            NeedReorderCount = needReorder
        };
    }
}