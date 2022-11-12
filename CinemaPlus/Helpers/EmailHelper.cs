using CinemaPlus.Models;
using CinemaPlus.Views.UserControls.MovieUC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using System.Windows;
using System.Reflection;
using System.Drawing;
using System.Windows.Interop;
using CinemaPlus.Views.Windows;
using System.Drawing.Imaging;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Threading;

namespace CinemaPlus.Helpers
{
    public class EmailHelper
    {
        public static void SendEmail(string toGmail, string messageSubject, string message, List<MovieTicketUC> ticketList)
        {
            try
            {
                using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.EnableSsl = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = true;
                    client.Credentials = new NetworkCredential("cinemakhan.cinema@gmail.com", "vtwumsciykkzydcg");
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.To.Add(toGmail);
                    mailMessage.From = new MailAddress("cinemakhan.cinema@gmail.com", "Cinema KHAN");
                    mailMessage.Subject = messageSubject;
                    mailMessage.Body = message;

                    int x = 1;
                    foreach (var ticket in ticketList)
                    {
                        var movieTicketWindow = new MovieTicketWindow();
                        movieTicketWindow.WrapPanel.Children.Add(ticket);
                        movieTicketWindow.Visibility = Visibility.Visible;
                        movieTicketWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        movieTicketWindow.Owner = App.Current.MainWindow;
                        movieTicketWindow.Show();
                        Thread.Sleep(500);
                        var bitmap = ScreenCapture.CaptureWindow(new WindowInteropHelper(movieTicketWindow).Handle);

                        bitmap.Save($"ticket{x}.png");
                        Document doc = new Document(PageSize.A4);
                        doc.Open();
                        iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(bitmap, System.Drawing.Imaging.ImageFormat.Png);
                        doc.Add(pdfImage);
                        doc.Close();

                        mailMessage.Attachments.Add(new Attachment($"ticket{x}.png"));
                        x++;
                    }

                    client.Port = 587;
                    client.Credentials = new System.Net.NetworkCredential("cinemakhan.cinema@gmail.com", "vtwumsciykkzydcg");
                    client.EnableSsl = true;
                    client.Send(mailMessage);
                }
            }
            catch (Exception)
            {

                throw;
            }
        
        }
    }
}
