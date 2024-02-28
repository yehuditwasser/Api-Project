using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Dal
{
    public class CartDal: ICartDal
    {
        private readonly ProjectContext _projectContext;

        public CartDal(ProjectContext projectContext)
        {
            this._projectContext = projectContext;
        }
        public async Task<List<Cart>> GetCart(string userId)
        {
            List<Cart> cart = await _projectContext.Cart.Where(x => x.UserId == userId).Include(c => c.Gift).ToListAsync();
            return cart;
        }
        public async Task<int> AddToCart(int giftId,string userId)
        {
            Cart c = await _projectContext.Cart.SingleOrDefaultAsync(c => c.UserId == userId && c.GiftId == giftId);
            if(c != null)
            {
                c.Quantity++;
                await _projectContext.SaveChangesAsync();
                return c.Id;
            }
            Cart cart = new Cart();
            cart.GiftId = giftId;
            cart.UserId = userId;
            cart.Quantity = 1;
            await _projectContext.Cart.AddAsync(cart);
            await _projectContext.SaveChangesAsync();
            return cart.Id;
        }

        public async Task<bool> Reduce(int giftId, string userId)
        {
            Cart c = await _projectContext.Cart.SingleOrDefaultAsync(c => c.UserId == userId && c.GiftId == giftId);
            if(c == null)
                return false;
            if(c.Quantity==1)
            {
                _projectContext.Cart.Remove(c);
                await _projectContext.SaveChangesAsync();
                return true;
            }
            c.Quantity--;
            await _projectContext.SaveChangesAsync();

            return true;
        }
        public async Task<bool> Increas(int giftId, string userId)
        {
            Cart c = await _projectContext.Cart.SingleOrDefaultAsync(c => c.UserId == userId && c.GiftId == giftId);
            if (c == null)
                return false;
            c.Quantity= c.Quantity+1;
            await _projectContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> removeFromCart(string userId, int giftId)
        {
            Cart c = await _projectContext.Cart.SingleOrDefaultAsync(c => c.UserId == userId && c.GiftId == giftId);
            if (c != null)
            {
                _projectContext.Cart.Remove(c);
                await _projectContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}
