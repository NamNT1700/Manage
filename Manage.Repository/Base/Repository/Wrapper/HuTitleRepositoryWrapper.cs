using Manage.Model.Context;
using Manage.Repository.Base.IRepository.IWrapper;
using Manage.Repository.IRepository;
using Manage.Repository.Repository;

namespace Manage.Repository.Base.Repository.Wrapper
{
   public class HuTitleRepositoryWrapper : IHuTitleRepositoryWrapper
    {
        private readonly DatabaseContext _context;
        private  IHuTitleRepository _title;

        public HuTitleRepositoryWrapper(DatabaseContext context)
        {
            _context = context;
          
        }
        public IHuTitleRepository Title {
            get
            {
                if (_title == null)
                    _title = new HuTitleRepository(_context);
                return _title;
            }
        }
    }
}
