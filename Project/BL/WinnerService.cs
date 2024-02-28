using Project.Dal;
using Project.DTO;
using Project.Models;

namespace Project.BL
{
    public class WinnerService : IWinnerService
    {
        private readonly IWinnerDal _winnerdal;

        public WinnerService(IWinnerDal winnerdal)
        {
            this._winnerdal = winnerdal;
        }
        public async Task<bool> Raffle(int giftId)
        {
            return await _winnerdal.Raffle(giftId);
        }
        public async Task<List<WinnerDTO>> GetWinners()
        {
            return await _winnerdal.GetWinners();
        }
        public void SendEmail(string emailTo)
        {
            _winnerdal.SendEmail(emailTo);
        }
        public Dictionary<string, int> CalculateGiftRevenue()
        {
            return _winnerdal.CalculateGiftRevenue();
        }
    }
}
