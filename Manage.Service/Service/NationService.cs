using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Manage.Common;
using Manage.Model.Context;
using Manage.Model.DTO.Hospital;
using Manage.Model.DTO.Nation;
using Manage.Model.Models;
using Manage.Repository.Base.IRepository;
using Manage.Repository.Base.IRepository.IWrapper;
using Manage.Service.IService;

namespace Manage.Service.Service
{
    public class NationService : INationService
    {
        private IMapper _mapper;
        private IHuNationRepositoryWrapper _huNationRepositoryWrapper;
        private DatabaseContext _context;


        public NationService(IMapper mapper, IHuNationRepositoryWrapper huNationRepositoryWrapper, DatabaseContext context)
        {
            _mapper = mapper;
            _huNationRepositoryWrapper = huNationRepositoryWrapper;
            _context = context;
        }

        public async Task<Response> AddNew(NationDTO nation)
        {
            Response responce = new Response();
            string message = await _huNationRepositoryWrapper.Nation.CheckData(nation);
            string messageCheck = _huNationRepositoryWrapper.Nation.CheckNull(nation.Code, nation.Id);
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

            HuNation huNation = _mapper.Map<HuNation>(nation);
            huNation.CreatedTime = DateTime.Now;
            huNation.LastUpdateTime = DateTime.Now;
            await _huNationRepositoryWrapper.Nation.Create(huNation);
            await _context.SaveChangesAsync();
            NationDTO hospitalDto = _mapper.Map<NationDTO>(huNation);
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
            List<HuNation> huNations = await _huNationRepositoryWrapper.Nation.GetAll();
            List<ListNationDTO> listAllwance = _mapper.Map<List<ListNationDTO>>(huNations);
            List<ListNationDTO> lists = new List<ListNationDTO>();
            int firstIndex = (request.pageNum - 1) * request.pageSize;
            if (firstIndex >= huNations.Count())
            {
                response.status = "400";
                response.success = false;
                response.message = "no data";
                return response;
            }
            if (firstIndex + request.pageSize < huNations.Count())
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
            HuNation nation = await _huNationRepositoryWrapper.Nation.FindById(id);
            if (nation != null)
            {
                HospitalDTO contract = _mapper.Map<HospitalDTO>(nation);
                response.item = contract;
                response.status = "200";
                response.success = true;
                return response;
            }
            response.message = $"no Nation with id {id} exist";
            response.status = "400";
            response.success = false;
            return response;
        }



        public async Task<Response> Update(UpdateNationDTO update)
        {
            Response response = new Response();
            HuNation nation = await _huNationRepositoryWrapper.Nation.FindById(update.Id);
            if (nation != null)
            {
                _mapper.Map(update.updateData, nation);
                nation.LastUpdateTime = DateTime.Now;
                await _context.SaveChangesAsync();
                response.status = "200";
                response.success = true;
                response.item = nation;
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
                HuNation hospital = await _huNationRepositoryWrapper.Nation.FindById(id);
                await _huNationRepositoryWrapper.Nation.Delete(hospital);
            }
            response.message = "Delete nation";
            response.status = "200";
            response.success = true;
            return response;
        }
    }
}
