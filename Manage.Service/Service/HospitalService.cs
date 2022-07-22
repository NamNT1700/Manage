using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Manage.Common;
using Manage.Model.Context;
using Manage.Model.DTO.Hospital;
using Manage.Model.Models;
using Manage.Repository.Base.IRepository;
using Manage.Repository.Base.IRepository.IWrapper;
using Manage.Service.IService;

namespace Manage.Service.Service
{
    public class HospitalService : IHospitalService
    {
        private IMapper _mapper;
        private IHuHospitalRepositoryWrapper _hospitalRepositoryWrapper;
        private DatabaseContext _context;


        public HospitalService(IMapper mapper, IHuHospitalRepositoryWrapper hospitalRepositoryWrapper, DatabaseContext context)
        {
            _mapper = mapper;
            _hospitalRepositoryWrapper = hospitalRepositoryWrapper;
            _context = context;
        }

        public async Task<Response> AddNew(HospitalDTO hospital)
        {
            Response responce = new Response();
            string message = await _hospitalRepositoryWrapper.Hospital.CheckData(hospital);
            string messageCheck = _hospitalRepositoryWrapper.Hospital.CheckNull(hospital.Code, hospital.Id);
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

            HuHospital huHospital = _mapper.Map<HuHospital>(hospital);
            huHospital.CreatedTime = DateTime.Now;
            huHospital.LastUpdateTime = DateTime.Now;
            await _hospitalRepositoryWrapper.Hospital.Create(huHospital);
            await _context.SaveChangesAsync();
            HospitalDTO hospitalDto = _mapper.Map<HospitalDTO>(huHospital);
            responce.status = "200";
            responce.item = hospitalDto;
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
            List<HuHospital> huContracts = await _hospitalRepositoryWrapper.Hospital.GetAll();
            List<ListHospitalDTO> listAllwance = _mapper.Map<List<ListHospitalDTO>>(huContracts);
            List<ListHospitalDTO> lists = new List<ListHospitalDTO>();
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
            HuHospital hospital = await _hospitalRepositoryWrapper.Hospital.FindById(id);
            if (hospital != null)
            {
                HospitalDTO contract = _mapper.Map<HospitalDTO>(hospital);
                response.item = contract;
                response.status = "200";
                response.success = true;
                return response;
            }
            response.message = $"no hospital with id {id} exist";
            response.status = "400";
            response.success = false;
            return response;
        }



        public async Task<Response> Update(UpdateHospitalDTO update)
        {
            Response response = new Response();
            HuHospital hospital = await _hospitalRepositoryWrapper.Hospital.FindById(update.Id);
            if (hospital != null)
            {
                _mapper.Map(update.updateData, hospital);
                hospital.LastUpdateTime = DateTime.Now;
                await _context.SaveChangesAsync();
                response.status = "200";
                response.success = true;
                response.item = hospital;
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
                HuHospital hospital = await _hospitalRepositoryWrapper.Hospital.FindById(id);
                await _hospitalRepositoryWrapper.Hospital.Delete(hospital);
            }
            response.message = "Delete hospital";
            response.status = "200";
            response.success = true;
            return response;
        }
    }
}
