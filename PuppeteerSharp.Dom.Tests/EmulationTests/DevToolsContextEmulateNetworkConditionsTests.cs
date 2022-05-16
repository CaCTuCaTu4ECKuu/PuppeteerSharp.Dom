using System.Net;
using System.Threading.Tasks;
using WebView2.DevTools.Dom;
using PuppeteerSharp.Dom.Tests.Attributes;
using Xunit;
using Xunit.Abstractions;

namespace PuppeteerSharp.Dom.Tests.DevToolsContextTests
{
    [Collection(TestConstants.TestFixtureCollectionName)]
    public class EmulateNetworkConditionsTests : PuppeteerPageBaseTest
    {
        public EmulateNetworkConditionsTests(ITestOutputHelper output) : base(output)
        {
        }

        [PuppeteerDomFact]
        public async Task ShouldChangeNavigatorConnectionEffectiveType()
        {
            var slow3G = PredefinedNetworkConditions.GetNetworkCondition(NetworkConditions.Slow3G);
            var fast3G = PredefinedNetworkConditions.GetNetworkCondition(NetworkConditions.Fast3G);

            Assert.Equal("4g", await DevToolsContext.EvaluateExpressionAsync<string>("window.navigator.connection.effectiveType").ConfigureAwait(true));
            await DevToolsContext.EmulateNetworkConditionsAsync(fast3G);
            Assert.Equal("3g", await DevToolsContext.EvaluateExpressionAsync<string>("window.navigator.connection.effectiveType").ConfigureAwait(true));
            await DevToolsContext.EmulateNetworkConditionsAsync(slow3G);
            Assert.Equal("2g", await DevToolsContext.EvaluateExpressionAsync<string>("window.navigator.connection.effectiveType").ConfigureAwait(true));
            await DevToolsContext.EmulateNetworkConditionsAsync(null);
        }
    }
}
