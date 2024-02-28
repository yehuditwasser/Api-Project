using Project.Dal;
using Project.DTO;
using Project.Models;
using Server_Api.DTO;

namespace Project.BL
{
    public class PurchaseService: IPurchaseService
    {
        private readonly IPurchaseDal _purchaseDal;

        public PurchaseService(IPurchaseDal purchaseDal)
        {
            this._purchaseDal = purchaseDal;
        }
        public async Task<List<PurchaseDTO>> GetPurchaseByGift(int giftId)
        {
            return await _purchaseDal.GetPurchaseByGift(giftId);       
        }
        public async Task<List<PurchaseDetails>> SortPurchaceDetails()
        {
            return await _purchaseDal.SortPurchaceDetails();
        }
        public async Task<List<PurchaseDetails>> SortPurchaceDetailsByAcquired()
        {
            return await _purchaseDal.SortPurchaceDetailsByAcquired();
        }
        public async Task<List<PurchaseDTO>> GetPurchases()
        {
            return await _purchaseDal.GetPurchases();
        }
        public async Task<List<UserDTO>> GetUsersWithPurchase()
        {
            return await _purchaseDal.GetUsersWithPurchase();
        }
        public async Task<List<GiftWithPurchasesDTO>> GetGiftsWithPurchases()
        {
            return await _purchaseDal.GetGiftsWithPurchases();
        }
        public async Task<bool> Actual_purchase(string userId)
        {
            return await _purchaseDal.Actual_purchase(userId);
        }
    }
}
