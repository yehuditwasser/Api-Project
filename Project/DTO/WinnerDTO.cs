using Project.Models;

namespace Project.DTO
{
    public class WinnerDTO
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public int GiftId { get; set; }

        public Gift Gift { get; set; }

        public UserDTO User { get; set; }
    }
}
