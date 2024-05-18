using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
  [Table("Comment")]
    public class Comment
    {
        public int StockId { get; set; }

        public int Id { get; set; }

        public string Title { get; set; } = String.Empty;

        public string Content { get; set; } = String.Empty;

        public DateTime Created { get; set; } = DateTime.Now;

      // Stock propetey is for navigation property
        public Stock? Stock { get; set; } 

        public string AppUserId { get; set; } = String.Empty;

        public AppUser? AppUser { get; set; }
    }
}