using System.Threading.Tasks;
using PuppeteerSharp.Dom.Tests.Attributes;
using Xunit.Abstractions;
using Xunit;

namespace PuppeteerSharp.Dom.Tests.ElementHandleTests
{

    [Collection(TestConstants.TestFixtureCollectionName)]
    public class NamedNodeMapTest : PuppeteerPageBaseTest
    {
        public NamedNodeMapTest(ITestOutputHelper output) : base(output)
        {
        }

        [PuppeteerDomFact]
        public async Task ShouldWork()
        {
            await Page.GoToAsync(TestConstants.ServerUrl + "/dataattributes.html");
            var namedNodeMap = await Page.QuerySelectorAsync<HtmlElement>("h1").AndThen(x => x.GetAttributesAsync());

            Assert.NotNull(namedNodeMap);

            var data = await namedNodeMap.ToArrayAsync();

            Assert.NotNull(data);
            Assert.NotEmpty(data);

            Assert.Equal("data-testing", await data[0].GetNameAsync());
            Assert.Equal("Test1", await data[0].GetValueAsync());
        }
    }
}
