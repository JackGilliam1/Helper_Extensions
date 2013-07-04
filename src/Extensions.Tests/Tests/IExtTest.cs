using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Extensions.Tests
{
    [TestClass]
    public abstract class IExtTest
    {
        protected const int TestInteger = 5;
        protected const double TestDouble = 5.1;
        protected const float TestFloat = 5.5f;
        protected const double TestRangeMin = 1.1;
        protected const double TestRangeMax = 10.1;
    }
}
