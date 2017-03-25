namespace FFY.Services.Utilities.Providers
{
    public interface IDirectoryProvider
    {
        void CreateDirectory(string path);

        bool IsDirectoryExisting(string path);
    }
}
