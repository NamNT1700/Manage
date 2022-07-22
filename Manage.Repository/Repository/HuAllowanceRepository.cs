using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manage.Model.Context;
using Manage.Model.DTO.Allowance;
using Manage.Model.Models;
using Manage.Repository.Base.Repository;
using Manage.Repository.IRepository;

namespace Manage.Repository.Repository
{
    public class HuAllowanceRepository : RepositoryBase<HuAllowance>, IHuAllowanceRepository
    {
        public HuAllowanceRepository(DatabaseContext context) : base(context)
        {
        }
        public async Task<string> CheckData(AllowanceDTO allowance)
        {
            HuAllowance huAllowance = await FindByCode(allowance.Code);
            if (huAllowance != null)
                return "code already exist";
            huAllowance = await FindById(allowance.Id);
            if (huAllowance != null)
                return "id already exist";
            
            return null;
        }
        public async Task<List<HuAllowance>> GetAll()
        {
            return await Task.Run(()=> FindAll().OrderBy(a => a.Id).ToList());
        }
    }

}
