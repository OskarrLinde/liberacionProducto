using LiberacionProductoWeb.Data.Repository.Base;
using LiberacionProductoWeb.Models.DataBaseModels;

namespace LiberacionProductoWeb.Data.Repository
{
    public class LeyendsPreviewTextRepository: Repository<LeyendsPreviewText>, ILeyendsPreviewTextRepository
    {
        private readonly AppDbContext _appDbContext;
        public LeyendsPreviewTextRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
}
