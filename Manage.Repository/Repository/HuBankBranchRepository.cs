using Manage.Model.Context;
using Manage.Model.Models;
using Manage.Repository.Base.Repository;
using Manage.Repository.IRepository;

namespace Manage.Repository.Repository
{
    public class HuBankBranchRepository : RepositoryBase<HuBankBranch>, IHuBankBranchRepository
    {
        public HuBankBranchRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
