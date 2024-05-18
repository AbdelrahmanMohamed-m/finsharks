using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DataLayer.Dtos.CommentDto;
using api.Models;

namespace api.ControlLayer.Interfaces
{
    public interface ICommentStock
    {
        Task<Comment> AddComment(Comment comment);
        Task<Comment?> DeleteComment(int id);
        Task<Comment?> GetCommentById(int id);
        Task<List<Comment>> GetComments();
        Task<Comment?> UpdateComment(int id, UpdateCommentDto comment);
    }
}