using Project.Models;
using Server_Api.DTO;

namespace Project.DTO
{
    public class GiftWithPurchasesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public string Picture { get; set; }
        public Category Category { get; set; }
        public List<PurchaseDetailsDTO> Purchases { get; set; }
    }
}
