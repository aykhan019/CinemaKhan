using System;
using System.Collections.Generic;
using System.Linq;  
using System.Text;
using System.Threading.Tasks;

namespace CinemaPlus.Models
{
    public class Ticket
    {
        public string MovieTitle { get; set; }
        public string MoviePoster { get; set; }
        public string CinemaName { get; set; }
        public string HallName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string SeatNo { get; set; }
        public string RowNo { get; set; }
        public string Price { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Payment { get; set; } 
        public string CardNumber { get; set; }
        public string PurchaseDate { get; set; }
    }
}
