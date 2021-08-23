using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TreasuryYields.Models.Entities;
using TreasuryYields.Services.Interfaces;

namespace TreasuryYields.API.Controllers
{
    [ApiController]
    [Route("api/yields")]
    public class TreasuryYieldsController : ControllerBase
    {
        private readonly ILogger<TreasuryYieldsController> _logger;
        private readonly ITreasuryYieldsService _tys;

        public TreasuryYieldsController(ILogger<TreasuryYieldsController> logger, ITreasuryYieldsService tys)
        {
            _logger = logger;
            _tys = tys;
        }

        [HttpGet]
        [Route("{date}", Name = "GetYieldByDate")]
        public IActionResult GetYieldByDate(String date, [FromQuery] String format = "dd-MM-yy")
        {
            var model = _tys.GetTreasuryYieldsDayByDate(date, format);
            return Ok(model);
        }
    }
}
