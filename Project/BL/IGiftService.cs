using Project.Models;

namespace Project.BL
{
    public interface IGiftService
    {
        public Task<List<Gift>> GetAllGifts();
        public Task<Gift> GetGiftById(int id);
        public Task<int> AddNewGift(string name, double cost, string picture, int category);
        public Task<bool> UpdateGift(int id, string name, double cost, string picture, string category);
        public Task<bool> DeleteGift(int id);
        public Task<List<Donor>> GetDonorsByGiftId(int id);
        public Task<List<Gift>> SearchGiftByName(string name);
        public Task<List<Gift>> SearchGiftsByDonor(string name);
        public Task<List<Gift>> GetGiftsWithNoDonorOrMoneyDonor();

        public Task<List<Gift>> SortGiftsByPrice();
        public Task<List<Gift>> SortGiftsByCategory();
        public Task<List<Category>> GetCategory();
        public Task<List<Gift>> GetGiftsWithNoDonor();
        public Task<List<Gift>> FilterGiftsByPrice(double minPrice, double maxPrice);

    }
}