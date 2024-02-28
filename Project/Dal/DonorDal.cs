using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Dal
{
    public class DonorDal: IDonorDal
    {
        private readonly ProjectContext _projectContext;

        public DonorDal(ProjectContext projectContext)
        {
            this._projectContext = projectContext ?? throw new ArgumentNullException(nameof(projectContext));
        }

        public async Task<List<Donor>> GetAllDonors()
        {
            return await _projectContext.Donor
                .Include(d => d.TypeOfDonation)
                .ToListAsync(); 
        }

        public async Task<Donor> GetDonorById(int id)
        {
            Donor d = await _projectContext.Donor.Include(d => d.TypeOfDonation).FirstOrDefaultAsync(d => d.Id == id);
            if (d == null)
                return new Donor();
            return d;
        }
        public async Task<List<Gift>> GetAllGiftByDonorId(int id)
        {
            List<Gift> gifts = new List<Gift>();
            var donations = await _projectContext.Donation
                .Include(d => d.Gift) // Include the associated gift
                .ThenInclude(g => g.Category) // Include the associated category
                .Where(d => d.Donor.Id == id)
                .ToListAsync();
            // Extract the gifts from the donations
            gifts = donations.Select(d => d.Gift).ToList();
            return gifts;
        }
        public async Task<int> AddNewDonor(string name, string email, string picture, int typeOfDonation)
        {
            Donor d = new Donor();
            d.Name = name;
            d.Email = email;    
            d.Picture = picture;
            TypeOfDonation td = await _projectContext.TypeOfDonation.FindAsync(typeOfDonation);

            d.TypeOfDonation = td;

            await _projectContext.Donor.AddAsync(d);
            await _projectContext.SaveChangesAsync();
            return d.Id;
        }

        public async Task<bool> UpdateDonor(int id,string name, string email, string picture, int typeOfDonation)
        {
            Donor d = await _projectContext.Donor.FindAsync(id);
            if (d == null)  
                return false;
            d.Name = name;
            d.Email = email;
            d.Picture = picture;
            TypeOfDonation td = await _projectContext.TypeOfDonation.FindAsync(typeOfDonation);

            d.TypeOfDonation = td;

            await _projectContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteDonor(int id)
        {
            Donor d = await _projectContext.Donor.FindAsync(id);
            if (d == null)
                return false;
            _projectContext.Donor.Remove(d);
            await _projectContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Donor>> searchByName(string nameOrEmail)
        {
            List<Donor> d = new List<Donor>();
            d = await _projectContext.Donor
                .Where(d => d.Name.Contains(nameOrEmail) || d.Email.Contains(nameOrEmail))
                .ToListAsync();
            return d;
        }
        public async Task<List<Donor>> searchDonorsByGift(string name)
        {
            List<Donor> donors = new List<Donor>();
            Gift gift = await _projectContext.Gift.FirstOrDefaultAsync(g => g.Name.Contains(name));
            if (gift != null)
            {
                List<Donation> donations = await _projectContext.Donation
                    .Include(d => d.Donor)
                    .ThenInclude(d => d.TypeOfDonation)
                    .Where(d => d.Gift.Id == gift.Id)
                    .ToListAsync();

                donors = donations.Select(donation => donation.Donor).ToList();
            }
            return donors;
        }
        public async Task<List<TypeOfDonation>> getTypeOfDonation()
        {
            return await _projectContext.TypeOfDonation.ToListAsync();
        }
    }
}
