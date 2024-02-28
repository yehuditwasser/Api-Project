using JWTAuthentication.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.BL;
using Project.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class DonorController : ControllerBase
    {
        private readonly IDonorService _donorService;

        public DonorController(IDonorService donorService)
        {
            this._donorService = donorService;
        }
        // GET: api/<DonorController>
        [HttpGet("GetAllDonors")]
        public async Task<ActionResult<List<Donor>>> Get()
        {
            return await _donorService.GetAllDonors();
        }

        // GET api/<DonorController>/5
        [HttpGet("GetDonorById/{id}")]
        public async Task<ActionResult<Donor>> Get(int id)
        {
            return await _donorService.GetDonorById(id);
        }

        [HttpGet("searchByName/{nameOrEmail}")]
        public async Task<ActionResult<List<Donor>>> SearchByName(string nameOrEmail)
        {
            return await _donorService.searchByName(nameOrEmail);
        }

        [HttpGet("searchDonorsByGift/{name}")]
        public async Task<ActionResult<List<Donor>>> SearchDonorsByGift(string name)
        {
            return await _donorService.searchDonorsByGift(name);
        }

        [HttpGet("GetAllGiftByDonorId/{id}")]
        public async Task<ActionResult<List<Gift>>> GetAllGiftByDonorId(int id)
        {
            return await _donorService.GetAllGiftByDonorId(id);
        }

        // POST api/<DonorController>
        [HttpPost("addNewDonor")]
        public async Task<ActionResult<int>> Post(Donor d)
        {
           return await _donorService.AddNewDonor(d.Name, d.Email, d.Picture, d.TypeOfDonation.Id);
        }

        // PUT api/<DonorController>/5
        //[HttpPut("UpdateDonor/{id}")]
        //public async Task<ActionResult<bool>> Put(int id, string name, string email, string picture, int typeOfDonation)
        //{
        //    return await _donorService.UpdateDonor(id, name, email, picture, typeOfDonation);
        //}
        [HttpPut("UpdateDonor")]
        public async Task<ActionResult<bool>> Put(Donor donor)
        {
            return await _donorService.UpdateDonor(donor.Id, donor.Name, donor.Email, donor.Picture, donor.TypeOfDonation.Id);
        }

        // DELETE api/<DonorController>/5
        [HttpDelete("DeleteDonor/{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _donorService.DeleteDonor(id);
        }
        [HttpGet("getTypeOfDonation")]
        public async Task<ActionResult<List<TypeOfDonation>>> getTypeOfDonation()
        {
            return await _donorService.getTypeOfDonation();
        }
    }
}
