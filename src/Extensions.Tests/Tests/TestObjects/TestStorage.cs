using System.Linq;

namespace Extensions.Tests.TestObjects
{
    public class TestStorage<DataType>
    {
        public IQueryable<TestObject<DataType>> TestObjects { get; set; }
    }
}
