using JWTAuthentication.Authentication;

namespace Project.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime Date { get; set; }
        public ICollection<PurchaseDetails> PurchaseDetails { get; set; }

    }
}