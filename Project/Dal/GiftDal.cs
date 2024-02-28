using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Dal
{
    public class GiftDal : IGiftDal
    {
        private readonly ProjectContext _projectContext;

        public GiftDal(ProjectContext projectContext)
        {
            this._projectContext = projectContext;
        }

        public async Task<List<Gift>> GetAllGifts()
        {
            return await _projectContext.Gift
                .Include(g => g.Category)
                .ToListAsync();
        }
        public async Task<Gift> GetGiftById(int id)
        {
            return await _projectContext.Gift
                .Include(g => g.Category)
                .FirstOrDefaultAsync(g => g.Id == id);
        }
 
        public async Task<int> AddNewGift(string name, double cost, string picture, int category)
        {
            Category c = await _projectContext.Category.FindAsync(category);
            if (c == null)
                return 0;
            Gift g = new Gift();
            g.Name = name;
            g.Cost = cost;
            g.Picture = picture;
            g.Category = c;

            await _projectContext.Gift.AddAsync(g);
            await _projectContext.SaveChangesAsync();
            return g.Id;
        }
        public async Task<bool> UpdateGift(int id, string name, double cost, string picture, string category)
        {
            Gift g = await _projectContext.Gift.FindAsync(id);
            if (g == null)
                return false;
            g.Name = name;
            g.Cost = cost;
            g.Picture = picture;

            //Category c = await _projectContext.Category.FindAsync(category);
            Category c = await _projectContext.Category.FirstOrDefaultAsync(cat => cat.Name == category);

            if (c == null)
                return false;
            g.Category = c;
            await _projectContext.SaveChangesAsync();

            Console.WriteLine(g);
            return true;
        }
        public async Task<bool> DeleteGift(int id)
        {
            Gift g = await _projectContext.Gift.FindAsync(id);
            if (g == null)
                return false;
            _projectContext.Remove(g);
            await _projectContext.SaveChangesAsync();
            return true;
        }
        public async Task<List<Donor>> GetDonorsByGiftId(int id)
        {
            List<Donor> donors = new List<Donor>();
            donors = await _projectContext.Donation
                                 .Where(d => d.Gift.Id == id)
                                 .Include(d => d.Donor.TypeOfDonation)
                                 .Select(d => d.Donor)
                                 .ToListAsync();
            return donors;
        }
        public async Task<List<Gift>> SearchGiftByName(string name)
        {
            List<Gift> gifts = await _projectContext.Gift.Include(g => g.Category)
                                                         .Where(g => g.Name.Contains(name))
                                                         .ToListAsync();
            return gifts;
        }
        public async Task<List<Gift>> SearchGiftsByDonor(string name)
        {
            //List<Gift> gifts = new List<Gift>();
            //var donations = await _projectContext.Donation
            //    .Include(d => d.Gift) // Include the associated gift
            //    .ThenInclude(g => g.Category) // Include the associated category
            //    .Where(d => d.Donor.Name == name)
            //    .ToListAsync();
            //// Extract the gifts from the donations
            //gifts = donations.Select(d => d.Gift).ToList();
            //return gifts;
            List<Gift> gifts = new List<Gift>();

            var donations = await _projectContext.Donation
                .Include(d => d.Gift)
                .ThenInclude(g => g.Category)
                .Where(d => d.Donor.Name.Contains(name))
                .ToListAsync();

            gifts = donations.Select(d => d.Gift).ToList();

            return gifts;
        }
        public async Task<List<Gift>> SortGiftsByPrice()
        {
            List<Gift> sortedGifts = await _projectContext.Gift.OrderBy(g => g.Cost).Include(g => g.Category).ToListAsync();
            return sortedGifts;

        }


        public async Task<List<Gift>> GetGiftsWithNoDonorOrMoneyDonor()
        {
            List<Gift> giftsWithMoneyDonor = new List<Gift>();

            var moneyDonorIds = await _projectContext.Donation
                .Where(d => d.Donor != null && d.Donor.TypeOfDonation != null && d.Donor.TypeOfDonation.Name == "כסף")
                .Select(d => d.Gift.Id)
                .Distinct()
                .ToListAsync();
            giftsWithMoneyDonor = await _projectContext.Gift.Where(p => moneyDonorIds.Contains(p.Id)).ToListAsync();

            List<Donation> donations = await _projectContext.Donation.ToListAsync();
            List<Gift> gifts = await _projectContext.Gift.ToListAsync();

            foreach (Gift gift in gifts)
            {
                bool existsInDonations = false;

                foreach (Donation donation in donations)
                {
                    if (donation.Gift.Id == gift.Id)
                    {
                        existsInDonations = true;
                        break;
                    }
                }

                if (!existsInDonations)
                {
                    giftsWithMoneyDonor.Add(gift);
                }
            }

            return giftsWithMoneyDonor;
        }

        //אין שימוש באנגולר
        public async Task<List<Gift>> SortGiftsByCategory()
        {
            List<Gift> sortedGifts = await _projectContext.Gift.Include(g => g.Category).OrderByDescending(g => g.Category).ToListAsync();
            return sortedGifts;
        }
        public async Task<List<Category>> GetCategory()
        {
            return await _projectContext.Category.ToListAsync();
        }

        public async Task<List<Gift>> GetGiftsWithNoDonor()
        {
            List<Gift> giftsWithNoDonor = new List<Gift>();

            List<Donation> donations = await _projectContext.Donation.ToListAsync();
            List<Gift> gifts = await _projectContext.Gift.ToListAsync();

            foreach (Gift gift in gifts)
            {
                bool existsInDonations = false;

                foreach (Donation donation in donations)
                {
                    if (donation.Gift.Id == gift.Id)
                    {
                        existsInDonations = true;
                        break;
                    }
                }

                if (!existsInDonations)
                {
                    giftsWithNoDonor.Add(gift);
                }
            }

            return giftsWithNoDonor;
        }
        public async Task<List<Gift>> FilterGiftsByPrice(double minPrice, double maxPrice)
        {
            return _projectContext.Gift.Where(g => g.Cost >= minPrice && g.Cost <= maxPrice).Include(g => g.Category).ToList();
        }
    }
}
