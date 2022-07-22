using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Manage.Common;
using Manage.Model.Context;
using Manage.Model.DTO.Title;
using Manage.Model.Models;
using Manage.Repository.Base.IRepository;
using Manage.Repository.Base.IRepository.IWrapper;
using Manage.Service.IService;

namespace Manage.Service.Service
{
    public class TitleService : ITitleService
    {
        private IMapper _mapper;
        private IHuTitleRepositoryWrapper _huTitleRepositoryWrapper;
        private DatabaseContext _context;


        public TitleService(IMapper mapper, IHuTitleRepositoryWrapper huTitleRepositoryWrapper, DatabaseContext context)
        {
            _mapper = mapper;
            _huTitleRepositoryWrapper = huTitleRepositoryWrapper;
            _context = context;
        }

        public async Task<Response> AddNew(TitleDTO title)
        {
            Response responce = new Response();
            string message = await _huTitleRepositoryWrapper.Title.CheckData(title);
            string messageCheck = _huTitleRepositoryWrapper.Title.CheckNull(title.Code, title.Id);
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

            HuTitle huTitle = _mapper.Map<HuTitle>(title);
            huTitle.CreatedTime = DateTime.Now;
            huTitle.LastUpdateTime = DateTime.Now;
            await _huTitleRepositoryWrapper.Title.Create(huTitle);
            await _context.SaveChangesAsync();
            TitleDTO hospitalDto = _mapper.Map<TitleDTO>(huTitle);
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
            List<HuTitle> huNations = await _huTitleRepositoryWrapper.Title.GetAll();
            List<ListTitleDTO> listAllwance = _mapper.Map<List<ListTitleDTO>>(huNations);
            List<ListTitleDTO> lists = new List<ListTitleDTO>();
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
            HuTitle nation = await _huTitleRepositoryWrapper.Title.FindById(id);
            if (nation != null)
            {
                TitleDTO contract = _mapper.Map<TitleDTO>(nation);
                response.item = contract;
                response.status = "200";
                response.success = true;
                return response;
            }
            response.message = $"no Title with id {id} exist";
            response.status = "400";
            response.success = false;
            return response;
        }



        public async Task<Response> Update(UpdateTitleDTO update)
        {
            Response response = new Response();
            HuTitle nation = await _huTitleRepositoryWrapper.Title.FindById(update.Id);
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
                HuTitle title = await _huTitleRepositoryWrapper.Title.FindById(id);
                await _huTitleRepositoryWrapper.Title.Delete(title);
            }
            response.message = "Delete Title";
            response.status = "200";
            response.success = true;
            return response;
        }
    }
}
