using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manage.Model.Context;
using Manage.Model.DTO.Title;
using Manage.Model.Models;
using Manage.Repository.Base.Repository;
using Manage.Repository.IRepository;

namespace Manage.Repository.Repository
{
    public class HuTitleRepository : RepositoryBase<HuTitle>, IHuTitleRepository
    {
        public HuTitleRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<string> CheckData(TitleDTO title)
        {
            HuTitle huTitle = await FindByCode(title.Code);
            if (huTitle != null)
                return "code already exist";
            huTitle = await FindById(title.Id);
            if (huTitle != null)
                return "id already exist";
            return null;
        }
        public async Task<List<HuTitle>> GetAll()
        {
            return await Task.Run(() => FindAll().OrderBy(a => a.Id).ToList());
        }
    }
}
