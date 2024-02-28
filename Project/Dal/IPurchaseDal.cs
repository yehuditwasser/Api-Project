using Project.DTO;
using Project.Models;
using Server_Api.DTO;

namespace Project.Dal
{
    public interface IPurchaseDal
    {
        public Task<List<PurchaseDTO>> GetPurchaseByGift(int giftId);
        public Task<List<PurchaseDetails>> SortPurchaceDetails();
        public Task<List<PurchaseDetails>> SortPurchaceDetailsByAcquired();
        public Task<List<PurchaseDTO>> GetPurchases();
        public Task<List<UserDTO>> GetUsersWithPurchase();
        public Task<List<GiftWithPurchasesDTO>> GetGiftsWithPurchases();
        public Task<bool> Actual_purchase(string userId);

    }
}