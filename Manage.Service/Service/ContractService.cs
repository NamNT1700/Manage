using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Manage.Common;
using Manage.Model.Context;
using Manage.Model.DTO.Contract;
using Manage.Model.Models;
using Manage.Repository.Base.IRepository;
using Manage.Repository.Base.IRepository.IWrapper;
using Manage.Service.IService;

namespace Manage.Service.Service
{
    public class ContractService : IContractService
    {
        private IMapper _mapper;
        private IHuContractRepositoryWapper _contractRepositoryWapper;
        private DatabaseContext _context;


        public ContractService(IMapper mapper, IHuContractRepositoryWapper contractRepositoryWapper, DatabaseContext context)
        {
            _mapper = mapper;
            _contractRepositoryWapper = contractRepositoryWapper;
            _context = context;
        }

        public async Task<Response> AddNew(ContractDTO contract)
        {
            Response responce = new Response();
            string message = await _contractRepositoryWapper.Contract.CheckData(contract);
            string messageCheck = _contractRepositoryWapper.Contract.CheckNull(contract.Code, contract.Id);
            if (messageCheck != null)
            {
                responce.message = messageCheck;
                responce.status = "400";
                return responce;
            }
            if (message != null)
            {
                responce.message = message;
                responce.status = "400";
                return responce;
            }

            HuContract huContract = _mapper.Map<HuContract>(contract);
            huContract.CreatedTime = DateTime.Now;
            huContract.LastUpdateTime = DateTime.Now;
            await _contractRepositoryWapper.Contract.Create(huContract);
            await _context.SaveChangesAsync();
            ContractDTO bankDto = _mapper.Map<ContractDTO>(huContract);
            responce.status = "200";
            responce.item = bankDto;
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
            if (request.pageNum <= 0 && request.pageSize <= 0)
            {
                response.status = "400";
                response.success = false;
                response.message = "pageNum and pageSize error";
                return response;
            }
            List<HuContract> huContracts = await _contractRepositoryWapper.Contract.GetAll();
            List<ListContractDTO> listAllwance = _mapper.Map<List<ListContractDTO>>(huContracts);
            List<ListContractDTO> lists = new List<ListContractDTO>();
            int firstIndex = (request.pageNum - 1) * request.pageSize;
            if (firstIndex >= huContracts.Count())
            {
                response.status = "400";
                response.success = false;
                response.message = "no data";
                return response;
            }
            if (firstIndex + request.pageSize < huContracts.Count())
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
            HuContract huContract = await _contractRepositoryWapper.Contract.FindById(id);
            if (huContract != null)
            {
                ContractDTO contract = _mapper.Map<ContractDTO>(huContract);
                response.item = contract;
                response.status = "200";
                response.success = true;
                return response;
            }
            response.message = $"no contract with id {id} exist";
            response.status = "400";
            response.success = false;
            return response;
        }



        public async Task<Response> Update(UpdateContractDTO update)
        {
            Response response = new Response();
            HuContract contract = await _contractRepositoryWapper.Contract.FindById(update.Id);
            if (contract != null)
            {
                _mapper.Map(update.updateData, contract);
                contract.LastUpdateTime = DateTime.Now;
                await _context.SaveChangesAsync();
                response.status = "200";
                response.success = true;
                response.item = contract;
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
                HuContract bank = await _contractRepositoryWapper.Contract.FindById(id);
                await _contractRepositoryWapper.Contract.Delete(bank);
            }
            response.message = "Delete contract";
            response.status = "200";
            response.success = true;
            return response;
        }
    }
}
