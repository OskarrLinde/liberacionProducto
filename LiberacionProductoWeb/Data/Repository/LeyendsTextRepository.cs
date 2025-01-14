using LiberacionProductoWeb.Data.Repository.Base;
using LiberacionProductoWeb.Models.DataBaseModels;

namespace LiberacionProductoWeb.Data.Repository
{
    public class LeyendsTextRepository: Repository<LeyendsText>, ILeyendsTextRepository
    {
        private readonly AppDbContext _appDbContext;
        public LeyendsTextRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
}
