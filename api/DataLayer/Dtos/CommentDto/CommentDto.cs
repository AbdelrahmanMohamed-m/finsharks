using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.DataLayer.Dtos.CommentDto
{
    public class CommentDto
    {

        public int Id { get; set; }
        public int StockId { get; set; }

        public string Title { get; set; } = String.Empty;

        public string Content { get; set; } = String.Empty;

        public DateTime Created { get; set; } = DateTime.Now;

        public String CreatedBy { get; set; } = String.Empty;
    }
}