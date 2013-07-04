using Extensions.Core.Components;
using Xunit;


namespace Extensions.Tests
{
    public class ComponentHandlerTests : TestsBase
    {
        private static IComponentHandler<IComponent> DefaultComponentHandler
        {
            get
            {
                return TestProperties.DefaultHandler;
            }
        }

        [Fact]
        public void ComponentsAreAdded()
        {
            DefaultComponentHandler.AddComponent(new DefaultComponent(""));
            AssertTrue(DefaultComponentHandler.Count > 0);
        }
    }
}
