using JWTAuthentication.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.BL;
using Project.Models;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        private readonly IDonationService _donationService;

        public DonationController(IDonationService donationService)
        {
            this._donationService = donationService;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost("AddNewDonation")]
        public async Task<ActionResult<int>> AddNewDonation(addDonation d)
        {
            return await _donationService.AddNewDonation(d.DonorId, d.GiftId);
        }
    }
}
