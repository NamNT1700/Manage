using Manage.Model.Context;
using Manage.Repository.Base.IRepository.IWrapper;
using Manage.Repository.IRepository;
using Manage.Repository.Repository;

namespace Manage.Repository.Base.Repository.Wrapper
{
    public class HuNationRepositoryWrapper : IHuNationRepositoryWrapper
    {
        private  IHuNationRepository _nation;
        private readonly DatabaseContext _context;

        public HuNationRepositoryWrapper(DatabaseContext context)
        {
            _context = context;
            
        }
        public IHuNationRepository Nation {
            get
            {
                if (_nation == null)
                    _nation = new HuNationRepository(_context);
                return _nation;
            }
        }
    }
}
