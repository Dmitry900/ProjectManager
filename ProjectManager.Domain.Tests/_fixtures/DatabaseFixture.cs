namespace ProjectManager.Domain.Tests._fixtures
{
    [CollectionDefinition(nameof(DatabaseFixture))]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }

    public class DatabaseFixture : IAsyncDisposable
    {
        public DatabaseFixture()
        {

        }

        #region IAsyncLifetime members

        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}