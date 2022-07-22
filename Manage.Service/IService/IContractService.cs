using Manage.Common;
using Manage.Model.DTO.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Service.IService
{
    public interface IContractService
    {
        Task<Response> AddNew(ContractDTO bank);
        Task<Response> GetAll(Request request);
        Task<Response> GetById(int id);
        Task<Response> Update(UpdateContractDTO update);
        Task<Response> Delete(List<int> ids);
    }
}
