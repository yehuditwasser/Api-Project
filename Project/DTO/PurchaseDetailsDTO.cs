using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Api.DTO
{
    public class PurchaseDetailsDTO
    {
        public int? Id { get; set; }

        public int GiftId { get; set; }

        public int? Quantity { get; set; }

        public Gift Gift { get; set; }
        public DateTime Date { get; internal set; }
    }
}
