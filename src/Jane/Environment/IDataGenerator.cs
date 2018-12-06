namespace Jane
{
    public interface IDataGenerator
    {
        string GetRandomString(byte length, bool includeDigit = true, bool includeUppercase = true, bool includeLowercase = true);

        string NumberToS36(long number);

        string NumberToS64(long number);

        long S36ToNumber(string s36);

        long S64ToNumber(string s64);
    }
}