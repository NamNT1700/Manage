using System;
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
            string messageCheck = _allowanceRepositoryWrapper.Allowance.CheckNull( allowance.Code, allowance.Id);
            if (message != null )
            {
                responce.message = message;
                responce.status = "400";
                return responce;
            }
            if (messageCheck != null)
            {
                responce.message = messageCheck;
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
            if (request.pageNum > request.pageSize)
            {
                response.status = "400";
                response.success = false;
                response.message = "this page is not exist";
                return response;
            }
            if (request.pageNum <= 0 && request.pageSize <=0)
            {
                response.status = "400";
                response.success = false;
                response.message = "pageNum and pageSize error";
                return response;
            }

           
            List<HuAllowance> huAllowances = await _allowanceRepositoryWrapper.Allowance.GetAll();
            List<ListAllowanceDTO> listAllwance =  _mapper.Map<List<ListAllowanceDTO>>(huAllowances);
            List<ListAllowanceDTO> lists = new List<ListAllowanceDTO>();
            int firstIndex = (request.pageNum - 1) * request.pageSize;
            if (firstIndex >= huAllowances.Count())
            {
                response.status = "400";
                response.success = false;
                response.message = "no data";
                return response;
            }
            if (firstIndex + request.pageSize < huAllowances.Count())
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
            response.message = $"no allowance with id {id} exist";
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
                HuAllowance allowance = await _allowanceRepositoryWrapper.Allowance.FindById(id);
                await _allowanceRepositoryWrapper.Allowance.Delete(allowance);
            }
            response.message = "Delete allowance";
            response.status = "200";
            response.success = true;
            return response;
        }
    }
}
