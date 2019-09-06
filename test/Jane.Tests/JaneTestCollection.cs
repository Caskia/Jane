using Xunit;

namespace Jane.Tests
{
    [CollectionDefinition(nameof(JaneTestCollection))]
    public class JaneTestCollection : ICollectionFixture<JaneTestFixture>
    {
    }
}