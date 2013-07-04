using SharpTestsEx;

namespace Extensions.Tests
{
    public abstract class TestsBase
    {
        protected void AssertTrue(bool value)
        {
            value.Should().Be.True();
        }

        protected void AssertTrue(bool value, string message)
        {
            value.Should(message).Be.True();
        }

        protected void AssertFalse(bool value)
        {
            value.Should().Be.False();
        }

        protected void AssertFalse(bool value, string message)
        {
            value.Should(message).Be.False();
        }

        protected void AssertIsNull(object value)
        {
            value.Should().Be.Null();
        }

        protected void AssertIsNull(object value, string message)
        {
            value.Should(message).Be.Null();
        }

        protected void AssertIsNotNull(object value)
        {
            value.Should().Not.Be.Null();
        }

        protected void AssertIsNotNull(object value, string message)
        {
            value.Should(message).Not.Be.Null();
        }

        protected void AssertFail(string message)
        {
            true.Should(message).Be.False();
        }

        protected void AssertNotEqual(object valueOne, object valueTwo)
        {
            valueOne.Should().Not.Be.EqualTo(valueTwo);
        }

        protected void AssertNotEqual(object valueOne, object valueTwo, string message)
        {
            valueOne.Should(message).Not.Be.EqualTo(valueTwo);
        }

        protected void AssertEqual(object valueOne, object valueTwo)
        {
            valueOne.Should().Be.EqualTo(valueTwo);
        }

        protected void AssertEqual(object valueOne, object valueTwo, string message)
        {
            valueOne.Should(message).Be.EqualTo(valueTwo);
        }
    }
}
