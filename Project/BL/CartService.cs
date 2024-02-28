using Project.Dal;
using Project.Models;

namespace Project.BL
{
    public class CartService: ICartService
    {
        private readonly ICartDal _cartDal;

        public CartService(ICartDal cartDal)
        {
            this._cartDal = cartDal;
        }
        public async Task<List<Cart>> GetCart(string userId)
        {
           return await _cartDal.GetCart(userId);
        }
        public async Task<int> AddToCart(int giftId, string userId)
        {
            return await _cartDal.AddToCart(giftId, userId);
        }

        public async Task<bool> Reduce(int giftId, string userId)
        {
            return await _cartDal.Reduce(giftId, userId);
        }

        public async Task<bool> Increas(int giftId, string userId)
        {
            return await _cartDal.Increas(giftId, userId);
        }

        public async Task<bool> removeFromCart(string userId, int giftId)
        {
            return await _cartDal.removeFromCart(userId, giftId);
        }
    }
}
