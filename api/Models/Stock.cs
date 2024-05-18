using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Stock")]
    public class Stock
    {
        public int Id { get; set; }

        public string Symbol { get; set; } = String.Empty;

        public string CompanyName { get; set; } = String.Empty;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Purshase { get; set; }

        [Column(TypeName = "decimal(18, 2)")]

        public decimal LastDivdend { get; set; }

        public String Industry { get; set; } = String.Empty;

        public long Marektcap { get; set; }

        public List<Comment> Comments { get; set; } = [];

        public List<Portfolio> Portfolios { get; set; } = [];


    }
}