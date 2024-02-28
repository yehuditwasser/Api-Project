using JWTAuthentication.Authentication;

namespace Project.Models
{
    public class Winner
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int GiftId { get; set; }

        public Gift Gift { get; set; }

        public ApplicationUser User { get; set; }
    }
}
