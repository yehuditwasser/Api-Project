using Project.Dal;

namespace Project.BL
{
    public class DonationService: IDonationService
    {
        private readonly IDonationDal _donationDal;

        public DonationService(IDonationDal donationDal)
        {
            this._donationDal = donationDal;
        }

        public async Task<int> AddNewDonation(int donorId, int giftId)
        {
            return await _donationDal.AddNewDonation(donorId, giftId);
        }
    }
}
