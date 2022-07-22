using Manage.Common;
using Manage.Model.DTO.Title;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Service.IService
{
    public interface ITitleService
    {
        Task<Response> AddNew(TitleDTO title);
        Task<Response> GetAll(Request request);
        Task<Response> GetById(int id);
        Task<Response> Update(UpdateTitleDTO update);
        Task<Response> Delete(List<int> ids);
    }
}
