using GetMyIpLibrary;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.Threading;
using NMock;
using RichardSzalay.MockHttp;

namespace Tests
{
    [TestFixture]
    public class GetMyIpTests
    {
        private MockRepository mockRepository;

        [SetUp]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
        }

        [TearDown]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void TestStripHtml()
        {
            string htmlIp = "<b>127.0.0.1</b>";
            string strippedIp = "127.0.0.1";
            Assert.AreEqual(GetMyIpLibrary.MyIp.StripHtml(htmlIp), strippedIp);
        }
        [Test]
        public void TestGetIpAsyncRequestWorks()
        {


            MyIp getMyIp = this.CreateGetMyIp();
            ConcurrentQueue<string> ipq = new ConcurrentQueue<string>();
            GetMyIpLibrary.MyIp.GetIpAsync(ipq);
            Assert.AreEqual(0, ipq.Count);
            while (ipq.Count == 0)
            {
                Thread.Sleep(10);
            }
            // after there has been a response, count should now be 1
            Assert.AreEqual(1, ipq.Count);
        }
        [Test]
        public void TestGetIpBlocking()
        {
            MyIp myip = new MyIp();

            string myIpAddress = myip.GetIpBlocking();
            Assert.IsNotEmpty(myIpAddress);
        }
        [Test]
        public void TestGetIpBlockingReturnsCorrectIp()
        {
            // set up mock HttpCLient and pass it to MyIp()
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(GetMyIpLibrary.MyIp.ipAddressDetectionUrl).Respond("content/text", "<b>127.0.0.1</b>");
            

            MyIp myip = new MyIp();
            myip.client = mockHttp.ToHttpClient();
            string testIp = "127.0.0.1";
            string myIpAddress = myip.GetIpBlocking();
            Assert.AreEqual(testIp, myIpAddress);
        }

        private MyIp CreateGetMyIp()
        {
            return new MyIp();
        }
    }
}