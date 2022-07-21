﻿using System;
using AutoMapper;
using Manage.Common;
using Manage.Service.IService;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manage.Model.Context;
using Manage.Model.DTO.Allowance;
using Manage.Model.Models;
using Manage.Repository.Base.IRepository.IWrapper;

namespace Manage.Service.Service
{
    public class AllowanceService :IAllowanceService
    {
        private IMapper _mapper;
        private IHuAllowanceRepositoryWrapper _allowanceRepositoryWrapper;
        private DatabaseContext _context ;
        

        public AllowanceService(IMapper mapper, IHuAllowanceRepositoryWrapper allowanceRepositoryWrapper, DatabaseContext context)
        {
            _mapper = mapper;
            _allowanceRepositoryWrapper = allowanceRepositoryWrapper;
            _context = context;
        }
        public async Task<Response> AddNew(AllowanceDTO allowance)
        {
            Response responce = new Response();
            string message = await _allowanceRepositoryWrapper.Allowance.CheckData(allowance);
            if (message != null)
            {
                responce.message = message;
                responce.status = "400";
                return responce;
            }

            HuAllowance huAllowance = _mapper.Map<HuAllowance>(allowance);
            huAllowance.CreatedTime = DateTime.Now;
            huAllowance.LastUpdateTime = DateTime.Now;
            await _allowanceRepositoryWrapper.Allowance.Create(huAllowance);
            await _context.SaveChangesAsync();
            AllowanceDTO allowanceDTO = _mapper.Map<AllowanceDTO>(huAllowance);
            responce.status = "200";
            responce.item = allowanceDTO;
            return responce;
        }

       

        public async Task<Response> GetAll(Request request)
        {
            Response response = new Response();
            List<HuAllowance> huAllwances = await _allowanceRepositoryWrapper.Allowance.GetAll();
            List<ListAllowanceDTO> listAllwance =  _mapper.Map<List<ListAllowanceDTO>>(huAllwances);
            List<ListAllowanceDTO> lists = new List<ListAllowanceDTO>();
            int firstIndex = (request.pageNum - 1) * request.pageSize;
            if (firstIndex >= huAllwances.Count())
            {
                response.status = "400";
                response.success = false;
                response.message = "no user yet";
                return response;
            }
            if (firstIndex + request.pageSize < huAllwances.Count())
                lists = listAllwance.GetRange(firstIndex, request.pageSize);
            else lists = listAllwance.GetRange(firstIndex, listAllwance.Count - firstIndex);
            response.status = "200";
            response.success = true;
            response.item = lists;
            return response;
        }

        public async Task<Response> GetById(int id)
        {
            Response response = new Response();
            HuAllowance huAllowance = await _allowanceRepositoryWrapper.Allowance.FindById(id);
            if (huAllowance != null)
            {
                AllowanceDTO allowance = _mapper.Map<AllowanceDTO>(huAllowance);
                response.item = allowance;
                response.status = "200";
                response.success = true;
                return response;
            }
            response.message = $"no allwance with id {id} exist";
            response.status = "400";
            response.success = false;
            return response;
        }

        public async Task<Response> Update(UpdateAllowanceDTO update)
        {
            Response response = new Response();
            HuAllowance allowance = await _allowanceRepositoryWrapper.Allowance.FindById(update.id);
            if(allowance!=null)
            {
                _mapper.Map(update.updateData, allowance);
                allowance.LastUpdateTime = DateTime.Now;
                await _context.SaveChangesAsync();
                response.status = "200";
                response.success = true;
                response.item = allowance;
                return response;
            }
            response.message = "update data fail";
            response.status = "400";
            response.success = false;
            return response;
        }
        public async Task<Response> Delete(List<int> ids)
        {
            Response response = new Response();
            foreach (int id in ids)
            {
                HuAllowance allwance = await _allowanceRepositoryWrapper.Allowance.FindById(id);
                await _allowanceRepositoryWrapper.Allowance.Delete(allwance);
            }
            response.message = "Delete allwance";
            response.status = "200";
            response.success = true;
            return response;
        }
    }
}