using CinemaPlus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaPlus.ViewModels.AdminSideViewModels
{
    public class PurchasedTicketCellUCViewModel : BaseViewModel
    {
        private Ticket ticket;

        public Ticket Ticket
        {
            get { return ticket; }
            set { ticket = value; OnPropertyChanged(); }
        }

        public PurchasedTicketCellUCViewModel(Ticket _ticket)
        {
            Ticket = _ticket;
        }
    }
}
