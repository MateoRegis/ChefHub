﻿using Application.Interfaces;
using Application.Models.Request;
using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChefHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        //[HttpGet("{recipeId}")]
        //public async Task<ActionResult> GetCommentsByRecipe([FromRoute] int recipeId)
        //{
        //    var response = await _commentService.GetCommentsByRecipe(recipeId);

        //    return Ok(new { success = true, data = response });
        //}
        [Authorize]
        [HttpPost("[action]")]

        public async Task<ActionResult> CreateComment([FromBody] CommentRequest request)
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userIdClaim == null)
                {
                    return Unauthorized(new { success = false, message = " Usuario No Autorizado" });

                }
                var response = await _commentService.CreateComment(request, int.Parse(userIdClaim));


                return Ok(new { success = true, data = response });

            }
            catch (Exception ex)
            {

                return NotFound(new { success = false, message = ex.Message });



            }


        }
        [Authorize]
        [HttpDelete("{commentId}")]
        public async Task<ActionResult> DeleteComment([FromRoute] int commentId, [FromQuery] int recipeId)
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                var userRoleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                await _commentService.DeleteComment(recipeId, commentId, int.Parse(userIdClaim), Enum.Parse<Role>(userRoleClaim));

                return NoContent();
            }
            catch (Exception ex)
            {

                return NotFound(new { success = false, message = ex.Message });

            }

        }
    }
}
