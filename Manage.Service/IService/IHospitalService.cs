using Manage.Common;
using Manage.Model.DTO.Hospital;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Service.IService
{
    public interface IHospitalService
    {
        Task<Response> AddNew(HospitalDTO hospital);
        Task<Response> GetAll(Request request);
        Task<Response> GetById(int id);
        Task<Response> Update(UpdateHospitalDTO update);
        Task<Response> Delete(List<int> ids);
    }
}
