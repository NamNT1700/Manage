using Manage.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manage.Model.DTO.Welface;

namespace Manage.Service.IService
{
    public interface IWelfaceService
    {
        Task<Response> AddNew(WelfaceDTO welface);
    }
}
