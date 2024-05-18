using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DataLayer.Dtos.CommentDto;
using api.Models;

namespace api.Dtos.StockDto
{
    public class StockDto
    {
        public int Id { get; set; }

        public string Symbol { get; set; } = String.Empty;

        public string CompanyName { get; set; } = String.Empty;

        public decimal Purshase { get; set; }


        public decimal LastDivdend { get; set; }

        public String Industry { get; set; } = String.Empty;

        public long Marektcap { get; set; }

        public decimal LastPrice { get; set; }

        public List<CommentDto> Comments { get; set; } = [];

    }
}