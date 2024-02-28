using Project.Models;

namespace Project.BL
{
    public interface ICartService
    {
        public Task<List<Cart>> GetCart(string userId);
        public Task<int> AddToCart(int giftId,string userId);
        public Task<bool> Reduce(int giftId, string userId);
        public Task<bool> Increas(int giftId, string userId);
        public Task<bool> removeFromCart(string userId, int giftId);

    }
}