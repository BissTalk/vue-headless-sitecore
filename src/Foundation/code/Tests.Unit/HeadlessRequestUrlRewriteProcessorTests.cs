using System;
using System.Collections.Generic;
using System.Web;
using HeadlessSc.Pipelines;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HeadlessSc.Tests.Unit
{
    [TestClass]
    public class HeadlessRequestUrlRewriteProcessorTests
    {
        [TestMethod]
        public void TestCanWriteValidRequest()
        {
            var uri = new Uri($"https://www.xfinity.com{ ModuleInfo.RoutePrefix }buy/home/page1/en.json");
            var requestMock = new Mock<HttpRequestBase>();
            requestMock.Setup(r => r.Url).Returns(uri);
            var contextMock = new Mock<HttpContextBase>();
            var items = new Dictionary<string, object>();
            contextMock.SetupGet(c => c.Items).Returns(items);
            contextMock.Setup(c => c.Request).Returns(requestMock.Object);
            contextMock.Setup(c => c.RewritePath(It.Is<string>(s => 
                    MatchesExpected(s, ModuleInfo.RoutePrefix.TrimEnd('/'), "buy", "/home/page1", "en")
                ))).Verifiable("The RewritePath method was never called with the expected URL.");

            var context = contextMock.Object;
            var processor = new HeadlessRequestUrlRewriteProcessor();
            processor.ExecuteRewrite(context, ModuleInfo.RoutePrefix.TrimEnd('/'));

            contextMock.Verify();
            Assert.IsTrue(context.IsHeadless());
        }

        [TestMethod]
        public void TestProcessorWillNotModifyRequestThatAreNotForHeadless()
        {
            var uri = new Uri("https://www.xfinity.com/buy/home/page1/en.json");
            var requestMock = new Mock<HttpRequestBase>();
            requestMock.Setup(r => r.Url).Returns(uri);
            var contextMock = new Mock<HttpContextBase>();
            var items = new Dictionary<string, object>();
            contextMock.SetupGet(c => c.Items).Returns(items);
            contextMock.Setup(c => c.Request).Returns(requestMock.Object);
            contextMock.Setup(c => c.RewritePath(It.IsAny<string>()))
                .Throws(new AssertFailedException("RewritePath was called unexpectedly"));

            var context = contextMock.Object;
            var processor = new HeadlessRequestUrlRewriteProcessor();
            processor.ExecuteRewrite(context, ModuleInfo.RoutePrefix.TrimEnd('/'));

            Assert.IsFalse(context.IsHeadless());
        }

        [TestMethod]
        public void TestPipelineExitsWithMissingContext()
        {
            var builder = new Mock<HeadlessRequestUrlRewriteProcessor>();
            builder.Setup(p => p.ExecuteRewrite(It.IsAny<HttpContextBase>(), It.IsAny<string>()))
                .Throws(new AssertFailedException("The ExecuteRewrite method should not be called"));
           
            var processor = new HeadlessRequestUrlRewriteProcessor();
            processor.Process(null);
        }

       [TestMethod]
        public void TestTheTestCodeCanValidateGoodUrls()
        {
            Assert.IsTrue(MatchesExpected(
                $"{ModuleInfo.RoutePrefix.TrimEnd('/')}?sc_site=buy&path=/home/page1&sc_lang=en",
                ModuleInfo.RoutePrefix.TrimEnd('/'), "buy", "/home/page1", "en"));
        }

        private bool MatchesExpected(string newUrl, string local, string site, string path, string language)
        {
            var url = new Uri($"http://localhost{newUrl}");
            var query = HttpUtility.ParseQueryString(url.Query);
            return url.LocalPath == local
                   && query["sc_site"] == site
                   && query["path"] == path
                   && query["sc_lang"] == language;
        }
    }
}
