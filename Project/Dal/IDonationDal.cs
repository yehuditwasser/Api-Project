namespace Project.Dal
{
    public interface IDonationDal
    {
        public Task<int> AddNewDonation(int donorId, int giftId);

    }
}