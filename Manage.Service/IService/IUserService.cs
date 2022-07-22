using Manage.Common;
using Manage.Model.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Service.IService
{
    public interface IUserService
    {
        public Task<Response> Register(UserDTO reUser);
        public Task<Response> GetAllUsers(Request request);
        public Task<Response> Login(LoginDTO user);
        public Task<Response> DeleteUsers(List<int> ids);
        public Task<Response> FindUserById(int id);
        public Task<Response> ChangeStatusUser(UserDTO user);
    }
}
