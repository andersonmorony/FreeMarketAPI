using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;
using Unico.Core.API.Models;
using Unico.Core.API.ServicesInterface;

namespace Unico.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketController : ControllerBase
    {
        private readonly IMarketServices _marketService;
        private readonly LinkGenerator _linkGenerator;

        public MarketController(IMarketServices marketService, LinkGenerator linkGenerator)
        {
            _marketService = marketService;
            _linkGenerator = linkGenerator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMarkets()
        {
            try
            {
                var response = await _marketService.GetMarketsAsync();

                if (response.IsSuccess)
                    return Ok(response.markets);

                return NotFound(response.MsgError);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
            }
        }
        [HttpGet("{name}")]
        public async Task<IActionResult> GetMarketByName(string name)
        {
            try
            {
                var response = await _marketService.GetMarketsByNameAync(name);

                if (response.IsSuccess)
                    return Ok(response.marketResponse);

                return NotFound(response.MsgError);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateMarket(MarketRequest request)
        {
            try
            {
                var response = await _marketService.CreateMarketAsync(request);

                if (response.IsSuccess)
                {

                    var linkGenaretor = _linkGenerator.GetPathByAction("GetMarketByName", "Market", new { name = response.markets.NOME_FEIRA });

                    return Created(linkGenaretor, response.markets);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<MarketResponse>> EditMarket(int id, MarketRequest request)
        {
            try
            {
                var response = await _marketService.EditMarketAsync(id, request);

                if (response.IsSuccess)
                    return response.marketResponse;

                return BadRequest(response.MsgError);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarket(int id)
        {
            try
            {
                var response = await _marketService.DeleteMarketAsync(id);

                if (response.IsSuccess)
                    return NoContent();

                return BadRequest(response.MsgError);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
            }
        }
    }
}
