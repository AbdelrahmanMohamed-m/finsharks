using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.ControlLayer.Interfaces
{
    public interface IPortfolioRepo
    { 
         Task<List<Stock>> GetUserPortoflio(AppUser User);

        Task<Portfolio> AddPortfolio(Portfolio portfolio);

        Task<Portfolio> DeletePortfolio(Portfolio portfolio);
    }
}