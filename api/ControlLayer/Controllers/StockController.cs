using api.Data;
using api.Dtos.StockDto;
using api.Interfaces;
using api.Mappers.Stockmapper;
using api.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
        private readonly IStockRepo _StockRepo;
        public StockController(IStockRepo stockRepo)
        { _StockRepo = stockRepo; }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Getall([FromQuery] QueryObject query)
        {
            var stocks = await _StockRepo.Getall(query);
            var stockDto = stocks.Select(s => s.ToStockDto()).ToList();
            return Ok(stockDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetId([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var StockByid = await _StockRepo.GetbyId(id);

            return StockByid != null ? Ok(StockByid.ToStockDto()) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StockCreateRequestDto stockCreateRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            /// Map StockCreateRequestDto to Stock because we need to save
            ///  it in the database and add function need a Stock object not a StockCreateRequestDto
            var stock = stockCreateRequestDto.FromStockRequestToStockModel();
            //used to return that the object has been created
            await _StockRepo.Create(stock);
            return CreatedAtAction(nameof(GetId), new { id = stock.Id }, stock.ToStockDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] StockUpdateRequestDto UpdateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var stock = await _StockRepo.Update(id, UpdateDto);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var stock = await _StockRepo.Delete(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }
    }
}