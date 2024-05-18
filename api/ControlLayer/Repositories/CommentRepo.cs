using api.ControlLayer.Interfaces;
using api.Data;
using api.DataLayer.Dtos.CommentDto;
using api.DataLayer.Mappers.CommentMapper;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.ControlLayer.Repositories
{
    public class CommentRepo : ICommentStock
    {

        private readonly ApplicationDBContext _context;

        public CommentRepo(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<Comment>> GetComments() { return await _context.Comments.Include(x => x.AppUser).ToListAsync(); }

        public async Task<Comment> AddComment(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }


        public async Task<Comment?> GetCommentById(int id)
        {
            return await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Comment?> UpdateComment(int id, UpdateCommentDto comment)
        {
            var existingComment = await _context.Comments.Include(x => x.AppUser).FirstOrDefaultAsync(x => x.Id == id);
            if (existingComment == null)
            {
                return null;
            }
            existingComment.UpdateFromDto(comment);
            await _context.SaveChangesAsync();
            return existingComment;
        }

        public async Task<Comment?> DeleteComment(int id)
        {
            var commentModel = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (commentModel == null)
            {
                return null;
            }
            _context.Comments.Remove(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }



    }
}