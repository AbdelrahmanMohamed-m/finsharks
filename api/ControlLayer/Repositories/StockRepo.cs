using api.Data;
using api.Dtos.StockDto;
using api.Interfaces;
using api.Mappers.Stockmapper;
using api.Models;
using api.Utils;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class StockRepo : IStockRepo
    {
        public readonly ApplicationDBContext _context;

        public StockRepo(ApplicationDBContext context) { _context = context; }

        public async Task<Stock> Create(Stock stock)
        {
            await _context.stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> Delete(int id)
        {
            var stock = await _context.stocks.FindAsync(id);
            if (stock == null)
            {
                return null;
            }
            _context.stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.stocks.AnyAsync(stock => stock.Id == id);
        }

        public async Task<List<Stock>> Getall(QueryObject query)
        {
            var Stocks = _context.stocks.Include(stock => stock.Comments).ThenInclude(comment => comment.AppUser).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                Stocks = Stocks.Where(stock => stock.Symbol.Contains(query.Symbol));
            }
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                Stocks = Stocks.Where(stock => stock.CompanyName.Contains(query.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    Stocks = query.IsDesending ? Stocks.OrderByDescending(stock => stock.Symbol) : Stocks.OrderBy(stock => stock.Symbol);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            Stocks = Stocks.Skip(skipNumber).Take(query.PageSize);

            return await Stocks.ToListAsync();
        }

        public async Task<Stock?> GetbyId(int id)
        {
            return await _context.stocks.Include(c => c.Comments).FirstOrDefaultAsync(stock => stock.Id == id);
        }

        public Task<Stock?> GetbySymbol(string Symbol)
        { 
             return _context.stocks.FirstOrDefaultAsync(stock => stock.Symbol == Symbol);
        }

        public async Task<Stock?> Update(int id, StockUpdateRequestDto stock)
        {

            var updateStockModel = await _context.stocks.FirstOrDefaultAsync(stock => stock.Id == id);

            if (updateStockModel == null)
            {
                return null;
            }
            updateStockModel.UpdateFromDto(stock);
            await _context.SaveChangesAsync();
            return updateStockModel;
        }
    }
}