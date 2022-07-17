using NexiPayout.ClientManagers;
using NexiPayout.Framework.Logging;
using NexiPayout.Models.Payouts;
using Ninject;
using NUnit.Framework;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;

namespace NexiPayout.Tests
{
    [TestFixture]
    public class PayoutTests
    {
        private PayoutResponse payoutResponse;
        private PayoutDetailResponse payoutDetailResponse;
        private PayoutManager manager;

        private string V1Eendpoint = "report/v1/payouts";
        private string V2Eendpoint = "report/v2/payouts";

        [SetUp]
        public void InitializeDepencyInjection()
        {
            NinjectBinding.SetLogger();
        }

        public void Setup(string endpoint)
        {
            manager = new PayoutManager(NinjectBinding.PayoutLogger);

            payoutResponse = manager.GetAllPayouts(endpoint);
        }

        public void SetupCallToV1()
        {
            Setup(V1Eendpoint);
        }

        [Test]
        public void ValidateUserCanRetrieveAllPayouts()
        {
            SetupCallToV1();
            Assert.IsNotNull(payoutResponse);
            Assert.AreEqual(HttpStatusCode.OK, manager.PayoutClient.GetStatusCode());
            Assert.AreEqual(2, payoutResponse.numberOfPayouts);
        }

        [Test]
        public void ValidateThatUserCannotReachServerWithInvalidEndpointWithNonOkResponse()
        {
            Setup(V2Eendpoint);
            Assert.AreEqual(HttpStatusCode.InternalServerError, manager.PayoutClient.GetStatusCode());
        }


        [Test]
        public void VerifyThatTheAMOUNTIsAnIntegerValueWhenGettingPayoutDetails()
        {
            SetupCallToV1();
            payoutDetailResponse = manager.GetPayoutDetailById(payoutResponse.payouts.ElementAt(1).id);
            Assert.IsTrue(payoutDetailResponse.amount is int);
        }

        [Test]
        public void ValidateThatCurrencyHasRIghtFormat()
        {
            SetupCallToV1();
            payoutDetailResponse = manager.GetPayoutDetailById(payoutResponse.payouts.ElementAt(1).id);
            Assert.That(payoutDetailResponse.currency.Length == 3);
        }

        public void GetUserPayoutDetails(string endpoint)
        {
            Setup(endpoint);
            payoutDetailResponse = manager.GetPayoutDetailById(payoutResponse.payouts.ElementAt(1).id);

        }

        [Test]
        public void ValidateUserCanRetirevePayoutDetailById()
        {
            GetUserPayoutDetails("report/v1/payouts");
            Assert.IsNotNull(payoutDetailResponse);
            Assert.AreEqual(HttpStatusCode.OK, manager.PayoutClient.GetStatusCode());
        }

        [Test]
        public void ValidatePayoutDetailsPropertyFormats()
        {
            GetUserPayoutDetails("report/v1/payouts");
            var firstPaymentAction = payoutDetailResponse.paymentActions.ElementAt(0);
            Assert.That(firstPaymentAction.paymentType.Equals("CARD") || firstPaymentAction.paymentType.Equals("INVOICE"));
            Assert.IsTrue(firstPaymentAction.vatAmount is int);
            foreach (var paymentAction in payoutDetailResponse.paymentActions)
            {
                Assert.That(paymentAction.paymentAction.Equals("CHARGE") || paymentAction.paymentAction.Equals("REFUND")
                    || paymentAction.paymentAction.Equals("DEPOSIT"));
                 
                DateTime d;
                Assert.True(DateTime.TryParseExact(paymentAction.timestamp, new string[] { "yyyy-MM-dd'T'HH:mm:ss.fff'Z'" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out d));
            }

        }
    }
}
