using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.StockDto;
using api.Models;
using api.Utils;

namespace api.Interfaces
{
    public interface IStockRepo
    {
        Task<List<Stock>> Getall(QueryObject query);

        Task<Stock?> GetbyId(int id);
        Task <Stock?> GetbySymbol(String Symbol);
        Task<Stock> Create(Stock stock);

        Task<Stock?> Update(int id, StockUpdateRequestDto stock);

        Task<Stock?> Delete(int id);
         
        Task<bool> Exists(int id);
 

    }
}