using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.StockDto
{
    public class StockCreateRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol must be at most 10 characters long")]
        public string Symbol { get; set; } = String.Empty;

        [Required]
        [MaxLength(50, ErrorMessage = "Company name must be at most 50 characters long")]
        public string CompanyName { get; set; } = String.Empty;

        [Required]
        [Range(1, 10000000, ErrorMessage = "Price must be between 0 and 10000000")]
        public decimal Purshase { get; set; }

        [Required]
        [Range(0.001, 100, ErrorMessage = "divdend  must be between 0 and 100")]
        public decimal LastDivdend { get; set; }


        [Required]
        [Range(1, 20, ErrorMessage = "industry name must be between 1 and 20")]
        public String Industry { get; set; } = String.Empty;

        [Required]
        [Range(1, 1000000000, ErrorMessage = "MarketCap must be between 1 and 1000000000")]
        public long Marektcap { get; set; }
    }
}