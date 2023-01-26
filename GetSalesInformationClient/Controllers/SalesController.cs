using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

using MessageService;
namespace GetSalesInformationClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;
        private const string getOrderTimesMethodName = "getordertimes";
        private const string getProductSalesMethodName = "getnumofsales";
        //private readonly TimerHub _timerService;

        public SalesController(IConfiguration configuration)//, TimerHub timerService)
        {
            _client = new HttpClient();
            _config = configuration;
            //_timerService = timerService;
        }
        [Route("GetProductSales")]
        [HttpGet]
        public async Task<IActionResult> GetProductSales(string orderBy= "maxnum", string orderDir="desc")
        {
            try
            {

                string url = _config.GetValue<string>("APIUrl") + getProductSalesMethodName;
                var response = await _client.GetAsync(url+"?orderby="+ orderBy+"&orderdir="+ orderDir);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            catch(Exception ex)
            {
                //writeLog
                return null;
            }
        }
        [Route("GetOrderTimes")]
        [HttpGet]
        public async Task<IActionResult> GetOrderTimes(string beforeOrAfter = "after")
        {
            try
            {
                string url = _config.GetValue<string>("APIUrl") + getOrderTimesMethodName;
                var response = await _client.GetAsync(url+ "?beforeOrAfter="+ beforeOrAfter);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            catch(Exception ex)
            {
                //writeLog
                return null;
            }
        }

    }
}