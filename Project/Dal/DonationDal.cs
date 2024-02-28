using Project.Models;

namespace Project.Dal
{
    public class DonationDal: IDonationDal
    {
        private readonly ProjectContext _projectContext;

        public DonationDal(ProjectContext projectContext)
        {
            this._projectContext = projectContext;
        }

        public async Task<int> AddNewDonation(int donorId, int giftId)
        {

            Donor donor = await _projectContext.Donor.FindAsync(donorId);
            Gift gift = await _projectContext.Gift.FindAsync(giftId);

            Donation newDonation = new Donation
            {
                Donor = donor,
                Gift = gift
            };

            await _projectContext.Donation.AddAsync(newDonation);
            await _projectContext.SaveChangesAsync();
            return newDonation.Id;
        }
    }
}
