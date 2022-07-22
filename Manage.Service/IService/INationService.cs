using Manage.Common;
using Manage.Model.DTO.Nation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Service.IService
{
    public interface INationService
    {
        Task<Response> AddNew(NationDTO nation);
        Task<Response> GetAll(Request request);
        Task<Response> GetById(int id);
        Task<Response> Update(UpdateNationDTO update);
        Task<Response> Delete(List<int> ids);
    }
}
