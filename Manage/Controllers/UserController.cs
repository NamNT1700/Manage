using Manage.Common;
using Manage.Model.DTO.User;
using Manage.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manage.API.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser( UserDTO user)
        {
            Response res = await _userService.Register(user);
            return Ok(res);
        }
        [HttpPost("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers(Request baseRequest)
        {
            Response res = await _userService.GetAllUsers(baseRequest);
            return Ok(res);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO user)
        {
            Response res = await _userService.Login(user);
            return Ok(res);
        }
        [HttpDelete("User")]
        public async Task<IActionResult> Delete(List<int> id)
        {
            Response res = await _userService.DeleteUsers(id);
            return Ok(res);
        }
        [HttpPut("StatusUser")]
        // [Authorize(Roles="Admin")]
        public async Task<IActionResult> UpdateStatusToInActive(UserDTO user)
        {
            Response res = await _userService.ChangeStatusUser(user);
            return Ok(res);
        }
    }
}
