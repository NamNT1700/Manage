using System.Collections.Generic;
using System.Threading.Tasks;
using Manage.Model.DTO.Nation;
using Manage.Model.Models;
using Manage.Repository.Base.IRepository;

namespace Manage.Repository.IRepository
{
    public interface IHuNationRepository : IRepositoryBase<HuNation>
    {
        public Task<string> CheckData(NationDTO nation);
        public Task<HuNation> FindByCode(string code);
        public Task<HuNation> FindById(int id);
        public Task<List<HuNation>> GetAll();
    }
}
