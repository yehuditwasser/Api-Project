using JWTAuthentication.Authentication;

namespace Project.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string UserId { get; set; }
        public int GiftId { get; set; }
        public Gift Gift { get; set; }
    }
}
