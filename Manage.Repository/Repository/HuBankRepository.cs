﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manage.Model.Context;
using Manage.Model.Models;
using Manage.Repository.Base.Repository;
using Manage.Repository.IRepository;

namespace Manage.Repository.Repository
{
    public class HuBankRepository : RepositoryBase<HuBank>, IHuBankRepository
    {
        public HuBankRepository(DatabaseContext context) : base(context)
        {
        }
    }
}