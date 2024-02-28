using JWTAuthentication.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.DTO;
using Project.Models;
using Server_Api.DTO;

namespace Project.Dal
{
    public class PurchaseDal: IPurchaseDal
    {
        private readonly ProjectContext _projectContext;
        private readonly UserManager<ApplicationUser> _users;

        public PurchaseDal(ProjectContext projectContext, UserManager<ApplicationUser> users)
        {
            this._projectContext = projectContext;
            this._users = users;
        }
        public async Task<List<PurchaseDTO>> GetPurchaseByGift(int giftId)
        {
            var purchases = await _projectContext.Purchase
                .Include(p => p.PurchaseDetails)
                .ThenInclude(pd => pd.Gift)
                .Where(p => p.PurchaseDetails.Any(pd => pd.GiftId == giftId))
                .ToListAsync();

            var purchaseDTOs = purchases.Select(p => new PurchaseDTO
            {
                Id = p.Id,
                UserId = p.UserId,
                Date = p.Date,
                PurchaseDetails = p.PurchaseDetails.Select(pd => new PurchaseDetailsDTO
                {
                    Id = pd.Id,
                    GiftId = pd.GiftId,
                    Quantity = pd.Quantity,
                    Gift = pd.Gift
                }).ToList()
            }).ToList();

            return purchaseDTOs;
        }
        public async Task<List<PurchaseDetails>> SortPurchaceDetails()
        {
            List<PurchaseDetails> sortedPurchaseDetails = await _projectContext.PurchaseDetails
                .OrderBy(purchaseDetail => purchaseDetail.Gift.Cost)
                .Include(p => p.Gift)
                .ToListAsync();

            return sortedPurchaseDetails;


        }
        public async Task<List<PurchaseDetails>> SortPurchaceDetailsByAcquired()
        {
            var sortedPurchaseDetails = _projectContext.PurchaseDetails
                .OrderByDescending(pd => _projectContext.PurchaseDetails
                    .Where(innerPd => innerPd.GiftId == pd.GiftId)
                    .Sum(innerPd => innerPd.Quantity))
                .ToList();
            return sortedPurchaseDetails;
        }
        public async Task<List<PurchaseDTO>> GetPurchases()
        {
            var data = await _projectContext.Purchase.Include(p => p.PurchaseDetails).ThenInclude(p => p.Gift).ThenInclude(p => p.Category).ToListAsync();

            var purchaseResponseDTOs = data.Select(p => new PurchaseDTO
            {
                Id = p.Id,
                UserId = p.UserId,
                Date = p.Date,
                PurchaseDetails = p.PurchaseDetails.Select(pd => new PurchaseDetailsDTO
                {
                    Id = pd.Id,
                    GiftId = pd.GiftId,
                    Quantity = pd.Quantity,
                    Gift = pd.Gift
                }).ToList()
            }).ToList();

            return purchaseResponseDTOs;
        }

        public async Task<List<UserDTO>> GetUsersWithPurchase()
        {
            var usersWithPurchase = await _projectContext.Purchase
                .Include(p => p.User) // Eager loading the User entity
                .Where(p => p.PurchaseDetails.Any())
                .Select(p => new UserDTO
                {
                    Name = p.User.UserName,
                    Email = p.User.Email
                })
                .ToListAsync();

            return usersWithPurchase;
        }
        public async Task<List<GiftWithPurchasesDTO>> GetGiftsWithPurchases()
        {
            var gifts = await _projectContext.Gift.ToListAsync();
            var giftDTOs = new List<GiftWithPurchasesDTO>();

            foreach (var gift in gifts)
            {
                var purchaseDetails = await _projectContext.PurchaseDetails
                    .Include(pd => pd.Purchase)
                    .Where(pd => pd.GiftId == gift.Id)
                    .ToListAsync();

                var purchases = purchaseDetails.Select(pd => new PurchaseDetailsDTO
                {
                    Id = pd.Id,
                    GiftId = pd.GiftId,
                    Quantity = pd.Quantity,
                    Gift = pd.Gift,
                    Date = pd.Purchase.Date // Include the Date property from the Purchase entity
                }).ToList();

                var giftDTO = new GiftWithPurchasesDTO
                {
                    Id = gift.Id,
                    Name = gift.Name,
                    Cost = (int)gift.Cost,
                    Picture = gift.Picture,
                    Category = gift.Category,
                    Purchases = purchases
                };

                giftDTOs.Add(giftDTO);
            }

            return giftDTOs;
        }
        public async Task<bool> Actual_purchase(string userId)
        {
            try
            {
                // Find the cart for the specified user
                var cart = await _projectContext.Cart
                    .Where(c => c.UserId == userId)
                    .ToListAsync();
                // Create a new instance of the Purchase object
                var purchase = new Purchase
                {
                    UserId = userId,
                    Date = DateTime.Now,
                    PurchaseDetails = cart.Select(item => new PurchaseDetails
                    {
                        GiftId = item.GiftId,
                        Quantity = item.Quantity
                    }).ToList()
                };

                // Add the purchase to the project context
                _projectContext.Purchase.Add(purchase);

                _projectContext.Cart.RemoveRange(cart);
                // Save the changes to the database
                await _projectContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
