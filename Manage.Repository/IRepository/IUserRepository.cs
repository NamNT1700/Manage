using Manage.Model.Models;
using Manage.Repository.Base.IRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manage.Repository.IRepository
{
    public interface IUserRepository: IRepositoryBase<SeUser>
    {
        public Task<string> CheckUserLogin(string username, string password);
        public Task<SeUser> FindById(int id);
        //public Task<string> CheckUserInfo(string username, string email);
        public Task<SeUser> CheckPassword(string password);
        public Task<SeUser> FindByUsername(string username);
        public Task<List<SeUser>> FindAllData();
    }
}
