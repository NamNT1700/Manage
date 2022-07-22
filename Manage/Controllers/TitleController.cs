using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manage.Common;
using Manage.Model.DTO.Title;
using Manage.Service.IService;

namespace Manage.API.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    public class TitleController : ControllerBase
    {
        private ITitleService _titleService;
        public TitleController(ITitleService titleService)
        {
            _titleService = titleService;
        }
        [HttpPost("AddNewTitle")]
        public async Task<IActionResult> AllNew(TitleDTO titleDto)
        {
            Response response = await _titleService.AddNew(titleDto);
            return Ok(response);
        }
        [HttpPost("GetAllTitle")]
        public async Task<IActionResult> GetAll( Request request)
        {
            Response response = await _titleService.GetAll(request);
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            Response response = await _titleService.GetById(id);
            return Ok(response);
        }
        [HttpPut("UpdateTitle")]
        public async Task<IActionResult> Update(UpdateTitleDTO update)
        {
            Response response = await _titleService.Update(update);
            return Ok(response);
        }
        [HttpDelete("DeleteTitle")]
        public async Task<IActionResult> Delete(List<int> ids)
        {
            Response response = await _titleService.Delete(ids);
            return Ok(response);
        }
    }
}
