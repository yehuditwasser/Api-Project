using JWTAuthentication.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.BL;
using Project.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController : ControllerBase
    {
        private readonly IGiftService _giftService;

        public GiftController(IGiftService giftService)
        {
            this._giftService = giftService;
        }

        // GET: api/<GiftController>
        [HttpGet("GetAllGifts")]
        public async Task<ActionResult<List<Gift>>> GetAllGifts()
        {
            return await _giftService.GetAllGifts();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("GetGiftById/{id}")]
        public async Task<ActionResult<Gift>> GetGiftById(int id)
        {
            return await _giftService.GetGiftById(id);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("GetDonorsByGiftId/{id}")]
        public async Task<ActionResult<List<Donor>>> GetDonorsByGiftId(int id)
        {
            return await _giftService.GetDonorsByGiftId(id);
        }
        [HttpGet("SearchGiftByName/{name}")]
        public async Task<ActionResult<List<Gift>>> SearchGiftByName(string name)
        {
            return await _giftService.SearchGiftByName(name);
        }
        [HttpGet("SearchGiftsByDonor/{name}")]
        public async Task<ActionResult<List<Gift>>> SearchGiftsByDonor(string name)
        {
            return await _giftService.SearchGiftsByDonor(name);
        }

        [Authorize(Roles = UserRoles.Admin)]

        // POST api/<GiftController>
        [HttpPost("AddNewGift")]
        public async Task<ActionResult<int>> AddNewGift(Gift g)
        {
            return await _giftService.AddNewGift(g.Name, g.Cost, g.Picture, g.Category.Id);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("UpdateGift")]
        public async Task<ActionResult<bool>> UpdateGift(Gift g)
        {
            return await _giftService.UpdateGift(g.Id, g.Name, g.Cost, g.Picture, g.Category.Name);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("DeleteGift/{id}")]
        public async Task<ActionResult<bool>> DeleteGift(int id)
        {
            return await _giftService.DeleteGift(id);
        }

        [Authorize(Roles = UserRoles.Admin)]

        [HttpGet("GetGiftsWithNoDonorOrMoneyDonor")]
        public async Task<ActionResult<List<Gift>>> GetGiftsWithNoDonorOrMoneyDonor()
        {
            return await _giftService.GetGiftsWithNoDonorOrMoneyDonor();
        }
        [Authorize(Roles = UserRoles.Admin)]

        [HttpGet("GetGiftsWithNoDonor")]
        public async Task<ActionResult<List<Gift>>> GetGiftsWithNoDonor()
        {
            return await _giftService.GetGiftsWithNoDonor();
        }
        [HttpGet("SortGiftsByPrice")]
        public async Task<ActionResult<List<Gift>>> SortGiftsByPrice()
        {
            return await _giftService.SortGiftsByPrice();
        }
        [HttpGet("SortGiftsByCategory")]
        public async Task<ActionResult<List<Gift>>> SortGiftsByCategory()
        {
            return await _giftService.SortGiftsByCategory();
        }
        [Authorize]
        [HttpGet("GetCategory")]
        public async Task<ActionResult<List<Category>>> GetCategory()
        {
            return await _giftService.GetCategory();
        }
        [HttpGet("FilterGiftsByPrice")]
        public async Task<ActionResult<List<Gift>>> FilterGiftsByPrice([FromQuery] double minPrice, [FromQuery] double maxPrice)
        {
            return await _giftService.FilterGiftsByPrice(minPrice, maxPrice);
        }
    }
}
