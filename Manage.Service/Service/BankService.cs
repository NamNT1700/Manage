using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Manage.Common;
using Manage.Model.Context;
using Manage.Model.DTO.Bank;
using Manage.Model.Models;
using Manage.Repository.Base.IRepository.IWrapper;
using Manage.Service.IService;

namespace Manage.Service.Service
{
    public class BankService : IBankService
    {
        private IMapper _mapper;
        private IHuBankRepositoryWrapper _bankRepositoryWrapper;
        private DatabaseContext _context;


        public BankService(IMapper mapper, IHuBankRepositoryWrapper bankRepositoryWrapper, DatabaseContext context)
        {
            _mapper = mapper;
            _bankRepositoryWrapper = bankRepositoryWrapper;
            _context = context;
        }

        public async Task<Response> AddNew(BankDTO bank)
        {
            Response responce = new Response();
            string message = await _bankRepositoryWrapper.Bank.CheckData(bank);
            string messageCheck = _bankRepositoryWrapper.Bank.CheckNull(bank.Code, bank.Id);
            if (message != null)
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

            HuBank huBank = _mapper.Map<HuBank>(bank);
            huBank.CreatedTime = DateTime.Now;
            huBank.LastUpdateTime = DateTime.Now;
            await _bankRepositoryWrapper.Bank.Create(huBank);
            await _context.SaveChangesAsync();
            BankDTO bankDto = _mapper.Map<BankDTO>(huBank);
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
            List<HuBank> huBanks = await _bankRepositoryWrapper.Bank.GetAll();
            List<ListBankDTO> listBankDtos = _mapper.Map<List<ListBankDTO>>(huBanks);
            List<ListBankDTO> list = new List<ListBankDTO>();
            int firstIndex = (request.pageNum - 1) * request.pageSize;
            if (firstIndex >= huBanks.Count())
            {
                response.status = "400";
                response.success = false;
                response.message = "no data";
                return response;
            }
            if (firstIndex + request.pageSize < huBanks.Count())
                list = listBankDtos.GetRange(firstIndex, request.pageSize);
            else list = listBankDtos.GetRange(firstIndex, listBankDtos.Count - firstIndex);
            response.status = "200";
            response.success = true;
            response.item = list;
            return response;
        }

        public async Task<Response> GetById(int id)
        {
            Response response = new Response();
            HuBank huBank = await _bankRepositoryWrapper.Bank.FindById(id);
            if (huBank != null)
            {
                BankDTO bank = _mapper.Map<BankDTO>(huBank);
                response.item = bank;
                response.status = "200";
                response.success = true;
                return response;
            }
            response.message = $"no bank with id {id} exist";
            response.status = "400";
            response.success = false;
            return response;
        }

       

        public async Task<Response> Update(UpdateBankDTO update)
        {
            Response response = new Response();
            HuBank bank = await _bankRepositoryWrapper.Bank.FindById(update.Id);
            if (bank != null)
            {
                _mapper.Map(update.updateData, bank);
                bank.LastUpdateTime = DateTime.Now;
                await _context.SaveChangesAsync();
                response.status = "200";
                response.success = true;
                response.item = bank;
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
                HuBank bank = await _bankRepositoryWrapper.Bank.FindById(id);
                await _bankRepositoryWrapper.Bank.Delete(bank);
            }
            response.message = "Delete bank";
            response.status = "200";
            response.success = true;
            return response;
        }
    }
}
