using AutoMapper;
using JWTAuthentication.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.BL;
using Project.DTO;
using Project.Models;
using Server_Api.DTO;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IMapper _map;

        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            this._purchaseService = purchaseService;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("GetPurchaseByGift")]
        public async Task<ActionResult<List<PurchaseDTO>>> GetPurchaseByGift(int giftId)
        {
            return await _purchaseService.GetPurchaseByGift(giftId);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("SortPurchaceDetails")]
        public async Task<ActionResult<List<PurchaseDetails>>> SortPurchaceDetails()
        {
            return await _purchaseService.SortPurchaceDetails();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("SortPurchaceDetailsByAcquired")]
        public async Task<ActionResult<List<PurchaseDetails>>> SortPurchaceDetailsByAcquired()
        {
            return await _purchaseService.SortPurchaceDetailsByAcquired();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("GetPurchases")]
        public async Task<ActionResult<List<PurchaseDTO>>> GetPurchases()
        {
            return await _purchaseService.GetPurchases();
        }

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpGet("GetUsersWithPurchase")]
        public async Task<ActionResult<List<UserDTO>>> GetUsersWithPurchase()
        {
            return await _purchaseService.GetUsersWithPurchase();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("GetGiftsWithPurchases")]
        public async Task<ActionResult<List<GiftWithPurchasesDTO>>> GetGiftsWithPurchases()
        {
            return await _purchaseService.GetGiftsWithPurchases();
        }

        [HttpPost("Actual_purchase")]
        public async Task<ActionResult<bool>> Actual_purchase()
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return await _purchaseService.Actual_purchase(userId);
        }

    }
}
