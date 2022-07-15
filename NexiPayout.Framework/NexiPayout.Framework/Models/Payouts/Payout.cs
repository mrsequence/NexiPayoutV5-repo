using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexiPayout.Models.Payouts
{
    public class Payout
    {
        public string id { get; set; }
        public string reference { get; set; }
        public string date { get; set; }
        public string bankAccountIban { get; set; }
        public string bankAccountBic { get; set; }
        public string currency { get; set; }
        public int amount { get; set; }
        public int chargedAmount { get; set; }
        public int refundedAmount { get; set; }
        public int fees { get; set; }
        public int feeTaxRate { get; set; }
        public int feeTaxAmount { get; set; }
        public ICollection<TransactionTaxDetail> transactionTaxDetails { get; set; }
        public int numberOfPaymentActions { get; set; }


    }
}
