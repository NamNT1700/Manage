using Manage.Model.Context;
using Manage.Model.Models;
using Manage.Repository.Base.Repository;
using Manage.Repository.IRepository;

namespace Manage.Repository.Repository
{
    public class HuDistrictRepository : RepositoryBase<HuDistrict>, IHuDistrictRepository
    {
        public HuDistrictRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
