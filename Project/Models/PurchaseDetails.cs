namespace Project.Models
{
    public class PurchaseDetails
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int PurchaseId { get; set; }
        public int GiftId { get; set; }
        public Purchase Purchase { get; set; }
        public Gift Gift { get; set; }
    }
}