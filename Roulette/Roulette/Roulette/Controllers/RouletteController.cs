using Microsoft.AspNetCore.Mvc;
using Roulette.Data;
using Roulette.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roulette.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouletteController : ControllerBase
    {
        private readonly IRouletteRepository _context;

        public RouletteController(IRouletteRepository context)
        {
            _context = context;
        }

        [HttpPost("PlaceBet")]
        public async Task<int> PlaceBet(Bet b)
        {
            return await _context.PlaceBet(b);
        }

        [HttpGet("Spin")]
        public async Task<SpinResult> Spin()
        {
            return await _context.Spin();
        }

        [HttpGet("Payout")]
        public async Task<bool> Payout(int spinNo)
        {
            return  _context.Payout(spinNo);
        }


        [HttpGet("PreviousSpins")]
        public async Task<List<SpinResult>> GetPreviousSpins()
        {
            return _context.GetPreviousSpins();
        }




    }
}
