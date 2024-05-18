using api.ControlLayer.Interfaces;
using api.ControlLayer.Repositories;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.ControlLayer.Controllers
{

    [ApiController]
    [Route("api/Porfolio")]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepo _stockRepo;
        private readonly IPortfolioRepo _portfolioRepo;
        public PortfolioController(UserManager<AppUser> userManager, IStockRepo stockRepo, IPortfolioRepo portfolioRepo)
        {
            _portfolioRepo = portfolioRepo;
            _stockRepo = stockRepo;
            _userManager = userManager;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUserName();

            var appUser = await _userManager.FindByNameAsync(username);
            var portfolios = await _portfolioRepo.GetUserPortoflio(appUser);
            return Ok(portfolios);
        }



        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(String Symbol)
        {
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepo.GetbySymbol(Symbol);
            if (stock == null) return BadRequest("Stock not Found");
            var userPortfolio = await _portfolioRepo.GetUserPortoflio(appUser);
            if (userPortfolio.Any(x => x.Symbol.ToLower() == Symbol.ToLower())) return BadRequest("Stock already in Portfolio");
            var portfolioModel = new Portfolio
            {
                AppUserId = appUser.Id,
                Stockid = stock.Id
            };

            await _portfolioRepo.AddPortfolio(portfolioModel);
            if (portfolioModel == null) return StatusCode(500, "Failed to add Stock to Portfolio");
            return Created();
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(String Symbol)
        {
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepo.GetbySymbol(Symbol);

            if (stock == null) return BadRequest("Stock not Found");

            var userPortfolio = await _portfolioRepo.GetUserPortoflio(appUser);

            if (!userPortfolio.Any(x => x.Symbol.ToLower() == Symbol.ToLower())) return BadRequest("Stock not in Portfolio");

            var portfolioModel = new Portfolio
            {
                AppUserId = appUser.Id,
                Stockid = stock.Id
            };

            await _portfolioRepo.DeletePortfolio(portfolioModel);

            if (portfolioModel == null) return StatusCode(500, "Failed to delete Stock from Portfolio");
            return Ok();
        }
    }
}