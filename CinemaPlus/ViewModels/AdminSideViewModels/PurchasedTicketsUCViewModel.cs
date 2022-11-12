using CinemaPlus.Helpers;
using CinemaPlus.Models;
using CinemaPlus.ViewModels.EndingViewModels;
using CinemaPlus.Views.UserControls.AdminSide;
using CinemaPlus.Views.UserControls.EndOfPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CinemaPlus.ViewModels.AdminSideViewModels
{
    public class PurchasedTicketsUCViewModel : BaseViewModel
    {
        public WrapPanel PurchasedTicketsWrapPanel { get; set; }

        public void CreateCellsForPurchasedTickets()
        {
            var allPurchasedTickets = JsonSerialization<Ticket>.Deserialize(@"~/../../../Files\purchasedTickets.json");
            PurchasedTicketsWrapPanel.Children.Clear();
            if (allPurchasedTickets.Count == 0)
            {
                var noResultUC = new NoResultUC();
                var noResultViewModel = new NoResultUCViewModel("There is no purchased ticket yet . . . ");
                noResultUC.DataContext = noResultViewModel;
                PurchasedTicketsWrapPanel.Children.Add(noResultUC);
                return;
            }
            foreach (var ticket in allPurchasedTickets)
            {
                var purchasedTicketView = new PurchasedTicketCellUC();
                var purchasedTicketViewModel = new PurchasedTicketCellUCViewModel(ticket);
                purchasedTicketView.DataContext = purchasedTicketViewModel;
                PurchasedTicketsWrapPanel.Children.Add(purchasedTicketView);
            }
        }
    }
}
