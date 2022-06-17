using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Unico.Core.API.Models;
using Unico.Core.API.ServicesInterface;

namespace Unico.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly IMarketServices _marketServices;

        public OperationsController(IMarketServices marketServices)
        {
            _marketServices = marketServices;
        }
        [HttpOptions("{filename}", Name = "InsertCsv")]
        public async Task<IActionResult> InsertCsv(string filename)
        {
            try
            {
                List<MarketCsv> markets = new List<MarketCsv>();

                #region Read CSV
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    MissingFieldFound = null
                };

                var path = $"{Directory.GetCurrentDirectory()}{@"\files"}" + "\\" + filename;
                using (var reader = new StreamReader(path))
                    using(var csv = new CsvReader(reader, config))
                {

                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        var market = csv.GetRecord<MarketCsv>();
                        markets.Add(market);
                    }
                }
                #endregion

                var result = await _marketServices.UploadCsvToCreateMarkets(markets);

                if(result.IsSuccess)
                    return Ok(result.marketResponse);

                return BadRequest(result.MsgError);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
