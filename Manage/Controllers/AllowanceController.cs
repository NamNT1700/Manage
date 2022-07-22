using Manage.Common;
using Manage.Service.IService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Manage.Model.DTO.Allowance;

namespace Manage.API.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    public class AllowanceController : ControllerBase
    {
        private IAllowanceService _allowanceService;
        public AllowanceController(IAllowanceService allowanceService)
        {
            _allowanceService = allowanceService;
        }
        [HttpPost("allowance-insert")]
        public async Task<IActionResult> AllNew( AllowanceDTO allowanceDto)
        {
            Response response = await _allowanceService.AddNew(allowanceDto);
            return Ok(response);
        }
        [HttpPost("allowance-get-all")]
        public async Task<IActionResult> GetAll( Request request)
        {
            Response response = await _allowanceService.GetAll(request);
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            Response response = await _allowanceService.GetById(id);
            return Ok(response);
        }
        [HttpPut("allowance-update")]
        public async Task<Response> Update( UpdateAllowanceDTO update)
        {
            Response response = await _allowanceService.Update(update);
            return response;
        }
        [HttpDelete("allowance-delete")]
        public async Task<IActionResult> Delete(List<int> ids)
        {
            Response response = await _allowanceService.Delete(ids);
            return Ok(response);
        }
    }
}
