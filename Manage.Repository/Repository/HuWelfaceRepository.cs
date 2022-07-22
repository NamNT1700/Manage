using Manage.Model.Context;
using Manage.Model.Models;
using Manage.Repository.Base.Repository;
using Manage.Repository.IRepository;

namespace Manage.Repository.Repository
{
   public class HuWelfareRepository : RepositoryBase<HuWelfare>, IHuWelfareRepository
    {
        public HuWelfareRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
