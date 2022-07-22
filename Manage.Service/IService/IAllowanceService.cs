using Manage.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Manage.Model.DTO.Allowance;

namespace Manage.Service.IService
{
    public interface IAllowanceService 
    {
        Task<Response> AddNew(AllowanceDTO allowance);
        Task<Response> GetAll(Request request);
        Task<Response> GetById(int id);
        Task<Response> Update(UpdateAllowanceDTO update);
        Task<Response> Delete(List<int> ids);
    }
}
