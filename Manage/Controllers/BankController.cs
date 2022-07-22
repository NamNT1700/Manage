using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manage.Common;
using Manage.Model.DTO.Bank;
using Manage.Service.IService;

namespace Manage.API.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    public class BankController : ControllerBase
    {
        private IBankService _bankService;
        public BankController(IBankService bankService)
        {
            _bankService = bankService;
        }
        [HttpPost("bank-insert")]
        public async Task<IActionResult> AllNew( BankDTO bankDto)
        {
            Response response = await _bankService.AddNew(bankDto);
            return Ok(response);
        }
        [HttpPost("bank-get-all")]
        public async Task<IActionResult> GetAll( Request request)
        {
            Response response = await _bankService.GetAll(request);
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            Response response = await _bankService.GetById(id);
            return Ok(response);
        }
        [HttpPut("bank-update")]
        public async Task<IActionResult> Update(UpdateBankDTO update)
        {
            Response response = await _bankService.Update(update);
            return Ok(response);
        }
        [HttpDelete("bank-delete")]
        public async Task<IActionResult> Delete(List<int> ids)
        {
            Response response = await _bankService.Delete(ids);
            return Ok(response);
        }
    }
}
