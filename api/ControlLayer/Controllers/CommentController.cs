using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.ControlLayer.Interfaces;
using api.ControlLayer.Repositories;
using api.DataLayer.Dtos.CommentDto;
using api.DataLayer.Mappers.CommentMapper;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.ControlLayer.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentStock _commentRepo;
        private readonly IStockRepo _stockRepo;
        private readonly UserManager<AppUser> _userManager;

        public CommentController(ICommentStock commentRepo, IStockRepo stockRepo, UserManager<AppUser> userManager)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var Comments = await _commentRepo.GetComments();
            var CommentsDto = Comments.Select(Comment => Comment.TocommentDto()).ToList();
            return Ok(CommentsDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var Comment = await _commentRepo.GetCommentById(id);
            if (Comment == null)
            {
                return NotFound();
            }
            return Ok(Comment.TocommentDto());
        }


        [HttpPost("{Stockid:int}")]
        public async Task<IActionResult> AddComment([FromRoute] int Stockid, CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!await _stockRepo.Exists(Stockid))
            {
                return BadRequest("Stock does not exist");
            }

            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);

            var comment = commentDto.ToComment(Stockid);
            comment.AppUserId = appUser.Id;
            await _commentRepo.AddComment(comment);
            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment.TocommentDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, UpdateCommentDto commentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var comment = await _commentRepo.UpdateComment(id, commentDto);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.TocommentDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var comment = await _commentRepo.DeleteComment(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.TocommentDto());
        }
    }
}