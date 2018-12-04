namespace Jane
{
    public interface IDataGenerator
    {
        string GetRandomString(byte length, bool includeDigit = true, bool includeUppercase = true, bool includeLowercase = true);
    }
}