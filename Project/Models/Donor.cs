namespace Project.Models
{
    public class Donor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
        public TypeOfDonation TypeOfDonation { get; set; }

        //public ICollection<Donation> Donation { get; set; }
    }
}
