using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Net;
using System.Threading.Tasks;
using Unico.Core.API.Models;
using Unico.Core.API.ServicesInterface;

namespace Unico.Core.API.Controllers
{

    /// <summary>
    /// Principal controller to application
    /// Have two injection depedence
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MarketController : ControllerBase
    {
        private readonly IMarketServices _marketService;

        /// <summary>
        /// Have one injection depedence
        /// </summary>
        /// <param name="marketService"></param>
        public MarketController(IMarketServices marketService)
        {
            _marketService = marketService;
        }
        /// <summary>
        /// Return all Markets.
        /// </summary>
        /// <returns>Items in Market List</returns>
        /// <response code="200">Return all Markets</response>
        [HttpGet]
        public async Task<IActionResult> GetAllMarkets()
        {
            try
            {
                var response = await _marketService.GetMarketsAsync();

                return Ok(response.markets);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
            }
        }
        /// <summary>
        /// Return some Markets by Name.
        /// </summary>
        /// <returns>Items in Market List</returns>
        /// <param name="name">Market name</param>
        /// <response code="200">Return some markets</response>
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
        /// <summary>
        /// Add a new Market to list.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST / Market
        ///    {
        ///    "id": 1,
        ///    "long": "-46550164",
        ///    "lat": "-23558733",
        ///    "setcens": 355030885000091,
        ///    "areap": 3550308005040,
        ///    "coddist": 87,
        ///    "distrito": "VILA FORMOSA",
        ///    "codsubpref": 26,
        ///    "subprefe": "ARICANDUVA-FORMOSA-CARRAO",
        ///    "regiaO5": "Leste",
        ///    "regiaO8": "Leste 1",
        ///    "nomE_FEIRA": "VILA FORMOSA",
        ///    "registro": "4041-0",
        ///    "logradouro": "RUA MARAGOJIPE",
        ///    "numero": "S/N",
        ///   "bairro": "VL FORMOSA",
        ///    "referencia": "TV RUA PRETORIA"
        ///    }
        ///
        /// </remarks>
        /// <returns>Items Createdt</returns>
        /// <response code="201">Return the item created</response>
        [HttpPost]
        public async Task<IActionResult> CreateMarket(MarketRequest request)
        {
            try
            {
                var response = await _marketService.CreateMarketAsync(request);

                if (response.IsSuccess)
                {
                    var actionName = nameof(GetMarketByName);
                    var routeValues = new { name = response.markets.NOME_FEIRA };
                    return CreatedAtAction(actionName, routeValues, response.markets);

                }
                return BadRequest();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
            }
        }
        /// <summary>
        /// Edit Market by Param ID.
        /// </summary>
        /// <returns>Market Updated</returns>
        /// <param name="id">Market Identifie</param>
        /// <param name="request">Market Obcjet</param>
        /// <response code="200">Return Market Updated</response>
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
        /// <summary>
        /// Delete Market by Param ID.
        /// </summary>
        /// <returns>Nothing</returns>
        /// <param name="id">Market Identifie</param>
        /// <response code="204">Not return values</response>
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
