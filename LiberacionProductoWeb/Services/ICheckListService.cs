using LiberacionProductoWeb.Models.DataBaseModels;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Services
{
    public interface ICheckListService
    {
        Task<CheckListPipeDictiumAnswer> GetCheckListCloseOk();
    }
}
