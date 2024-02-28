using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.BL;
using Project.DTO;
using Project.Models;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WinnerController : ControllerBase
    {
        private readonly IWinnerService _winnerService;

        public WinnerController(IWinnerService winnerService)
        {
            this._winnerService = winnerService;
        }

        [HttpPost("Raffle")]
        public async Task<ActionResult<bool>> Raffle([FromBody] int giftId )
        {
            return await _winnerService.Raffle(giftId);
        }

        [HttpGet("GetWinners")]
        public async Task<ActionResult<List<WinnerDTO>>> GetWinners()
        {
            return await _winnerService.GetWinners();
        }
        [HttpPost("SendEmail")]
        public void SendEmail([FromBody] string emailTo)
        {
            _winnerService.SendEmail(emailTo);   
        }
        [HttpGet("CalculateGiftRevenue")]
        public ActionResult<Dictionary<string, int>> CalculateGiftRevenue()
        {
            return _winnerService.CalculateGiftRevenue();
        }

    }
}
