using Project.DTO;
using Project.Models;

namespace Project.BL
{
    public interface IWinnerService
    {
        public Task<bool> Raffle(int giftId);
        public Task<List<WinnerDTO>> GetWinners();
        public void SendEmail(string emailTo);
        public Dictionary<string, int> CalculateGiftRevenue();

    }
}