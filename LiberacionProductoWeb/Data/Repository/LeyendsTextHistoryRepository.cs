using LiberacionProductoWeb.Data.Repository.Base;
using LiberacionProductoWeb.Models.DataBaseModels;

namespace LiberacionProductoWeb.Data.Repository
{
    public class LeyendsTextHistoryRepository : Repository<LeyendsTextHistory>, ILeyendsTextHistoryRepository
    {
        private readonly AppDbContext _appDbContext;
        public LeyendsTextHistoryRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
    public class LeyendsTextHistoryGroupRepository : Repository<LeyendsTextHistoryGroup>, ILeyendsTextHistoryGroupRepository
    {
        private readonly AppDbContext _appDbContext;
        public LeyendsTextHistoryGroupRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }

}