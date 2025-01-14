using LiberacionProductoWeb.Data.Repository.Base;
using LiberacionProductoWeb.Models.DataBaseModels;

namespace LiberacionProductoWeb.Data.Repository
{
    public class ProductionOrderCustomersFilesRepository : Repository<ProductionOrderCustomersFiles>, IProductionOrderCustomersFilesRepository
    {
        private readonly AppDbContext _appDbContext;
        public ProductionOrderCustomersFilesRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
}
