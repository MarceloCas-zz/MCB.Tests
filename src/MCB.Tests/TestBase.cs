using Xunit.Abstractions;

namespace MCB.Tests
{
    public abstract class TestBase
    {
        // Properties
        protected ITestOutputHelper TestOutputHelper { get; }

        // Constructors
        protected TestBase(ITestOutputHelper testOutputHelper)
        {
            TestOutputHelper = testOutputHelper;
        }
    }
}
