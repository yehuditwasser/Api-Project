using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Donation
    {
        public int Id { get; set; }
        public Donor Donor { get; set; }    
        public Gift Gift { get; set; }

    }
}
