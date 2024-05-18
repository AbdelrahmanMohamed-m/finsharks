using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DataLayer.Dtos.CommentDto;
using api.Models;

namespace api.DataLayer.Mappers.CommentMapper
{
    public static class CommentMapper
    {
        public static CommentDto TocommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                StockId = commentModel.StockId,
                Title = commentModel.Title,
                Content = commentModel.Content,
                Created = commentModel.Created,
                CreatedBy = commentModel.AppUser.UserName
            };
        }

        public static Comment ToComment(this CreateCommentDto commentDto, int stockId)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockId
            };

            
        }
        public static void UpdateFromDto(this Comment comment, UpdateCommentDto updateDto)
            {
                comment.Title = updateDto.Title;
                comment.Content = updateDto.Content;
            }
    }
}