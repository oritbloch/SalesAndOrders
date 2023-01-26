using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace SalesDataServices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesController : ControllerBase
    {
      
        private readonly ILogger<SalesController> _logger;
        private readonly IServiceHelper _service;
        public SalesController(ILogger<SalesController> logger, IServiceHelper service)
        {
            _logger = logger;
            _service = service;
        }

        [Route("GetNumOfSales")]
        [HttpGet]
        public IActionResult GetNumOfSales(string orderBy= "MaxNum", string orderDir="2")
        {
            try
            {
                var data = _service.GetNumOfSales(orderBy, orderDir);
                return Ok(data);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return null;
        }

        [Route("GetOrderTimes")]
        [HttpGet]
        public IActionResult GetOrderTimes(string beforeOrAfter="after")
        {
            try
            {
                var data = _service.GetOrderTimes(beforeOrAfter);
                return Ok(data);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return null;
        }
    }
}