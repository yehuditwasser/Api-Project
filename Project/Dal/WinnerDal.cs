using Microsoft.EntityFrameworkCore;
using Project.DTO;
using Project.Models;
using System.Net;
using System.Net.Mail;

namespace Project.Dal
{
    public class WinnerDal : IWinnerDal
    {
        private readonly ProjectContext _projectContext;

        public WinnerDal(ProjectContext projectContext)
        {
            this._projectContext = projectContext;
        }

        public async Task<bool> Raffle(int giftId)
        {

            List<PurchaseDetails> purchaseDetails = await _projectContext.PurchaseDetails
            .Include(p => p.Purchase) // Include the Purchase navigation property
            .Where(p => p.GiftId == giftId)
            .ToListAsync();

            Gift g = new Gift();
            g = await _projectContext.Gift.FindAsync(giftId);

            if (purchaseDetails.Count > 0)
            {
                Random random = new Random();
                int randomIndex = random.Next(0, purchaseDetails.Count);
                PurchaseDetails selectedPurchaseDetail = purchaseDetails[randomIndex];

                if (selectedPurchaseDetail.Purchase != null) // Check if Purchase is not null
                {
                    Winner winner = new Winner();
                    winner.GiftId = selectedPurchaseDetail.GiftId;
                    winner.UserId = selectedPurchaseDetail.Purchase.UserId;

                    await _projectContext.Winner.AddAsync(winner);
                    g.Raffeled = true;
                    await _projectContext.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }
        public async Task<List<WinnerDTO>> GetWinners()
        {
            var winners = await _projectContext.Winner
                .Include(w => w.Gift)
                .Include(w => w.User)
                .ToListAsync();

            var winnersWithDetails = new List<WinnerDTO>();

            foreach (var winner in winners)
            {
                var winnerWithDetails = new WinnerDTO
                {
                    GiftId = winner.GiftId,
                    UserId = winner.UserId,
                    Gift = winner.Gift,
                    User = new UserDTO
                    {
                        Name = winner.User.UserName,
                        Email = winner.User.Email
                    }
                };
                winnersWithDetails.Add(winnerWithDetails);
            }
            return winnersWithDetails.ToList();
        }

        public void SendEmail(string emailTo)
        {
            // Set up the email message
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("YadBeyad2024@gmail.com", "יד ביד - מכירה סינית 2024");
            mail.To.Add(emailTo);
            mail.Subject = "מערכת יד ביד - מכירה סינית 2024";
            mail.Body = "<div dir=\"rtl\">מזל טוב על זכייתך במבצע סין.<br><br>המשך לצבור זכויות ותתרום תמיד אלינו</div>";

            mail.IsBodyHtml = true;

            // Set up the SMTP client
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587; // or the appropriate port number
            smtpClient.Credentials = new NetworkCredential("YadBeyad2024@gmail.com", "rldm vxiq taeh ccko");
            //smtpClient.Credentials = new NetworkCredential("yehuda5862522@gmail.com", "stdu ywhk eski eixd");
            smtpClient.EnableSsl = true;

            // Send the email
            smtpClient.Send(mail);
        }
        public Dictionary<string, int> CalculateGiftRevenue()
        {
            List<Purchase> purchases = _projectContext.Purchase.Include(p => p.PurchaseDetails).ThenInclude(p => p.Gift).ToList();
            Dictionary<string, int> giftRevenue = new Dictionary<string, int>();

            foreach (var purchase in purchases)
            {
                foreach (var purchaseDetail in purchase.PurchaseDetails)
                {
                    string giftName = purchaseDetail.Gift.Name;
                    int quantity = purchaseDetail.Quantity;
                    double giftCost = purchaseDetail.Gift.Cost;
                    int revenue = (int)(quantity * giftCost);

                    if (giftRevenue.ContainsKey(giftName))
                    {
                        giftRevenue[giftName] += revenue;
                    }
                    else
                    {
                        giftRevenue[giftName] = revenue;
                    }
                }
            }

            return giftRevenue;
        }
    }
}
