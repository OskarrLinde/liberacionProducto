using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Models.External;
using LiberacionProductoWeb.Models.LayoutLeyendsViewModels;
using LiberacionProductoWeb.Models.ProductionOrderViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Services
{
    public interface IProductionOrderService
    {
        Task<IList<ProductionOrder>> GetAllAsync();
        Task<IList<VwLotesProduccionDetalle>> GetLot(string plant, string product, string tank, string productoNombre);
        Task<ProductionOrderViewModel> GetByIdAsync(int Id, int preview = 0);
        Task<Boolean> ForReleasedProductionOrder(int ProductionOrderId);
        Task<int> GetProductionOrderForProduct(string productId);
        Task<List<LeyendsPreviewText>> leyendsHistoryTexts(Guid guid, bool style = false);
        Task<List<LayoutLeyendsProductionOrderVM>> GenerateLeyends(ProductionOrderViewModel model, int preview = 0);
        Task<ProductionOrderCustomersFiles> SaveCustomersFiles(ProductionOrderViewModel model, string TableName, IFormFile files);
        Task<bool> DeleteCustomerFile(ProductionOrderViewModel model);
        Task<List<ProductionOrderCustomersFilesViewModel>> GetCustomersFiles(int ProductionOrderId);
        Task<int> GetConditioningOrderForProduct(string productId);
        Task<bool> ValidateExistFile(ProductionOrderViewModel model, string TableName, IFormFileCollection files);
    }
}
