using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manage.Common;
using Manage.Model.DTO.Nation;
using Manage.Model.DTO.Title;
using Manage.Service.IService;

namespace Manage.API.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    public class NationController : ControllerBase
    {
        private INationService _nationService;
        public NationController(INationService nationService)
        {
            _nationService = nationService;
        }
        [HttpPost("AddNewNation")]
        public async Task<IActionResult> AllNew( NationDTO nation)
        {
            Response response = await _nationService.AddNew(nation);
            return Ok(response);
        }
        [HttpPost("GetAllNation")]
        public async Task<IActionResult> GetAll( Request request)
        {
            Response response = await _nationService.GetAll(request);
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            Response response = await _nationService.GetById(id);
            return Ok(response);
        }
        [HttpPut("UpdateNation")]
        public async Task<IActionResult> Update(UpdateNationDTO update)
        {
            Response response = await _nationService.Update(update);
            return Ok(response);
        }
        [HttpDelete("DeleteNation")]
        public async Task<IActionResult> Delete(List<int> ids)
        {
            Response response = await _nationService.Delete(ids);
            return Ok(response);
        }
    }
}
