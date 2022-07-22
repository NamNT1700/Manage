using Manage.Model.Context;
using Manage.Model.Models;
using Manage.Repository.Base.Repository;
using Manage.Repository.IRepository;

namespace Manage.Repository.Repository
{
    public class HuProvinceRepository : RepositoryBase<HuProvince>, IHuProvinceRepository
    {
        public HuProvinceRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
