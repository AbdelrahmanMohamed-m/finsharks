using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DataLayer.Mappers.CommentMapper;
using api.Dtos.StockDto;
using api.Models;

namespace api.Mappers.Stockmapper
{
    public static class StockMapper
    {

        // Map Stock to StockDto
        public static StockDto ToStockDto(this Stock StockModel)
        {
            return new StockDto
            {
                Id = StockModel.Id,
                Symbol = StockModel.Symbol,
                CompanyName = StockModel.CompanyName,
                Purshase = StockModel.Purshase,
                LastDivdend = StockModel.LastDivdend,
                Industry = StockModel.Industry,
                Marektcap = StockModel.Marektcap,
                Comments = StockModel.Comments.Select(comments => comments.TocommentDto()).ToList()
            };
        }

        // Map StockCreateRequestDto to Stock
        public static Stock FromStockRequestToStockModel(this StockCreateRequestDto stockCreateRequestDto)
        {
            return new Stock
            {
                Symbol = stockCreateRequestDto.Symbol,
                CompanyName = stockCreateRequestDto.CompanyName,
                Purshase = stockCreateRequestDto.Purshase,
                LastDivdend = stockCreateRequestDto.LastDivdend,
                Industry = stockCreateRequestDto.Industry,
                Marektcap = stockCreateRequestDto.Marektcap

            };
        }

        public static void UpdateFromDto(this Stock stock, StockUpdateRequestDto updateDto)
        {
            stock.Symbol = updateDto.Symbol;
            stock.CompanyName = updateDto.CompanyName;
            stock.Purshase = updateDto.Purshase;
            stock.LastDivdend = updateDto.LastDivdend;
            stock.Industry = updateDto.Industry;
            stock.Marektcap = updateDto.Marektcap;
        }
    }
}
