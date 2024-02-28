using Project.Models;

namespace Project.Dal
{
    public interface IDonorDal
    {
        public Task<List<Donor>> GetAllDonors();
        public Task<Donor> GetDonorById(int id);
        public Task<int> AddNewDonor(string name, string email, string picture, int typeOfDonation);
        public Task<bool> UpdateDonor(int id, string name, string email, string picture, int typeOfDonation);
        public Task<bool> DeleteDonor(int id);
        public Task<List<Gift>> GetAllGiftByDonorId(int id);
        public Task<List<Donor>> searchByName(string nameOrEmail);
        public Task<List<Donor>> searchDonorsByGift(string name);
        public Task<List<TypeOfDonation>> getTypeOfDonation();


    }
}