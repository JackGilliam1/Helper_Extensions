using Extensions.Core.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Extensions.Tests
{
    [TestClass]
    public class ComponentHandlerTests
    {
        private static IComponentHandler<IComponent> DefaultComponentHandler
        {
            get
            {
                return TestProperties.DefaultHandler;
            }
        }

        [TestMethod]
        public void ComponentsAreAdded()
        {
            DefaultComponentHandler.AddComponent(new DefaultComponent(""));
            Assert.IsTrue(DefaultComponentHandler.Count > 0);
        }
    }
}
