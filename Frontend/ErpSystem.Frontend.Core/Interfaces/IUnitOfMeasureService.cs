using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Core.Models.UnitsOfMeasure;

namespace ErpSystem.Frontend.Core.Interfaces;

public interface IUnitOfMeasureService
{
    Task<PageResult<UnitOfMeasureViewModel>> GetUnitsOfMeasureAsync(int page = 1, int pageSize = 10);
    Task<UnitOfMeasureViewModel?> GetUnitOfMeasureByIdAsync(Guid id);
    Task<Guid> CreateUnitOfMeasureAsync(UnitOfMeasureEditModel model);
    Task UpdateUnitOfMeasureAsync(UnitOfMeasureEditModel model);
    Task DeleteUnitOfMeasureAsync(Guid id);
} 