using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.BL;
using Project.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            this._cartService = cartService;
        }

        [HttpGet("GetCart")]
        public async Task<ActionResult<List<Cart>>> GetCart()
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return await _cartService.GetCart(userId);
        }

        [HttpPost("AddToCart")]
        public async Task<ActionResult<int>> AddToCart([FromBody]int giftId)
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return await _cartService.AddToCart(giftId,userId);
        }

        [HttpPut("Reduce")]
        public async Task<ActionResult<bool>> Reduce([FromBody] int giftId)
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return await _cartService.Reduce(giftId,userId);
        }
        [HttpPut("Increas")]

        public async Task<ActionResult<bool>> Increas([FromBody]  int giftId)
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return await _cartService.Increas(giftId,userId);
        }

        [HttpDelete("removeFromCart/{giftId}")]
        public async Task<ActionResult<bool>> removeFromCart(int giftId)
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return await _cartService.removeFromCart(userId, giftId);
        }
    }
}
