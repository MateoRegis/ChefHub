﻿using System.Security.Claims;
using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChefHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminUserController : ControllerBase
    {
        private readonly IUserService _userService;
        public AdminUserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<UserResponse>> CreateUserAsync(UserRequest request)
        {
            var userRoleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (userRoleClaim != Role.Admin.ToString())
            {
                return Unauthorized("Se necesita rol de administrador para ejecutar esta accion");
            }

            var response = await _userService.CreateUserAsync(request);

            if (response == null)
            {
                return BadRequest(new { success = false, message = "Ya existe un usuario con este correo" });
            }

            return Created("", new { success = true, data = response });
        }
    }
}
