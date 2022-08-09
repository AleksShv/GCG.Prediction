using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using GCG.Prediction.PredictionModule.Clients;
using GCG.Prediction.PredictionModule.Data;

namespace GCG.Prediction.API.Controllers
{
    [ApiController]
    [Route("prediction/")]
    public class PredictionController : Controller
    {
        private readonly IPredictionClient _client;

        public PredictionController(IPredictionClient client)
        {
            _client = client;
        }

        [HttpPost("predict")]
        public IActionResult Predict([FromBody] HistoricalData historicalData)
        {
            var prediction = _client.Predict(historicalData);
            return Ok(prediction);
        }

        [HttpPost("covert-to-input")]
        public IActionResult ConvertToInputData([FromBody] HistoricalData historicalData)
        {
            var input = _client.ConvertToInputData(historicalData);
            return Ok(input);
        }
    }
}
