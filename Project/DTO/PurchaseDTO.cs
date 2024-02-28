using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Server_Api.DTO
{
    public class PurchaseDTO
    {
        public int Id { get; set; }

        public string UserId { get; set; }


        public DateTime Date { get; set; }

        public ICollection<PurchaseDetailsDTO> PurchaseDetails { get; set; } =new List<PurchaseDetailsDTO>();
            
    }
}
