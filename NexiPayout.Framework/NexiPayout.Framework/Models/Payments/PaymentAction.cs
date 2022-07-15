﻿using NexiPayout.Models.Payouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexiPayout.Models.Payments
{
    public class PaymentAction
    {
        public string id { get; set; }  
        public string paymentId { get; set; }
        public string paymentAction { get; set; }
        public string paymentMethod { get; set; }
        public string paymentType { get; set; }
        public string timestamp { get; set; }
        public string reference { get; set; }
        public string invoiceNumber { get; set; }
        public int amount { get; set; }
        public int vatAmount { get; set; }
        public string currency { get; set; }
        public int fee { get; set; }
        public int flexibleFee { get; set; }
        public int fixedFee { get; set; }
        public int feeVATpercent { get; set; }
        public int feeTaxRate { get; set; }
        public int feeTaxAmount { get; set; }
        public ICollection<TransactionTaxDetail> transactionTaxDetails { get; set; }
        public string subscriptionId { get; set; }

    }
}
