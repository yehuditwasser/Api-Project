namespace Project.BL
{
    public interface IDonationService
    {
        public Task<int> AddNewDonation(int donorId, int giftId);

    }
}