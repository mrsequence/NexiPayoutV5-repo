using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexiPayout.Models.Payouts
{
    public class PayoutResponse
    {
        public int numberOfPayouts { get; set; }
        public ICollection<Payout> payouts { get; set; }
    }
}
