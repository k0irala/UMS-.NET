﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UMS.Models;
using UMS.Models.Entities;
using UMS.Repositories;

namespace UMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController(IAccountRepository repository) : ControllerBase
    {
        [HttpPost("EmployeeRegister")]
        public IActionResult Register(Employee emp)
        {

            return Ok();
        }

        [HttpPost("ManagerRegister")]
        public IActionResult Register(ManagerRegisterModel managerModel)
        {
            var result =repository.ManagerRegister(managerModel);

            if (result == HttpStatusCode.OK) { return Ok("Manager Registered Successfully"); }
            else if (result == HttpStatusCode.Conflict) { return Conflict("E"); }
            else return Conflict("Error in Registering Manager to the database");
        }
        [HttpPost]
        public IActionResult Login(LoginRequestModel model) 
        {
            var validateLogin = repository.Login(model);   
            return Ok(validateLogin);

        }
    }
}
