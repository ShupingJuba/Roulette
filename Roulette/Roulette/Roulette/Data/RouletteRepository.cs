using Microsoft.EntityFrameworkCore;
using Roulette.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roulette.Data
{
    public class RouletteRepository :IRouletteRepository
    {
        private readonly DataContext _context;
        private readonly List<SpinResult> _spinHistory;

        public RouletteRepository(DataContext context)
        {
            _context = context;
            _spinHistory = new List<SpinResult>();
        }



        public  List<SpinResult> GetPreviousSpins()
        {
            return _spinHistory;
        }

        private double CalculatePayoutAmount(int betNumber, int spinNumber, double betAmount)
        {
            if (betNumber == spinNumber)
            {
                // Payout for winning number bet
                return betAmount * 36; // Payout is 36 times the bet amount for a winning number bet
            }
            else if (betNumber == 0 && spinNumber == 0)
            {
                // Payout for a bet on 0 (single zero) if spin lands on 0
                return betAmount * 36; // Payout is 36 times the bet amount for a bet on 0
            }
            else if (IsEvenOddBet(betNumber) && IsEvenOddBet(spinNumber))
            {
                // Payout for even/odd bet if spin lands on the same even/odd
                return betAmount * 2; // Payout is 2 times the bet amount for an even/odd bet
            }
            else if (IsColorBet(betNumber) && IsColorBet(spinNumber))
            {
                // Payout for color bet if spin lands on the same color
                return betAmount * 2; // Payout is 2 times the bet amount for a color bet
            }
            else
            {
                // No payout for losing bets
                return 0;
            }
        }

        private bool IsEvenOddBet(int number)
        {
            // Determine if the number corresponds to an even/odd bet
            return number % 2 == 0; // Returns true for even bets, false for odd bets
        }

        private bool IsColorBet(int number)
        {
            // Determine if the number corresponds to a red/black bet
            return number != 0; // Returns true for red/black bets (excluding 0), false otherwise
        }


      

        public  bool Payout(int spinNo)
        {
            var spin =  _spinHistory.FirstOrDefault(s => s.Number == spinNo);
            var result = false;
            if (spin != null)
            {
                foreach (var bet in spin.WinningBets)
                {
                    // Calculate the payout amount based on the bet type and spin result
                    double payoutAmount = CalculatePayoutAmount(bet.Number, spinNo, bet.Amount);

                   
                }

                result = true;
            }

            return result;
        }

        public async Task<int> PlaceBet(Bet b)
        {
            _context.Bets.Add(b);
            await _context.SaveChangesAsync();
            return b.Id;
        }

    

       public async Task<SpinResult> Spin()
        {
            var random = new Random();
            var number = random.Next(0, 37);
            var color = (number == 0 || (number >= 1 && number <= 10) || (number >= 19 && number <= 28)) ? "Red" : "Black";

            var winningBets = await _context.Bets.Where(x => x.Number == number).ToListAsync();

            var spinResult = new SpinResult
            {
                Number = number,
                Color = color,
                WinningBets = winningBets
            };

            _spinHistory.Add(spinResult);

            return spinResult;
        }
    }
}
