using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaPlus.Models
{
    public class Session
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Cinema { get; set; }
        public string Hall { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Price { get; set; }

        public override bool Equals(object other)
        {
            var _session = other as Session;
            return Cinema == _session.Cinema &&
                   Hall == _session.Hall &&
                   Date == _session.Date &&
                   Time == _session.Time &&
                   Price == _session.Price;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
