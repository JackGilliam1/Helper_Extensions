using Extensions.Core.Components;

namespace Extensions.Tests
{
    class TestProperties
    {
        private static readonly IComponentHandler<IComponent> _defaultHandler = new DefaultComponentHandler<IComponent>();

        public static IComponentHandler<IComponent> DefaultHandler
        {
            get
            {
                return _defaultHandler;
            }
        }

        public static void Clear()
        {
            _defaultHandler.Clear();
        }
    }
}
