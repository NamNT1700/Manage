using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manage.Common;
using Manage.Model.DTO.Contract;
using Manage.Service.IService;

namespace Manage.API.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    public class ContractController : ControllerBase
    {
        private IContractService _contractService;
        public ContractController(IContractService contractService)
        {
            _contractService = contractService;
        }
        [HttpPost("AddNewContract")]
        public async Task<IActionResult> AllNew( ContractDTO contractDto)
        {
            Response response = await _contractService.AddNew(contractDto);
            return Ok(response);
        }
        [HttpPost("GetAllContract")]
        public async Task<IActionResult> GetAll( Request request)
        {
            Response response = await _contractService.GetAll(request);
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            Response response = await _contractService.GetById(id);
            return Ok(response);
        }
        [HttpPut("UpdateContract")]
        public async Task<IActionResult> Update(UpdateContractDTO update)
        {
            Response response = await _contractService.Update(update);
            return Ok(response);
        }
        [HttpDelete("DeleteContract")]
        public async Task<IActionResult> Delete(List<int> ids)
        {
            Response response = await _contractService.Delete(ids);
            return Ok(response);
        }
    }
}
