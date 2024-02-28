using Project.Dal;
using Project.Models;

namespace Project.BL
{
    public class GiftService: IGiftService
    {
        private readonly IGiftDal _giftDal;

        public GiftService(IGiftDal giftDal)
        {
            this._giftDal = giftDal;
        }

        public async Task<List<Gift>> GetAllGifts()
        {
            return await _giftDal.GetAllGifts();
        }

        public async Task<Gift> GetGiftById(int id)
        {
            return await _giftDal.GetGiftById(id);
        }

        public async Task<int> AddNewGift(string name, double cost, string picture, int category)
        {
            return await _giftDal.AddNewGift(name, cost, picture, category);
        }
        public async Task<bool> UpdateGift(int id, string name, double cost, string picture, string category)
        {
            return await _giftDal.UpdateGift(id, name, cost, picture, category);
        }
        public async Task<bool> DeleteGift(int id)
        {
            return await _giftDal.DeleteGift(id);
        }
        public async Task<List<Donor>> GetDonorsByGiftId(int id)
        {
            return await _giftDal.GetDonorsByGiftId(id);
        }
        public async Task<List<Gift>> SearchGiftByName(string name)
        {
            return await _giftDal.SearchGiftByName(name);
        }

        public async Task<List<Gift>> SearchGiftsByDonor(string name)
        {
            return await _giftDal.SearchGiftsByDonor(name);
        }
        public async Task<List<Gift>> GetGiftsWithNoDonorOrMoneyDonor()
        {
            return await _giftDal.GetGiftsWithNoDonorOrMoneyDonor();
        }
        public async Task<List<Gift>> SortGiftsByPrice()
        {
            return await _giftDal.SortGiftsByPrice(); 
        }

        public async Task<List<Gift>> SortGiftsByCategory()
        {
            return await _giftDal.SortGiftsByCategory();
        }
        public async Task<List<Category>> GetCategory()
        {
            return await _giftDal.GetCategory();
        }
        public async Task<List<Gift>> GetGiftsWithNoDonor()
        {
            return await _giftDal.GetGiftsWithNoDonor();
        }
        public async Task<List<Gift>> FilterGiftsByPrice(double minPrice, double maxPrice)
        {
            return await _giftDal.FilterGiftsByPrice(minPrice, maxPrice);
        }
    }
}
