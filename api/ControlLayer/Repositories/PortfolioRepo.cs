using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.ControlLayer.Interfaces;
using api.Data;
using api.DataLayer.Dtos.CommentDto;
using api.Dtos.StockDto;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.ControlLayer.Repositories
{
    public class PortfolioRepo : IPortfolioRepo
    {

        private readonly ApplicationDBContext _context;
        public PortfolioRepo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<StockDto>> GetUserPortoflio(AppUser User)
        {
            return await _context.Portfolios.Where(x => x.AppUserId == User.Id).Select(stock => new StockDto
            {
                Id = stock.Stock.Id,
                Symbol = stock.Stock.Symbol,
                LastDivdend = stock.Stock.LastDivdend,
                Industry = stock.Stock.Industry,
                Marektcap = stock.Stock.Marektcap,
                CompanyName = stock.Stock.CompanyName,
                Purshase = stock.Stock.Purshase, 

                Comments = stock.Stock.Comments.Select(comment => new CommentDto
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    StockId = comment.StockId,
                    CreatedBy = comment.AppUser.UserName,
                }).ToList()
            }).ToListAsync();
        }

        public async Task<Portfolio> AddPortfolio(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<Portfolio> DeletePortfolio(Portfolio portfolio)
        {
            _context.Portfolios.Remove(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }
    }
}