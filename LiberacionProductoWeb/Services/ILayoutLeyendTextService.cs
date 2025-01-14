using LiberacionProductoWeb.Models.LayoutLeyendsViewModels;
using System;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Services
{
    public interface ILayoutLeyendTextService
    {
        #region productionOrders
        Task AddLayoutProductionOrder(LayoutLeyendsVM leyendsCertificateVM, bool active = false);
        Task AddLayoutPreviewProductionOrder(LayoutLeyendsVM leyendsCertificateVM);
        Task<LayoutLeyendsVM> ProductionOrderText(string productId = "OX");
        Task<int> GetProductionOrderForProduct(string productId);
        #endregion

        #region conditioningOrders
        Task AddLayoutConditioningOrder(LayoutLeyendsVM leyendsCertificateVM, bool active = false);
        Task AddLayoutPreviewConditioningOrder(LayoutLeyendsVM leyendsCertificateVM);
        Task<LayoutLeyendsVM> ConditioningOrderText(string productId = "OX");
        Task<int> GetConditioningOrderForProduct(string productId);
        #endregion

        #region checklist
        Task<LayoutLeyendsVM> CheckListText();
        Task AddLayoutCheckList(LayoutLeyendsVM leyendsCertificateVM, bool active = false);
        Task AddLayoutPreviewCheckList(LayoutLeyendsVM leyendsCertificateVM);
        #endregion
        Task<Guid> GetlastGroupProduct(string productId, string type);

        Task<Guid> GetlastGroupCheckList(string productId);
    }
}
