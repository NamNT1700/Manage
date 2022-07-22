﻿using Manage.Model.Context;
using Manage.Model.Models;
using Manage.Repository.Base.Repository;
using Manage.Repository.IRepository;

namespace Manage.Repository.Repository
{
    public class HuWardRepository : RepositoryBase<HuWard>, IHuWardRepository
    {
        public HuWardRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
