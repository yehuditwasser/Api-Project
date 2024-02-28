using Project.Dal;
using Project.Models;

namespace Project.BL
{
    public class DonorService: IDonorService
    {
        private readonly IDonorDal _donorDal;

        public DonorService(IDonorDal donorDal)
        {
            this._donorDal = donorDal ?? throw new ArgumentNullException(nameof(donorDal));
        }

        public async Task<List<Donor>> GetAllDonors()
        {
            return await _donorDal.GetAllDonors();
        }
        public async Task<int> AddNewDonor(string name, string email, string picture, int typeOfDonation)
        {
            return await _donorDal.AddNewDonor(name, email, picture,typeOfDonation);
        }
        public async Task<bool> UpdateDonor(int id, string name, string email, string picture, int typeOfDonation)
        {
            return await _donorDal.UpdateDonor(id, name, email, picture, typeOfDonation);
        }
        public async Task<bool> DeleteDonor(int id)
        {
            return await _donorDal.DeleteDonor(id);
        }
        public async Task<Donor> GetDonorById(int id)
        {
            return await _donorDal.GetDonorById(id);   
        }
        public async Task<List<Gift>> GetAllGiftByDonorId(int id)
        {
            return await _donorDal.GetAllGiftByDonorId(id);
        }
        public async Task<List<Donor>> searchByName(string nameOrEmail)
        {
            return await _donorDal.searchByName(nameOrEmail);
        }
        public async Task<List<Donor>> searchDonorsByGift(string name)
        {
            return await _donorDal.searchDonorsByGift(name);
        }
        public async Task<List<TypeOfDonation>> getTypeOfDonation()
        {
            return await _donorDal.getTypeOfDonation();
        }




    }
}
