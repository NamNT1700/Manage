using Manage.Model.Context;
using Manage.Repository.Base.IRepository.IWrapper;
using Manage.Repository.IRepository;
using Manage.Repository.Repository;

namespace Manage.Repository.Base.Repository.Wrapper
{
    public class HuContractRepositoryWapper : IHuContractRepositoryWapper
    {
        private readonly DatabaseContext _context;
        private  IHuContractRepository _contract;

        public HuContractRepositoryWapper(DatabaseContext context)
        {
            _context = context;
        }
        public IHuContractRepository Contract {
            get
            {
                if (_contract == null)
                    _contract = new HuContractRepository(_context);
                return _contract;
            }
        }
    }
}
