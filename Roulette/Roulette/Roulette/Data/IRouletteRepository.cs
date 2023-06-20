using Roulette.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Roulette.Data
{
    public interface IRouletteRepository
    {
        Task<int> PlaceBet(Bet b);
        Task<SpinResult> Spin();
        bool Payout(int spinNo);
        List<SpinResult> GetPreviousSpins();
    }
}
